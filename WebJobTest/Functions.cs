using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Utilities;

namespace WebJobTest
{
    public static class Functions
    {
        public static async void ProcessMessage([QueueTrigger(JobQueues.RawIncoming)] string jobDescription)
        {
            Console.WriteLine("ProcessMessageFired");
            var job = Newtonsoft.Json.JsonConvert.DeserializeObject<JobDescription>(jobDescription);
            Console.WriteLine("Incoming Job: {0}, recipient: {1}, time: {2}", job.PullRequestURL, job.RequestingEmail, job.RequestTime);

            var queue = Utils.GetQueue(JobQueues.ProcessedIncoming);
            foreach (var repo in Utils.RepoList)
            {
                var jobPart = new JobPart(job.PullRequestURL, job.RequestingEmail, job.JobID, repo);
                var msg = new CloudQueueMessage(Newtonsoft.Json.JsonConvert.SerializeObject(jobPart));
                Console.WriteLine("Posting JobPart: {0}, recipient: {1}, time: {2}, target: {3}", jobPart.PullRequestURL, jobPart.RequestingEmail, jobPart.RequestTime, jobPart.TargetRepoURL);
                await queue.AddMessageAsync(msg);
            }            
        }
    }
}
