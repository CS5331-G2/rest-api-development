FROM microsoft/aspnetcore-build:2.0
COPY . /app
WORKDIR /app

# compile project
RUN ["dotnet", "restore"]
RUN ["dotnet", "build"]

# expose ports
EXPOSE 80
EXPOSE 8080

# transfer control to bash
RUN chmod +x ./entrypoint.sh
CMD /bin/bash ./entrypoint.sh

