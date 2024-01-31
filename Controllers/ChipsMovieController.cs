using ChipsMovieLogz.Models;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace ChipsMovieLogz.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChipsMovieController : ControllerBase
    {
        private readonly ILogger<ChipsMovieController> _logger;

        public ChipsMovieController(ILogger<ChipsMovieController> logger)
        {
            _logger = logger;
        }
        [HttpGet("movies", Name = "GetMovie")]
        public IEnumerable<Models.Movie> Get(string? title, string? genre, DateTime releaseDate, string? about, int imdbRating, int motionPictureRating, int metaScore, string? director, string? writer, int runtime, string? topCast)
        {
            var returnData = new RetrieveMovies("DataSource");
            var syncedItemHistory = returnData.GetSyncedMovies(title, genre, releaseDate, about, imdbRating, motionPictureRating, metaScore, director, writer, runtime, topCast);
            return syncedItemHistory;
        }
        [HttpGet("series", Name = "GetSeries")]
        public IEnumerable<Models.Series> Get(string? name, string? genre, DateTime premierDate, string? about, int imdbRating, string? certification, int runtime, string? director, string? writer, string? topCast, int episodes)
        {
            var returnData = new RetrieveSeries("DataSource");
            var syncedItemHistory = returnData.GetSyncedSeries(name,genre, premierDate, about, imdbRating, certification, runtime, director, writer, topCast, episodes);
            return syncedItemHistory;

        }
        [HttpGet ("actors", Name = "GetActors")]
        public IEnumerable<Models.Actor> Get(string? firstName, string? lastName, int credits, string? placeOfBirth, DateTime dateOfBirth, string? gender, int age, string? biography, string? featuresIn, string? productionCredit, string? crewCredit, string? writingCredit)
        {
            var returnData = new RetrieveActors("DataSource");
            var syncedItemHistory = returnData.GetSyncedActors(firstName, lastName, credits, placeOfBirth, dateOfBirth, gender, age, biography, featuresIn, productionCredit, crewCredit, writingCredit);     
            return syncedItemHistory;

        }





    }

}