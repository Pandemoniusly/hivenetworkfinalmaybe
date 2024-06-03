cd ../
dotnet new tool-manifest
dotnet tool install --local evaisa.netcodepatcher.cli --version 4.20
cd Project2
start /b del "install-netcode-patcher.cmd"
