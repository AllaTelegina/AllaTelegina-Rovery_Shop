# ��������� �����(image) docker � ������������ �����;
FROM mcr.microsoft.com/dotnet/sdk:8.0

# ���������� ������� ����������, ������ ����� �������� �����(image);
WORKDIR /app

# ��������� ��� ����� ������� � docker;
COPY . /app

# �������� ������ � �������;
RUN dotnet restore /app/Backend_asp.net.csproj

# ������ ������ � ����������;
RUN dotnet publish -c Release -o out

#ENV ASPNETCORE_URLS=http://+:8080

# ������ ������� � ����������;
CMD ["dotnet", "watch", "run", "--project", "Backend_asp.net.csproj","--urls", "http://0.0.0.0:5000"]