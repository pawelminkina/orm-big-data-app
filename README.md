# orm-big-data-app

It's an application required for university, which combines ORM and big data funcionality, in this case it was elasticsearch.

## How to setup elasticsearch

1. You need docker desktop
2. Execute commands

```
docker network create elastic
docker pull docker.elastic.co/elasticsearch/elasticsearch:8.12.0
docker run --name elasticsearch --net elastic -p 9200:9200 -p 9300:9300 -e "discovery.type=single-node" -t docker.elastic.co/elasticsearch/elasticsearch:8.12.0
```

3. copy generated password
4. Open docker desktop cli with elastic search and execute command. Copy retrieved fingerprint

```
openssl x509 -fingerprint -sha256 -in config/certs/http_ca.crt
```

5. Open appsettings.json and configure ElasticSearch section values with retrieved items
   {
   "Url": "https://localhost:9200",
   "DefaultIndex": "big-data-orm-app",
   "CertFingerprint": "from-openssl-script", //example: EE:87:E1:19:03:D7:DC:09:1B:FD:7B:84:8D:68:3A:10:D4:9B:07:08:C3:5E:C2:FD:57:A7:DE:D3:B5:45:9E:E5
   "Password": "password from step 3"
   "UserName": "user from step 3, default elastic"
   }

6. If needed more information can be found here: https://www.elastic.co/guide/en/elasticsearch/reference/current/run-elasticsearch-locally.html

## How to setup application

1. You have to configure elasticsearch mentioned before
2. Pass you connection string to SQL Server database in section ConnectionStrings:DefaultConnection
3. Run update-database command in package manager console
