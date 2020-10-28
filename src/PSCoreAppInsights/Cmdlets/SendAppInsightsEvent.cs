using System;
using System.Collections.Generic;
using System.Text;
using System.Management.Automation;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using System.Collections;
using System.Linq;

namespace PSCoreAppInsights.Cmdlets
{
    [Cmdlet(VerbsCommunications.Send, "AppInsightsEvent")]
    //[OutputType(typeof(TelemetryClient))]
    public class SendAppInsightsEvent : PSCmdlet
    {
        /// <summary>
        /// <para type="description">A TelemetryClient instance (produced by running New-AppInsightsClient</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        public TelemetryClient AppInsightsClient { get; set; }

        /// <summary>
        /// <para type="description">The name of the event to send to App Insights</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        public string EventName { get; set; }

        /// <summary>
        /// <para type="description">A hashtable containing optional properties to attach to the event</para>
        /// </summary>
        [Parameter()]
        public Hashtable AdditionalProperties { get; set; }

        protected override void ProcessRecord()
        {
            if (null != AdditionalProperties)
            {
                Dictionary<string, string> propertiesDict = AdditionalProperties.Cast<DictionaryEntry>().ToDictionary(d => d.Key.ToString(), d => d.Value.ToString());
                AppInsightsClient.TrackEvent(EventName, propertiesDict);
            }
            else
            {
                AppInsightsClient.TrackEvent(EventName);
            }
        }
    }
}
