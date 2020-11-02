# PSCoreAppInsights
A multi-platform PowerShell module for using Azure Application Insights.

![CI Status](https://github.com/tommagumma/PSCoreAppInsights/workflows/PSCoreAppInsights/badge.svg)

This is in a very early state - proper docs, cmdlet help, tests and other features will be on their way.

## Installation

```PowerShell
Install-Module -Name PSCoreAppInsights
```

## Usage
To use this module to send telemetry to Application Insights, you first need to create a client using the Instrumentation Key from your account:

```PowerShell
$client = New-AppInsightsClient -InstrumentationKey 'xyz'
```

The `$client` must then be passed into each of the cmdlets using the `AppInsightsClient` parameter.

### Available Cmdlets

```PowerShell
PS > Get-Command -Module PSCoreAppInsights | Select-Object -Property Name

Name
----
New-AppInsightsClient
Send-AppInsightsDependency
Send-AppInsightsEvent
Send-AppInsightsException
Send-AppInsightsTrace
```



