Testing how Azure WebJobs SDK works

For local development you need:

WebQueueTest\Web.Secret.Config:
	<appSettings>
		<add key="StorageConnectionString" value="<fromAzurePortal>"/>
	</appSettings>

WebJobTest\App.Secret.Config:
	<appSettings>
		<add key="AzureWebJobsDashboard" value="<fromAzurePortal>" />  
		<add key="AzureWebJobsStorage" value="<fromAzurePortal>" />
	</appSettings>