namespace taskApi.DTO
{
    public class TaskDTO
    {

        public string title { get; set; }

        public string description { get; set; }

        public DateTime dateCreated { get; set; }

        public DateTime dateExpirated { get; set; }

        public Boolean completed { get; set; }
    }
}
