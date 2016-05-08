using System;
using System.Collections.Generic;

namespace StoryWebApp.Models
{
    public class Chapter
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public IList<Section> Sections { get; } = new List<Section>();
    }
}