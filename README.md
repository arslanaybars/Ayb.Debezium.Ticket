
# Ayb.Debezium.Ticket

# Introduction

This open source software project is a .NET framework based implementation of the Outbox Pattern using Apache Kafka and Debezium. 

# Try in machine

1. Run ```docker-compose up``` and wait for all infra running.
2. Send a Ticket api requests (Post, Put or Delete). See the what's happening. (The postman collection is available in the repository)
3. Optional  you can use the control center at http://localhost:9021
4. Optional  You can see logs at http://localhost:5555/#/events

# Pictures make concepts easier to grasp
High-Level diagram of the sample app
![image](https://raw.githubusercontent.com/arslanaybars/Ayb.Debezium.Ticket/main/images/debezium.png)

Tickets Table
![image](https://raw.githubusercontent.com/arslanaybars/Ayb.Debezium.Ticket/main/images/ticket-table.png)

OutboxEvents Table
![image](https://raw.githubusercontent.com/arslanaybars/Ayb.Debezium.Ticket/main/images/outbox-events-table.png)

MongoDB Tickets collection
![image](https://raw.githubusercontent.com/arslanaybars/Ayb.Debezium.Ticket/main/images/mongo-tickets.png)

# Tools

- .NET framework
- Apache Kafka
- Debezium
- PostgreSQL
- MongoDB
- Seq
- Docker

# Thanks To
[@oguzhankiyar](https://github.com/oguzhankiyar)

[@suadev](https://github.com/suadev)

[@kahramanumut](https://github.com/kahramanumut)


# References
https://debezium.io/blog/2019/02/19/reliable-microservices-data-exchange-with-the-outbox-pattern/

https://github.com/suadev/microservices-change-data-capture-with-debezium

# Licence

This project is licensed under the MIT License. See the LICENSE file for details.
