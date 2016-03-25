using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.WindowsAzure.Storage.Queue;
using Utilities;

namespace WorkerRoleProxy
{
    public class Functions
    {
        public static async void ProcessQueueMessage([QueueTrigger(JobQueues.ProcessedIncoming)] string jobDescription)
        {
            var processedIncomingQueue = Utils.GetQueue(JobQueues.ProcessedIncoming);
            Console.WriteLine("ProcessMessageFired");
            var incomingJob = Newtonsoft.Json.JsonConvert.DeserializeObject<JobPart>(jobDescription);
            Console.WriteLine("Incoming Job: {0}, recipient: {1}, targetRepo: {2}, time: {3}", incomingJob.PullRequestURL, incomingJob.RequestingEmail, incomingJob.TargetRepoURL, incomingJob.RequestTime);

            var msg = await processedIncomingQueue.PeekMessageAsync();
            if (msg != null)
            {
                var processedJobPartMessage = (await processedIncomingQueue.GetMessageAsync()).AsString;
                var processedJobPart = Newtonsoft.Json.JsonConvert.DeserializeObject<JobPart>(processedJobPartMessage);
                Console.WriteLine("Completed Job: {0}, recipient: {1}, targetRepo: {2}, time: {3}", processedJobPart.PullRequestURL, processedJobPart.RequestingEmail, processedJobPart.TargetRepoURL, processedJobPart.RequestTime);

                // This should work but there's nothing dequeuing items here yet so we won't bother to fill it up
                //var outgoingQueue = Utils.GetQueue(JobQueues.Outgoing);
                //var completedJob = new JobPart(processedJobPart.PullRequestURL, processedJobPart.RequestingEmail, processedJobPart.ParentJobID, processedJobPart.TargetRepoURL);
                //var completedJobPartMessage = new CloudQueueMessage(Newtonsoft.Json.JsonConvert.SerializeObject(completedJob));
                //await outgoingQueue.AddMessageAsync(completedJobPartMessage);
            }            
        }
    }
}
