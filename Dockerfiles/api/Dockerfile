FROM microsoft/dotnet:2.2-sdk
COPY /Bafp.Contracts /app/Bafp.Contracts
COPY /Bafp.Logic /app/Bafp.Logic
COPY /Bafp.Web /app/Bafp.Web
COPY /Bafp.sln /app/Bafp.sln
WORKDIR /app
RUN ["dotnet", "restore"]
WORKDIR /app/Bafp.Web
RUN ["dotnet", "build"]
CMD ["dotnet", "run", "--urls", "http://*:5000", "--no-launch-profile"]