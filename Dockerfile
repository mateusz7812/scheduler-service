#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
#WORKDIR /app
#RUN git clone https://mateusz78122@dev.azure.com/mateusz78122/Scheduler/_git/SchedulerService
#WORKDIR /app/scheduler-service
#RUN dotnet restore
#RUN dotnet publish -c Debug -o out

#FROM mcr.microsoft.com/dotnet/aspnet:8.0
#WORKDIR /app
#COPY --from=build-env /app/scheduler-service/out .
#ENTRYPOINT ["dotnet", "SchedulerWebApplication.dll"]

FROM mcr.microsoft.com/dotnet/aspnet:8.0

COPY ["./build", "."]

ENTRYPOINT ["dotnet", "SchedulerWebApplication.dll"]