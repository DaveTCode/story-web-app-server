using System;
using System.Collections.Generic;

namespace StoryWebApp.Models
{
    public class Story
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Blurb { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public IList<Chapter> Chapters { get; } = new List<Chapter>();
    }
}