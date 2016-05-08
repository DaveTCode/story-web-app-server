using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using StoryWebApp.Models;

namespace StoryWebApp.Controllers
{
    [RoutePrefix("stories")]
    public class StoriesController : ApiController
    {
        private readonly DBLayer.DBLayer _dbLayer = new DBLayer.DBLayer();

        /// <summary>
        /// This route retrieves the full set of stories as seen by the client.
        /// </summary>
        /// 
        /// <returns>A Task that resolves to the list of stories.</returns>
        [HttpGet, Route("")]
        public async Task<IEnumerable<Story>> GetStories()
        {
            return await _dbLayer.GetStories();
        }
    }
}
