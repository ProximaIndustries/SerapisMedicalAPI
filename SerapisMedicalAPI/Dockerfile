FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app

# Copy everything else 
COPY . .

# Build runtime image
USER 5000 
ENTRYPOINT ["dotnet", "SerapisMedicalAPI.dll"]





