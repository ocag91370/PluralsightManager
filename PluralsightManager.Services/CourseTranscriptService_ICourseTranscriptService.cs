﻿using DecryptPluralSightVideos.Encryption;
using PluralsightManager.Models;
using PluralsightManager.Models.Models;
using PluralsightManager.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Threading.Tasks;

namespace PluralsightManager.Services
{
    public partial class CourseTranscriptService : ICourseTranscriptService
    {
        public bool Download(CourseModel course, List<FolderModel> folders)
        {
            if (!course.HasTranscript)
                return false;

            var orderedModules = course.Modules.OrderBy(m => m.Index);
            Parallel.ForEach(orderedModules, module => Download(module, folders.Where(folder => folder.ModuleName == module.Name).ToList()));

            return true;
        }

        public bool Download(ModuleModel module, List<FolderModel> folders)
        {
            var orderedClips = module.Clips.OrderBy(c => c.Index);
            Parallel.ForEach(orderedClips, clip => Download(clip, folders.FirstOrDefault(folder => folder.ClipName == clip.Name)));

            return true;
        }

        public bool Download(ClipModel clip, FolderModel folder)
        {
            var orderedTrancripts = clip.Transcripts.OrderBy(t => t.StartTime).ToList();
            Download(orderedTrancripts, folder);

            return true;
        }

        public bool Download(List<TranscriptModel> transcripts, FolderModel folder)
        {
            var outputFile = Path.Combine(folder.Output.Path, $"{folder.Output.Filename}{ _configuration.TranscriptFileExtension}");

            if (! transcripts.Any())
            {
                _consoleService.Log(LogType.Warning, $"Transcript file '{outputFile}' ie empty");
                return false;
            }

            _consoleService.Log(LogType.Begin, $"Start to download transcript file '{outputFile}'");

            try
            {
                var status = ExtractAndSave(transcripts, outputFile);

                _consoleService.Log(LogType.End, $"Transcript file '{outputFile}' downloaded successfully");

                return status;
            }
            catch
            {
                _consoleService.Log(LogType.End, $"Transcript file '{outputFile}' download failed");
                return false;
            }
        }
    }
}
