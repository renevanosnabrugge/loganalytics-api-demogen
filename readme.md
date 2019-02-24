# Description
This container generates dummy data of some builds. By using the container from the Loganalytics-API repository, you can use this one to add CustomLogs to Log Analytics that you can play with

Start this container first following the instruction here
https://github.com/renevanosnabrugge/azure-loganalytics-api


# Build image
```
docker build -t repo/containername .
```

# Run Image
Add the url of the running Loganalytics-api container as api-url and the SharedAccessKey as api-key

```
docker run -d -e "api-url=https://url.com:5900" -e "api-key=RuggedDemo" repo/containername
```

# Run with Log Analytics image on host 

```
docker run -d -e "api-url=https://10.x.x.x:5900" -e "api-key=RuggedDemo" repo/containername
```

# Look in Log Analytics queries and execute

```
BuildCompliance_CL
| summarize count() by BuildDefinitionName_s
```
