using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    [Serializable]
    public class JobDescription
    {
        public readonly int JobID;
        public readonly string PullRequestURL;
        public readonly string RequestingEmail;
        public readonly DateTime RequestTime;

        public JobDescription(string pullRequestUrl, string requestingEmail)
        {
            JobID = (new Random()).Next();
            PullRequestURL = pullRequestUrl;
            RequestingEmail = requestingEmail;
            RequestTime = DateTime.UtcNow;
        }
    }

    [Serializable]
    public class JobPart : JobDescription
    {
        public readonly int ParentJobID;
        public readonly string TargetRepoURL;

        public JobPart(string pullRequestUrl, string requestingEmail, int parentJobID, string targetRepoUrl)
            : base(pullRequestUrl, requestingEmail)
        {
            ParentJobID = parentJobID;
            TargetRepoURL = targetRepoUrl;
        }
    }
}
