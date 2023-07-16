Param(
    [Parameter(Mandatory=$true)]
    [String] $modName="",

    [Parameter(Mandatory=$true)]
    [String] $modVersion="",
    
    [Parameter(Mandatory=$true)]
    [String] $modWebsiteUrl="",

    [Parameter(Mandatory=$true)]
    [String] $modDescription="",

    [Parameter(Mandatory=$false)]
    [String[]] $dependencies=@(),

    [Parameter(Mandatory=$true)]
    [String] $outPath=""
)

$manifest = @{
    "name" = $modName
    "version_number" = $modVersion
    "website_url" = $modWebsiteUrl
    "description" = $modDescription
    "dependencies" = $dependencies
}

$json = $manifest | ConvertTo-Json
$json | Set-Content -Path "$outPath/manifest.json" -Force 
