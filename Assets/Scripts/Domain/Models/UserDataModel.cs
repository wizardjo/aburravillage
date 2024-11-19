using System;

namespace Domain.Models
{
    public class UserDataModel
    {
        public string DisplayName { get; set; }
        public int Points { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public UserEconomy UserEconomy { get; set; }
    }

    public class UserEconomy
    {
        public int Wood { get; set; }
        public int Food { get; set; }
        public int Water { get; set; }
    }
}
