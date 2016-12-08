.\packages\OpenCover.4.6.519\tools\OpenCover.Console.exe -target:.\packages\NUnit.Runners.2.6.4\tools\nunit-console.exe -targetargs:"/nologo /noshadow .\Jal.Factory.Tests\bin\Debug\Jal.Factory.Tests.dll" -filter:"+[Jal.*]* -[*.Tests]*" -register:user

.\packages\coveralls.net.0.7.0\tools\csmacnz.Coveralls.exe --opencover -i .\results.xml 