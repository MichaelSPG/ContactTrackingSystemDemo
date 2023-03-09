namespace ContactTrackingSystem.Shared.Interfaces
{
    /// <summary>
    /// DataBase Entity base
    /// </summary>
    public interface IBdEntity
    {
        Guid? Id { get; set; }
        IBdEntity? Copy(IBdEntity? other);
    }    
}
