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
        public IEnumerable<Models.Movie> GetMovies(string? title, string? genre)
        {
            var returnData = new RetrieveMovies("DataSource");
            var movieshistory = returnData.GetSyncedMovies(title, genre);
            return movieshistory;
        }
        [HttpGet("series", Name = "GetSeries")]
        public IEnumerable<Models.Series> GetSeries(string title, string genre)
        {
            var returnData = new RetrieveSeries("DataSource");
            var syncedItemHistory = returnData.GetSyncedSeries(title,genre);
            return syncedItemHistory;

        }
        [HttpGet ("actors", Name = "GetActors")]
        public IEnumerable<Models.Actor> GetActors(string? firstName, string? lastName)
        {
            var returnData = new RetrieveActors("DataSource");
            var syncedItemHistory = returnData.GetSyncedActors(firstName, lastName);     
            return syncedItemHistory;

        }





    }

}