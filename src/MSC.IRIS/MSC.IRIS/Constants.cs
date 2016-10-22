using System;
namespace MSC.IRIS
{
	public static class Constants
	{
		//android
		public const string SenderID = "<GoogleProjectNumber>"; // Google API Project Number
		public const string ListenConnectionString = "<Listen connection string>";
		public const string NotificationHubName = "<hub name>";

		//ios
		public const string ConnectionString = "Endpoint=sb://missingchildren.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=6w1EEBnt1MfFB2oRHd7fG2V8cnw/sdeZQ9lDxSUqhZk=";
		public const string NotificationHubPath = "MissingChildrenHub";
	}
}
