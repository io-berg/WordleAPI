namespace WordleAPI
{
    public class WordleData
    {
        public List<char>? rightLetters { get; set; }
        public List<char>? wrongLetters { get; set; }
        public Dictionary<int, char>? rightPos { get; set; }
    }
}