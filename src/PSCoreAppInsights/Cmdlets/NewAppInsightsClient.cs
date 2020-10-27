using System;
using System.Collections.Generic;
using System.Text;
using System.Management.Automation;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;

namespace PSCoreAppInsights.Cmdlets
{
    [Cmdlet(VerbsCommon.New, "AppInsightsClient")]
    [OutputType(typeof(TelemetryClient))]
    public class NewAppInsightsClient : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public string InstrumentationKey { get; set; }

        protected override void ProcessRecord()
        {
            WriteVerbose("Creating App Insights client");
            TelemetryConfiguration clientConfiguration = new TelemetryConfiguration(InstrumentationKey);
            WriteObject(new TelemetryClient(clientConfiguration));
        }
    }
}
