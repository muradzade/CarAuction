# Overview
Followed Object-Oriented, SOLID, KISS Principles. This project targeted simplicity, extensibility and maintainability. These also make the code review and tests easier.

## Object-Oriented Principles
Created abstract Vehicle class to hold common properties. Vehicle-specific properties are presented in child classes. Inheritance behavior preserved, we can add new vehicle types without modifying existing code.

## SOLID Principles
**VehicleService** only manages vehicle inventory operations, **AuctionService** only handles auction lifecycle and bidding, each vehicle class represents one vehicle type with its specific properties. Vehicle hierarchy allows new types without changing existing code. Any Vehicle subclass can replace Vehicle base class without breaking functionality. **IVehicleService**, **IAuctionService** have specific responsibilities. **CarAuctionFacadeService** depends on interfaces, not directly services.

## Design Patterns
**Business Layer** separated business logic from data access by using services. **Facade Pattern** provides unified, simplified interface with CarAuctionFacadeService and hides complexity of multiple service interactions.

## Exception Handling
Custom exception types for specific error scenarios. Fail-fast validation in constructors. This ensures that objects are always valid.

## Testing
Used XUnit test framework. **Unit Tests**: All classes tests are isolated. **Integration Tests**: Full workflow tested through facade service. **Mocking** used for testing service interactions in isolation.