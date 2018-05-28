.\packages\OpenCover.4.6.519\tools\OpenCover.Console.exe -target:.\packages\NUnit.ConsoleRunner.3.8.0\tools\nunit3-console.exe -targetargs:"/domain:single .\Jal.Factory.Tests\bin\Debug\Jal.Factory.Tests.dll" -filter:"+[Jal.*]* -[*.Tests]*" -register:user -output:coverage.xml

.\packages\coveralls.io.1.4.2\tools\coveralls.net.exe --opencover .\coverage.xml