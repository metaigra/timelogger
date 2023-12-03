@echo off

echo Start server...
cd server
dotnet build
cd Timelogger.Api
start dotnet run
cd ../..

echo Start client...
cd client
start npm run start
cd ..

start http://localhost:3001/swagger/index.html
start http://localhost:3000