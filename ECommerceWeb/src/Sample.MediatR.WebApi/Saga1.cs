/*
Hi
Do we have class today ?

Saga
    -   solution for long-running, distributed transactions in a reliable and testable manner.
    -   In distributed system, different parts of a business transaction are handled by different microservices often with their own databases
    -   In short - saga is a sequence of local transactions
    -   Each transaction updates data within a single service
        and each service publishes an event to trigger the next transaction in the saga
        if an fails saga executes compensating transactions to undo the impact of the failed transaction.
    -   long-running, distributed transactions where each step needs to be reliable and reversible
    -   2 types
        a.  Choreography-Based Saga
            -   local transaction publishes domain events that other services listen to.
            -   here, there is no central coordination.
            -   Each service is responsible for knowing which events it needs to listen for
                and what actions it needs to take when it receives an event.
            -   reduces coupling between services, as they only need to communicate through events.

        b.  Orchestration-Based Saga
            -   central orchestrator that is responsible for managing the saga.
            -   Example - queuing system with a new Checkout Service
            -   there isn’t a queuing system to pull from.
                Instead, the new Checkout Service acting as the orchestrator for the fulfillment process    
            -   orchestrator instructs each service involved in the transaction on what action to take
            -   centralizes the control and decision-making processn easy to manage and understanf
            -   tighter coupling between services as they need to know how to communicate with the orchestrator.

NServicBus
    -   focus on distributed architectures and microservices.
    -   communication between mutually interacting software applications in a service-oriented architecture
    -   messages between different parts of a system, such as queuing, publish/subscribe, and advanced routing capabilities


ICommand interface
    -   request to perform a action or operation
        that should change the state of our saga or initiate some behavior in the system.

IEvent interface
    -   is a signal that something has happened in system.
        We usually broadcast it to multiple listeners to inform them about past actions or state changes.






*/
