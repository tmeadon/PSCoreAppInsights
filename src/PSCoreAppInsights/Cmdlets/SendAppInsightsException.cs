using System;
using System.Management.Automation;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;


namespace PSCoreAppInsights.Cmdlets
{
    [Cmdlet(VerbsCommunications.Send, "AppInsightsException")]
    //[OutputType(typeof(TelemetryClient))]
    public class SendAppInsightsException : PSCmdlet
    {
        /// <summary>
        /// <para type="description">A TelemetryClient instance (produced by running New-AppInsightsClient)</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        public TelemetryClient AppInsightsClient { get; set; }

        /// <summary>
        /// <para type="description">The exception to send to App Insights</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        public Exception Exception { get; set; }

        protected override void ProcessRecord()
        {
            ExceptionTelemetry exception = new ExceptionTelemetry(Exception);
            AppInsightsClient.TrackTrace(string.Format("ERROR: {0}", Exception.Message), SeverityLevel.Error);
            AppInsightsClient.TrackException(exception);
        }
    }
}
