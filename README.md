# Message Processing System Task

## About the project

### The project is a asp .net core microservices project that consist of three different microservices that communicate with each other using API (GET) and using MSSQL and REDIS to cache and pass information.
### The whole project is running on docker compose in order to simplify things and run the whole services that used in the same network and make it easier to startup.

## Requirements

* [docker version 3.8 or above](https://www.docker.com/) (link to the offical website)

## Getting Started

In order to run and test the project the first thing need to be done is to build each one of the services images.

First in any terminal navigate to each one of the services directory (src/ServiceA, src/ServiceB etc..) and run the following commands

```
docker build -t servicea .      (for src/ServiceA)
docker build -t serviceb .      (for src/ServiceB)
docker build -t servicec .      (for src/ServiceC)
```

after that all the images for the services exist and now you can run the following command in the cli inside the project folder (Message Processing System) to run all the services:

```
docker compose -f .\dockercompose.yml up -d
```

now you can jump directly to this url (http://localhost:5002/api/ServiceC) or run trough the testing process.

## Testing

Now that we have the container running there are several things that accesable,

### Service A :

* (http://localhost:5000/api/ServiceA)

in this service when we navigating to the url we are making a API call (GET), this call generates random number as shown at the endpoint as seen and changes with each refresh but also saved to the database (dbo.random_Number) and it can be seen with any sql system as SSMS or Azure Data Studio and etc.. with the following credentials:

```
Server Name: localhost,1433
Login: sa
Password: P@ssw0rd121#
```

### Service B :

* (http://localhost:5001/api/ServiceB)

in this service we make an API call to the first service which generate another random number and saves it to the DB as mentioned but now we adding the step of this service which multiply the number as shown in the endpoint and also save it in cache with Redis
in the Redis server you wont be able to see the saved number because it's save as hash and not as string but you will be able to see it in the next service.

### Service C :

* (http://localhost:5002/api/ServiceC)

in the last service again we making another API call that make another API call to ServiceA that generate the number and then get multiplied and cached in ServiceB and finally we getting this cache from the Redis server and reveal him in the endpoint with is the website instead of terminal.

## Adittions

if i had more time i would definitely add terminal and complete the task as it should be, i would also seperate and simplify the controllers of the microservice to one liners for more clean code and i would try to figure out about a better way rather than use API calls to communicate between the microservices because it isn's felt the official way to do that.
