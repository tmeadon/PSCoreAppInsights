using System;
using System.Collections.Generic;
using System.Text;
using System.Management.Automation;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using System.Collections;
using System.Linq;

namespace PSCoreAppInsights.Cmdlets
{
    [Cmdlet(VerbsCommunications.Send, "AppInsightsTrace")]
    //[OutputType(typeof(TelemetryClient))]
    public class SendAppInsightsTrace : PSCmdlet
    {
        /// <summary>
        /// <para type="description">A TelemetryClient instance (produced by running New-AppInsightsClient</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        public TelemetryClient AppInsightsClient { get; set; }

        /// <summary>
        /// <para type="description">The message to send in the trace</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        public string Message { get; set; }

        /// <summary>
        /// <para type="description">The severity to associate with the message</para>
        /// </summary>
        [Parameter()]
        public SeverityLevel SeverityLevel { get; set; } = SeverityLevel.Information;

        /// <summary>
        /// <para type="description">A hashtable containing additional properties to attach to the message</para>
        /// </summary>
        [Parameter()]
        public Hashtable AdditionalProperties { get; set; } = null;

        protected override void ProcessRecord()
        {
            // convert the properties hashtable to a dictionary if they were supplied
            if (null != AdditionalProperties)
            {
                Dictionary<string, string> propertiesDict = AdditionalProperties.Cast<DictionaryEntry>().ToDictionary(d => d.Key.ToString(), d => d.Value.ToString());

                WriteVerbose(string.Format("Sending App Insights trace with message {0}, severity level {1} and extra properties {2}",
                    Message,
                    SeverityLevel.ToString(),
                    string.Join(", ", propertiesDict.Keys)));
               
                AppInsightsClient.TrackTrace(Message, SeverityLevel, propertiesDict);
            }
            else
            {
                WriteVerbose(string.Format("Sending App Insights trace with message {0} and severity level {1} and no extra properties",
                    Message,
                    SeverityLevel.ToString()));

                AppInsightsClient.TrackTrace(Message, SeverityLevel);
            }
            
        }
    }
}
