@echo off

echo Server init...
cd server
dotnet restore
dotnet build

echo Client init...
cd client
npm install
cd ..
