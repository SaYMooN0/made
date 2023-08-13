﻿using MadeLib.Src.ProjectClasses;
using Newtonsoft.Json;

namespace MadeLib.Src
{
    public class ProjectManager
    {
        private const string FileName = "links.madeLinks";

        [JsonProperty("_projectLinks")]
        private List<string> _projectLinks;

        [JsonProperty("_pinnedProjectLinks")]
        private List<string> _pinnedProjectLinks;

        [JsonIgnore]
        public List<MadeProject> Projects { get; private set; } = new();

        [JsonIgnore]
        public List<MadeProject> PinnedProjects { get; private set; } = new();

        
        public void SaveToFile()
        {
            string jsonInstance = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(FileName, jsonInstance);
            if (Projects != null && Projects.Count > 0)
                foreach (var item in Projects) { item.SaveToFile(); }
        }
        static public ProjectManager Initialize()
        {
            if (!File.Exists(FileName))
                return new ProjectManager();
            else
            {
                string jsonInstance = File.ReadAllText(FileName);
                ProjectManager pm = JsonConvert.DeserializeObject<ProjectManager>(jsonInstance);
                return pm;
            }
        }
    }
}
