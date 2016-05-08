using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using StoryWebApp.Models;

namespace StoryWebApp.DBLayer
{
    internal class DBLayer
    {
        private readonly CommonDbLayer _dbLayer =
            new CommonDbLayer(ConfigurationManager.ConnectionStrings["StoryBook"].ConnectionString);

        internal async Task<IEnumerable<Story>> GetStories()
        {
            return await _dbLayer.AsyncDbQuery(
                "dbo.GetStories",
                new List<SqlParameter>(),
                reader =>
                {
                    var stories = new Dictionary<int, Story>();
                    var chapters = new Dictionary<int, Chapter>();
                    var sections = new Dictionary<int, Section>();

                    while (reader.Read())
                    {
                        var storyId = reader.GetValueOrDefault<int>("StoryId");
                        if (!stories.ContainsKey(storyId))
                        {
                            stories.Add(storyId, new Story
                            {
                                Id = storyId,
                                Name = reader.GetValueOrDefault<string>("StoryName"),
                                Blurb = reader.GetValueOrDefault<string>("StoryBlurb"),
                                DateCreated = reader.GetValueOrDefault<DateTime>("StoryDateCreated"),
                                DateModified = reader.GetValueOrDefault<DateTime>("StoryDateModified")
                            });
                        }
                        var story = stories[storyId];

                        var chapterId = reader.GetValueOrDefault<int?>("ChapterId");
                        if (!chapterId.HasValue) continue;
                        if (!chapters.ContainsKey(chapterId.Value))
                        {
                            chapters.Add(chapterId.Value, new Chapter
                            {
                                Id = chapterId.Value,
                                Name = reader.GetValueOrDefault<string>("ChapterName"),
                                DateCreated = reader.GetValueOrDefault<DateTime>("ChapterDateCreated"),
                                DateModified = reader.GetValueOrDefault<DateTime>("ChapterDateModified"),
                            });
                        }
                        var chapter = chapters[chapterId.Value];
                        story.Chapters.Add(chapter);

                        var sectionId = reader.GetValueOrDefault<int?>("SectionId");
                        if (!sectionId.HasValue) continue;
                        if (!sections.ContainsKey(sectionId.Value))
                        {
                            sections.Add(sectionId.Value, new Section
                            {
                                Id = sectionId.Value,
                                Name = reader.GetValueOrDefault<string>("SectionName"),
                                DateCreated = reader.GetValueOrDefault<DateTime>("SectionDateCreated"),
                                DateModified = reader.GetValueOrDefault<DateTime>("SectionDateModified")
                            });
                        }
                        chapter.Sections.Add(sections[sectionId.Value]);
                    }

                    return stories.Values;
                });
        }
    }
}