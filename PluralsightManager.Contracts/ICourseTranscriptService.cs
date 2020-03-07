using PluralsightManager.Models.Models;
using System.Collections.Generic;

namespace PluralsightManager.Contracts
{
    public interface ICourseTranscriptService
    {
        bool Download(CourseModel course, List<FolderModel> folders);

        bool Download(ClipModel clip, FolderModel folder);

        bool Download(List<TranscriptModel> transcripts, FolderModel folder);
    }
}