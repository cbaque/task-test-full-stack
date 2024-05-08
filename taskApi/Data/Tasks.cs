﻿namespace taskApi.Data
{
    public class Tasks
    {
        public int id { get; set; }

        public string title { get; set; }

        public string description { get; set; }

        public DateTime dateCreated { get; set; }

        public DateTime dateExpiration { get; set; }

        public Boolean completed { get; set; }
    }
}
