using System;
using System.Management.Automation;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;

namespace PSCoreAppInsights.Cmdlets
{
    [Cmdlet(VerbsCommunications.Send, "AppInsightsMetric")]
    public class SendAppInsightsMetric : PSCmdlet
    {
        /// <summary>
        /// <para type="description">A TelemetryClient instance (produced by running New-AppInsightsClient)</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        public TelemetryClient AppInsightsClient { get; set; }

        /// <summary>
        /// <para type="description">Name of the metric to track</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        public string MetricName { get; set; }

        /// <summary>
        /// <para type="description">The value to track for the metric</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        public double MetricValue { get; set; }
        
        protected override void ProcessRecord()
        {
            AppInsightsClient.GetMetric(MetricName).TrackValue(MetricValue);
        }
    }

}