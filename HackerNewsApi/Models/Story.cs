namespace HackerNewsApi.Models
{
    public class Story
    {
        public string By { get; set; } = string.Empty;
        public int Descendants { get; set; }
        public int Id { get; set; }
        public List<int> Kids { get; set; } = new List<int>();
        public int Score { get; set; }
        public int Time { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
    }
}
