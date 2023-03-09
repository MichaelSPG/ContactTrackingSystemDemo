using System.Diagnostics.Metrics;
using System.Reflection.PortableExecutable;
using System.Reflection;
using System;
using ContactTrackingSystem.Shared.Model;

namespace ContactTrackingSystem.Shared.Data
{
    public class ApiUtils
    {
        /// <summary>
        /// For this demo we are using in-memory data through static classes 
        /// </summary>
        public const int PhoneCountryCode = 1;
        public static List<Candidate> DefaultCandidates = new List<Candidate>
        {
            new Candidate { FirstName = "Ifan", LastName= "Bender",  Id = Guid.NewGuid(), Email = "IfanBen123@gmail.com", ZipCode = "04662", PhoneNumber = "858-695-0341" },
            new Candidate { FirstName = "Myah", LastName= "Webster",  Id = Guid.NewGuid(), Email = "MyahWebs123@gmail.com", ZipCode = "14738", PhoneNumber = "209-628-6790" },
            new Candidate { FirstName = "Harold", LastName= "Evans",  Id = Guid.NewGuid(), Email = "HaroldEv212@gmail.com", ZipCode = "68023", PhoneNumber = "909-433-0106" },
            new Candidate { FirstName = "Louis", LastName= "Haas",  Id = Guid.NewGuid(), Email = "LouisHs34@gmail.com", ZipCode = "17563", PhoneNumber = "574-325-9021" },
            new Candidate { FirstName = "Rosanna", LastName= "Chan",  Id = Guid.NewGuid(), Email = "Rosanna23C1@gmail.com", ZipCode = "92704", PhoneNumber = "323-772-9223" },
            new Candidate { FirstName = "Neo", LastName= "Livingston",  Id = Guid.NewGuid(), Email = "Livingston333@gmail.com", ZipCode = "48637", PhoneNumber = "646-637-8787" },
            new Candidate { FirstName = "Zoya", LastName= "Jensen",  Id = Guid.NewGuid(), Email = "Jensen324@gmail.com", ZipCode = "84003", PhoneNumber = "801-495-3528" },
            new Candidate { FirstName = "Leslie", LastName= "Velez",  Id = Guid.NewGuid(), Email = "LeslieV12@gmail.com", ZipCode = "28119", PhoneNumber = "720-862-1980" },
            new Candidate { FirstName = "Abbas", LastName= "Castro",  Id = Guid.NewGuid(), Email = "AbbasCt22@gmail.com", ZipCode = "05488", PhoneNumber = "323-383-5785" },
            new Candidate { FirstName = "Lara", LastName= "Weaver",  Id = Guid.NewGuid(), Email = "LaraWev21@gmail.com", ZipCode = "30022", PhoneNumber = "417-839-0402" },
            new Candidate { FirstName = "Jaydon", LastName= "Willis",  Id = Guid.NewGuid(), Email = "Willis112Jaydon@gmail.com", ZipCode = "16679", PhoneNumber = "662-496-6898" },
            new Candidate { FirstName = "Macie", LastName= "Hansen",  Id = Guid.NewGuid(), Email = "Hansen213Mac@gmail.com", ZipCode = "75146", PhoneNumber = "802-825-7654" },
            new Candidate { FirstName = "Whitney", LastName= "House",  Id = Guid.NewGuid(), Email = "House54Whi@gmail.com", ZipCode = "75446", PhoneNumber = "906-753-9204" },
            new Candidate { FirstName = "Yasmine", LastName= "Stuart",  Id = Guid.NewGuid(), Email = "Yasmine123GSt@gmail.com", ZipCode = "96034", PhoneNumber = "315-523-3693" },
            new Candidate { FirstName = "Sylvie", LastName= "Hunter",  Id = Guid.NewGuid(), Email = "Sylvie132H@gmail.com", ZipCode = "45432", PhoneNumber = "610-331-0934" },
        };
    }
}
