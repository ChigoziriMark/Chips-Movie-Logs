namespace ChipsMovieLogz.Models
{
    public class Movie
    {
        private string _title = string.Empty;
        public string Title
        {
            get { return _title; }
            set { _title = value ?? string.Empty; }
        }

        private string _genre = string.Empty;
        public string Genre
        {
            get { return _genre; }
            set { _genre = value ?? string.Empty; }
        }

        public DateTime ReleaseDate { get; set; }

        private string _about = string.Empty;
        public string About
        {
            get { return _about; }
            set { _about = value ?? string.Empty; }
        }

        public int IMDBRating { get; set; }
        public int MotionPictureRating { get; set; }
        public int MetaScore { get; set; }

        private string _director = string.Empty;
        public string Director
        {
            get { return _director; }
            set { _director = value ?? string.Empty; }
        }

        private string _writer = string.Empty;
        public string Writer
        {
            get { return _writer; }
            set { _writer = value ?? string.Empty; }
        }

        public int Runtime { get; set; }

        private string _topCast = string.Empty;
        public string TopCast
        {
            get { return _topCast; }
            set { _topCast = value ?? string.Empty; }
        }
    }
}
