using ChipsMovieLogz.Models;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using Microsoft.AspNetCore.Cors;

namespace ChipsMovieLogz.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("MyPolicy")]
    public class ChipsMovieController : ControllerBase
    {
        private readonly ILogger<ChipsMovieController> _logger;

        public ChipsMovieController(ILogger<ChipsMovieController> logger)
        {
            _logger = logger;
        }
        string dataSource = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Chigo\OneDrive\Documents\Chigoziri DB.accdb;Persist Security Info=False;";
        [HttpGet("movies", Name = "GetMovie")]
        public IEnumerable<Models.Movie> GetMovies(string? title, string? genre)
        {
            var returnData = new RetrieveMovies(dataSource);
            var movieshistory = returnData.GetSyncedMovies(title, genre);
            return movieshistory;
        }
        [HttpGet("series", Name = "GetSeries")]
        public IEnumerable<Models.Series> GetSeries(string title, string genre)
        {
            var returnData = new RetrieveSeries(dataSource);
            var syncedItemHistory = returnData.GetSyncedSeries(title,genre);
            return syncedItemHistory;

        }
        [HttpGet ("actors", Name = "GetActors")]
        public IEnumerable<Models.Actor> GetActors(string? firstName, string? lastName)
        {
            var returnData = new RetrieveActors(dataSource);
            var syncedItemHistory = returnData.GetSyncedActors(firstName, lastName);     
            return syncedItemHistory;

        }





    }

}