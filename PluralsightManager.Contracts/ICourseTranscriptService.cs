using PluralsightManager.Models.Models;
using System.Collections.Generic;

namespace PluralsightManager.Contracts
{
    public interface ICourseTranscriptService
    {
        bool Download(List<FolderModel> folders, List<TranscriptModel> transcripts);

        bool Download(FolderModel folder, List<TranscriptModel> transcripts);
    }
}