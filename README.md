# scheduler-service

Scheduler is a tool for IT administrators that enables remote execution of PowerShell commands on computers within a network. Its features include creating task templates, simultaneously executing task trees on multiple machines, and logging actions, which simplifies the management of the IT environment and increases administrative efficiency. The project can be especially useful in large IT environments where managing numerous computers requires automation and centralization of tasks.

The Scheduler Service is the core of the distributed system. It handles requests from the web application and distributes them to worker nodes. It is developed using .NET Core, Entity Framework, GraphQL, Zipkin, and Docker.

The Azure pipeline builds image and pushes it to repository at: https://hub.docker.com/repository/docker/mateusz7812/scheduler_service/general.

More information can be found in other projects related to the Scheduler app.
