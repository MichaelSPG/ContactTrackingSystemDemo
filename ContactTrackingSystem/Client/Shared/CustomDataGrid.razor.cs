using ContactTrackingSystem.Shared.Filtering;
using Microsoft.AspNetCore.Components;
using System.Reflection;
using ContactTrackingSystem.Shared.Extensions;

namespace ContactTrackingSystem.Client.Shared
{
    /// <summary>
    /// Custom datagrid, this was created by my own with some inspirations in order to deliever an alternative way to show data
    /// </summary>
    /// <typeparam name="TItem">Generic type</typeparam>
    public partial class CustomDataGrid<TItem> : ComponentBase where TItem : class
    {
        [Parameter] public int ItemsPerPage { get; set; }
        [Parameter] public List<TItem> Items { get; set; }
        [Parameter] public EventCallback<TItem> OnRemoveItemRequest { get; set; }
        [Parameter] public EventCallback<TItem> OnEditItemRequest { get; set; }
        public List<TItem> FilteredItems { get; set; }
        private List<Column> Columns { get; set; }
        private LinkedList<Filter> FilterLinkedList { get; set; }
        private Filter CurrentFilter { get; set; }
        private bool IsNewFilter { get; set; }
        private bool ShowFiltersMenu { get; set; }
        private bool ShowActiveFilters { get; set; }
        private int CurrentPage { get; set; }
        private int MaxPages { get; set; }
        public string FilterMessage { get; private set; }

        /// <summary>
        /// Constructor, initializes all
        /// </summary>
        public CustomDataGrid()
        {
            Items = new List<TItem>();
            FilteredItems = new List<TItem>();
            ShowFiltersMenu = false;
            CurrentFilter = new Filter();
            FilterLinkedList = new LinkedList<Filter>();
        }

