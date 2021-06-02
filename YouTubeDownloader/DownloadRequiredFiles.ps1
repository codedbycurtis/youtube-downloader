# Declare variables
$url = "https://github.com/ffbinaries/ffbinaries-prebuilt/releases/download/v4.2.1/ffmpeg-4.2.1-win-64.zip"
$zipPath = "$PSScriptRoot\ffmpeg-4.2.1-win-64.zip"
$ffmpegPath = "$PSScriptRoot\ffmpeg.exe"

# Check if file exists
if (Test-Path "$ffmpegPath") {
    Write-Host "File already exists; exiting..."
    exit
}

# Create WebClient and download pre-built FFmpeg binaries to a zip archive, and then dispose of the client
Write-Host "Downloading FFmpeg..."
$webClient = New-Object System.Net.WebClient
$webClient.DownloadFile($url, $zipPath)
$webClient.Dispose()

# Extract only "ffmpeg.exe" from the zip archive, and then dispose of the client
Write-Host "Extracting..."
Add-Type -Assembly System.IO.Compression.FileSystem
$zip = [IO.Compression.ZipFile]::OpenRead($zipPath)
[IO.Compression.ZipFileExtensions]::ExtractToFile($zip.GetEntry("ffmpeg.exe"), "$ffmpegPath", $true)
$zip.Dispose()

# Remove unneccessary files & folders, and copy FFmpeg to the solution compilation folder
Write-Host "Cleaning up..."
Remove-Item $zipPath
Copy-Item "$ffmpegPath" "$PSScriptRoot\bin\Debug"

Pause