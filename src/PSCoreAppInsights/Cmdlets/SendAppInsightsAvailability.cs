using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;


namespace PSCoreAppInsights.Cmdlets
{
    [Cmdlet(VerbsCommunications.Send, "AppInsightsAvailability")]
    //[OutputType(typeof(TelemetryClient))]
    public class SendAppInsightsAvailability : PSCmdlet
    {
        /// <summary>
        /// <para type="description">A TelemetryClient instance (produced by running New-AppInsightsClient)</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        public TelemetryClient AppInsightsClient { get; set; }

        /// <summary>
        /// <para type="description">The name of the test to send a result for</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        public string TestName { get; set; }

        /// <summary>
        /// <para type="description">The time of the test</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        public DateTimeOffset DateTime { get; set; }

        /// <summary>
        /// <para type="description">The duration of the test</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// <para type="description">The name of the location the test was executed from</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        public string TestRunLocation { get; set; }

        /// <summary>
        /// <para type="description">Whether the result was successful or not</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        public bool Success { get; set; }

        /// <summary>
        /// <para type="description">An optional message to include with the test</para>
        /// </summary>
        [Parameter()]
        public string Message { get; set; } = default;

        /// <summary>
        /// <para type="description">A hashtable containing additional properties to attach to the test result</para>
        /// </summary>
        [Parameter()]
        public Hashtable AdditionalProperties { get; set; } = null;

        protected override void ProcessRecord()
        {
            WriteVerbose(String.Format("Sending availability test result with name {0}, location {1}, time {2}, duration {3}, success {4} and message {5}",
                TestName, TestRunLocation, DateTime, Duration, Success, Message));

            if (AdditionalProperties != null)
            {
                Dictionary<string, string> propertiesDict = AdditionalProperties.Cast<DictionaryEntry>().ToDictionary(d => d.Key.ToString(), d => d.Value.ToString());
                AppInsightsClient.TrackAvailability(TestName, DateTime, Duration, TestRunLocation, Success, Message, propertiesDict);
            }
            else
            {
                AppInsightsClient.TrackAvailability(TestName, DateTime, Duration, TestRunLocation, Success, Message);
            }
        }
    }
}
