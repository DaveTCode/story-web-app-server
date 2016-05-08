using System;

namespace StoryWebApp.Models
{
    public class Section
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Snippet { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}