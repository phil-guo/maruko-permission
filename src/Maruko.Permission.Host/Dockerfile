FROM microsoft/dotnet:2.2-aspnetcore-runtime
#FROM wl/aspnetcoreruntime


### 时区设置，默认是【格林威治时间】
#ENV TZ=Asia/Shanghai
#RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone
##

WORKDIR /app
EXPOSE 39001
COPY . .
#RUN apt-get update && apt-get install -y --no-install-recommends libgdiplus libc6-dev && rm -rf /var/lib/apt/lists/* 
ENTRYPOINT ["dotnet", "Maruko.Permission.Host.dll"]