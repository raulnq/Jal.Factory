packages\NuGet.CommandLine.2.8.6\tools\nuget pack Jal.Factory\Jal.Factory.csproj -Properties "Configuration=Release;Platform=AnyCPU;OutputPath=bin\Release" -Build -IncludeReferencedProjects -OutputDirectory Jal.Factory.Nuget

packages\NuGet.CommandLine.2.8.6\tools\nuget pack Jal.Factory.Installer\Jal.Factory.Installer.csproj -Properties "Configuration=Release;Platform=AnyCPU;OutputPath=bin\Release" -Build -IncludeReferencedProjects -OutputDirectory Jal.Factory.Nuget

pause;