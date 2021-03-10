$path = "bin/Debug/net5.0/OsFunctions.dll"

try
{
    Add-Type -Path $path
}
catch [System.Reflection.ReflectionTypeLoadException]
{
    Write-Host "Message: $($_.Exception.Message)"
    Write-Host "StackTrace: $($_.Exception.StackTrace)"
    Write-Host "LoaderExceptions: $($_.Exception.LoaderExceptions)"
}


$OsUtil = New-Object ClassLibrary3.OsUtil
$OsUtil.GetInfoAboutSystem();

$EventLogInfo = New-Object ClassLibrary3.EventLogInfo
$EventLogInfo.GetInfo("");