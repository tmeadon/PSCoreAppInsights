param
(
    # Version number to stamp module manifest with
    [Parameter()]
    [version]
    $NewVersionNumber,

    # Key for the PowerShell gallery
    [Parameter()]
    [string]
    $PsGalleryKey
)

task buildOnly CleanModule, BuildModule, CopyFiles
task buildAndPublish

$BuildRoot = (Get-Item -Path $BuildRoot).Parent.FullName
$buildOutputPath = "$BuildRoot\build\output"

task CleanModule {
    if (Test-Path -Path $buildOutputPath -PathType Container)
    {
        Remove-Item -Path $buildOutputPath -Recurse
    }
    dotnet.exe clean "$BuildRoot\src\PSCoreAppInsights\PSCoreAppInsights.sln" -c release
}

task BuildModule {
    dotnet.exe publish "$BuildRoot\src\PSCoreAppInsights\PSCoreAppInsights.sln" -c release
}

task CopyFiles {
    New-Item -ItemType Directory -Path $buildOutputPath -ErrorAction SilentlyContinue
    Copy-Item -Path "$BuildRoot\src\PSCoreAppInsights.ps*" -Destination $buildOutputPath
    Copy-Item -Path "$BuildRoot\src\PSCoreAppInsights\bin\Release\netstandard2.0\publish" -Destination "$buildOutputPath\bin" -Recurse
}

task UpdateVersion {
    if ($NewVersionNumber)
    {
        Update-ModuleManifest -Path "$BuildRoot\PSCoreAppInsights.psd1" -ModuleVersion $NewVersionNumber
    }
}

task PublishToGallery {
    if (-not $PsGalleryKey)
    {
        throw "You must supply a value for the the -PSGalleryKey in order to run this task"
    }
    Publish-Module -Path $buildOutputPath -NuGetApiKey $PsGalleryKey -Repository 'PSGallery'
}