        protected override Task OnInitializedAsync()
        {
            Columns = new List<Column>();
            var ModelType = typeof(TItem);
            {
                List<PropertyInfo> properties = ModelType.GetProperties().ToList();
                foreach (PropertyInfo property in properties)
                {
                    Columns.Add(new Column { Name = property.Name, PropertyName = property.Name });
                }
            }
            Update();
            return base.OnInitializedAsync();
        }
        /// <summary>
        /// Updates the datagrid paging after a change to filter or elements
        /// </summary>
        protected void UpdatePaging()
        {
            CurrentPage = 0;
            int pages = 0;
            if (ItemsPerPage > 0)
            {
                pages = (FilterLinkedList.Any() ? FilteredItems.Count : Items.Count);
                MaxPages = pages / ItemsPerPage;
                MaxPages += (pages % ItemsPerPage) != 0 ? 1 : 0;
            }
            else
                MaxPages = 1;
        }
        /// <summary>
        /// Triggers the event for editing a element
        /// </summary>
        /// <param name="item">the generic item</param>
        /// <returns>Task</returns>
        private async Task CallEditItem(TItem item)
        {
            if (OnEditItemRequest.HasDelegate)
                await OnEditItemRequest.InvokeAsync(item);
        }
        /// <summary>
        /// Triggers the event for deleting a element
        /// </summary>
        /// <param name="item">the generic item</param>
        /// <returns>Task</returns>
        private async Task CallRemoveItem(TItem item)
        {
            if (OnRemoveItemRequest.HasDelegate)
                await OnRemoveItemRequest.InvokeAsync(item);
        }
        /// 
        /// <summary>
        /// Gets the value of a property through reflection
        /// </summary>
        /// <param name="item">the generic item to extract the values from</param>
        /// <param name="ColumnName">The property name of the generic item instance</param>
        /// <returns>The value of property</returns>
        protected object? GetValueOfItem(TItem item, string ColumnName)
        {
            var ModelType = typeof(TItem);
            PropertyInfo? property = ModelType.GetProperty(ColumnName);
            return property?.GetValue(item, null);
        }
        /// <summary>
        /// Gets the containing node of a Filter
        /// </summary>
        /// <param name="value">The filter to find out</param>
        /// <returns></returns>
        protected LinkedListNode<Filter>? FindFilterNode(Filter value)
        {
            return FilterLinkedList.Find(value);
        }
        /// <summary>
        /// Adds a filter to the current datagrid
        /// </summary>
        /// <param name="filter">The filter to add or edit</param>
        /// <param name="connectorType">The connector type</param>
        protected void AddEditFilter(Filter filter, ConnectorType? connectorType)
        {
            FilterMessage = "";
            if (FilterLinkedList.First == null && IsNewFilter)
            {
                FilterLinkedList.AddFirst(filter);
            }
            else if (IsNewFilter && FilterLinkedList.Count <= 3)
            {
                FilterLinkedList.Last.ValueRef.ConnectorType = connectorType.Value;
                FilterLinkedList.AddLast(filter);
            }
            else if (!IsNewFilter)
            {
                var res = FilterLinkedList.ToList().Find(x => filter.Id.Equals(x.Id));
                res?.CopyFrom(filter);
            }
            if(FilterLinkedList.Count > 3)
            {
                FilterMessage = "Max Filter count reached!";
            }
            HideNewFilterMenu();
            Update();
            StateHasChanged();
        }
        /// <summary>
        /// Removes a filter from the current datagrid
        /// </summary>
        /// <param name="filter">The filter to remove</param>
        protected void RemoveFilter(Filter filter)
        {
            if (filter == null)
                return;
            FilterLinkedList.Remove(filter);
            Update();
            StateHasChanged();
        }
        /// <summary>
        /// Shows or hides a menu for creating or editing a filter
        /// </summary>
        /// <param name="filter"></param>
        protected void ShowFilterConfigMenu(Filter? filter = null)
        {
            if (filter == null)
                CurrentFilter = new Filter();
            else
                CurrentFilter = new Filter(filter);
            IsNewFilter = filter == null;
            CurrentFilter.Id = IsNewFilter ? CurrentFilter.Id : filter.Id;
            ShowFiltersMenu = true;
        }
        /// <summary>
        /// Hides the filer menu
        /// </summary>
        protected void HideNewFilterMenu()
        {
            ShowFiltersMenu = false;
        }
        /// <summary>
        /// Changes the ConnectorType between 'And' and 'Or' operators to alter the resulting filtered datagrid
        /// </summary>
        /// <param name="filter">The filter which contains the ConnectorType</param>
        protected void SwapConnectorType(Filter? filter)
        {
            var liked = FilterLinkedList.Find(filter);
            liked.Previous.ValueRef.ConnectorType = liked.Previous.Value.ConnectorType == ConnectorType.And ? ConnectorType.Or : ConnectorType.And;
            Update();

        }
        /// <summary>
        /// Applies a filter to a collection of generic items
        /// </summary>
        /// <param name="field">The current value of the field to compare </param>
        /// <param name="value">The data to compare wirh the current value</param>
        /// <param name="filterType">Which filter type to apply</param>
        /// <param name="matchCase">Enables or diables the case validation</param>
        /// <returns>The comparation result</returns>
        private bool ApplyFilterType(object? field, string value, FilterType filterType, bool matchCase)
        {
            var asString = field?.ToString();
            if (!matchCase)
            {
                asString = asString?.ToLower();
                value = value.ToLower();
            }

            switch (filterType)
            {
                case FilterType.None:
                    return true;
                case FilterType.Contains:
                    return asString.Contains(value);
                case FilterType.Equals:
                    return asString.Equals(value);
                case FilterType.NotContains:
                    return !asString.Contains(value);
                case FilterType.NotEquals:
                    return !asString.Equals(value);
                case FilterType.StartsWith:
                    return asString.StartsWith(value);
                case FilterType.EndsWith:
                    return asString.EndsWith(value);
                default:
                    return false;
            }
        }
        /// <summary>
        /// Updates the content of datagrid applying filters and updating the paging of elements
        /// </summary>
        public void Update()
        {
            ApplyFilters();
            UpdatePaging();
        }
        /// <summary>
        /// Applies all the added filters by reflection
        /// </summary>
        /// <returns></returns>
        protected List<TItem> ApplyFilters()
        {
            if (FilterLinkedList.First == null)
            {
                FilteredItems = Items.ToList();
                return FilteredItems;
            }
            var FilterList = FilterLinkedList.ToList();
            Type elementType = typeof(TItem);
            var stringProperties = elementType.GetProperties().Where(x => x.PropertyType == typeof(string)).ToList();
            Func<TItem, bool> expression = (item) =>
            {
                bool validation = true;
                foreach (var filter in FilterList)
                {
                    var currentType = item.GetType();
                    var property = currentType.GetProperty(filter.Source);
                    var filterNode = FilterLinkedList.Find(filter);
                    bool filterValidation = true;
                    ConnectorType connectorType = filterNode.Previous != null ? filterNode.Previous.Value.ConnectorType.Value : ConnectorType.None;
                    if (filter.Source.ToLower() == "all")
                    {
                        var macthes = stringProperties.Where(x => ApplyFilterType(x.GetValue(item, null), filter.Value, filter.FilterType.Value, filter.MatchCase)).ToList();
                        filterValidation = macthes.Any();
                    }
                    else
                    {
                        filterValidation = ApplyFilterType(property.GetValue(item, null), filter.Value, filter.FilterType.Value, filter.MatchCase);
                    }
                    if (FilterList.IndexOf(filter) == 0)
                    {
                        validation = filterValidation;
                    }
                    else if (connectorType == ConnectorType.And)
                    {
                        validation = validation && filterValidation;
                    }
                    else if (connectorType == ConnectorType.Or)
                    {
                        validation = validation || filterValidation;
                    }

                }
                return validation;
            };
            FilteredItems = Items.Where(expression).ToList();

            return FilteredItems;
        }

        void OrderByColumn(Column? column)
        {
            if (column == null)
            {
                return;
            }
            Columns.ForEach(x =>
            {
                x.Focused = false;
                x.OrderDesc = x.Equals(column) ? column.OrderDesc : false;
            });
            column.Focused = true;
            FilteredItems = !column.OrderDesc ? FilteredItems.AsQueryable().OrderBy(column.PropertyName).ToList() : FilteredItems.AsQueryable().OrderByDescending(column.PropertyName).ToList();
            column.OrderDesc = !column.OrderDesc;
        }
    }
}