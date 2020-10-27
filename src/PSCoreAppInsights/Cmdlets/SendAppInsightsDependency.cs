using System;
using System.Collections.Generic;
using System.Text;
using System.Management.Automation;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Metrics;
using System.Collections;
using System.Linq;
using System.Transactions;

namespace PSCoreAppInsights.Cmdlets
{
    [Cmdlet(VerbsCommunications.Send, "AppInsightsDependency")]
    //[OutputType(typeof(TelemetryClient))]
    public class SendAppInsightsDependency : PSCmdlet
    {
        /// <summary>
        /// <para type="description">A TelemetryClient instance (produced by running New-AppInsightsClient</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        public TelemetryClient AppInsightsClient { get; set; }

        /// <summary>
        /// <para type="description">A name for the dependency</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        public string Name { get; set; }

        /// <summary>
        /// <para type="description">A TimeSpan representing the amount of time a dependency took</para>
        /// </summary>
        [Parameter()]
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// <para type="description">The hostname of the dependency</para>
        /// </summary>
        [Parameter()]
        public string Hostname { get; set; }

        /// <summary>
        /// <para type="description">Data associated with the current dependency instance e.g. command name/statement for SQL dependency, URL for http dependency</para>
        /// </summary>
        [Parameter()]
        public string Data { get; set; }

        protected override void ProcessRecord()
        {
            WriteVerbose(string.Format("Sending dependency {0} with target {1}", Name, Hostname));

            DependencyTelemetry dependency = new DependencyTelemetry
            {
                Name = Name,
                Duration = Duration,
                Target = Hostname,
                Data = Data
            };

            AppInsightsClient.TrackDependency(dependency);
        }
    }
}
