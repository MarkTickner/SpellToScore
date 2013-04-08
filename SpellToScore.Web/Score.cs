namespace SpellToScore.Web
{
    public class Score
    {
        private int position;
        public int Position
        {
            get { return position; }
        }

        private User user;
        public User User
        {
            get { return user; }
        }

        private int playerScore;
        public int PlayerScore
        {
            get { return playerScore; }
        }

        private string dateTime;
        public string DateTime
        {
            get { return dateTime; }
        }

        public Score(int position, User user, int playerScore, string dateTime)
        {
            this.position = position;
            this.user = user;
            this.playerScore = playerScore;
            this.dateTime = dateTime;
        }
    }
}