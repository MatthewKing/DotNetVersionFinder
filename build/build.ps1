$root = Resolve-Path (Join-Path $PSScriptRoot "..")
$project =  "$root/src/DotNetVersionFinder"
$output = "$root/artifacts"
dotnet pack $project --output $output --configuration "Release" -p:ContinuousIntegrationBuild=true
