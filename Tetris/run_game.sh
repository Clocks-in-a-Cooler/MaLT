echo "building..."

dotnet msbuild Tetris.sln

echo "done building. running..."

./bin/Debug/netcoreapp3.1/Tetris

echo "exit."