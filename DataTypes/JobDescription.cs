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
        public string PullRequestURL;
        public string RequestingEmail;
        public DateTime RequestTime;
    }
}
