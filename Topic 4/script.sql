-- ============================================
-- SQL Practice Database Schema and Test Data
-- Compatible with Microsoft SQL Server
-- ============================================

-- Drop all tables if they exist (in correct order due to foreign key constraints)
IF OBJECT_ID('PAYMENT_BY_POLICY', 'U') IS NOT NULL DROP TABLE PAYMENT_BY_POLICY;
IF OBJECT_ID('POLICY', 'U') IS NOT NULL DROP TABLE POLICY;
IF OBJECT_ID('INSURANCE_TYPE', 'U') IS NOT NULL DROP TABLE INSURANCE_TYPE;
IF OBJECT_ID('POLICYHOLDER', 'U') IS NOT NULL DROP TABLE POLICYHOLDER;

IF OBJECT_ID('RESERVATION', 'U') IS NOT NULL DROP TABLE RESERVATION;
IF OBJECT_ID('FLIGHT', 'U') IS NOT NULL DROP TABLE FLIGHT;
IF OBJECT_ID('ROUTE', 'U') IS NOT NULL DROP TABLE ROUTE;
IF OBJECT_ID('AIRCRAFT', 'U') IS NOT NULL DROP TABLE AIRCRAFT;
IF OBJECT_ID('AIRPORT', 'U') IS NOT NULL DROP TABLE AIRPORT;

IF OBJECT_ID('ENGAGEMENT', 'U') IS NOT NULL DROP TABLE ENGAGEMENT;
IF OBJECT_ID('PROJECT', 'U') IS NOT NULL DROP TABLE PROJECT;
IF OBJECT_ID('WORKER', 'U') IS NOT NULL DROP TABLE WORKER;
IF OBJECT_ID('COMPANY', 'U') IS NOT NULL DROP TABLE COMPANY;

IF OBJECT_ID('PAYMENT', 'U') IS NOT NULL DROP TABLE PAYMENT;
IF OBJECT_ID('ENGAGEMENT_PROF', 'U') IS NOT NULL DROP TABLE ENGAGEMENT_PROF;
IF OBJECT_ID('PROFESSOR', 'U') IS NOT NULL DROP TABLE PROFESSOR;
IF OBJECT_ID('FACULTY', 'U') IS NOT NULL DROP TABLE FACULTY;
IF OBJECT_ID('UNIVERSITY', 'U') IS NOT NULL DROP TABLE UNIVERSITY;

IF OBJECT_ID('GRADE', 'U') IS NOT NULL DROP TABLE GRADE;
IF OBJECT_ID('CERTIFICATE', 'U') IS NOT NULL DROP TABLE CERTIFICATE;
IF OBJECT_ID('CANDIDATE', 'U') IS NOT NULL DROP TABLE CANDIDATE;
IF OBJECT_ID('SCHOOL', 'U') IS NOT NULL DROP TABLE SCHOOL;
IF OBJECT_ID('PLACE', 'U') IS NOT NULL DROP TABLE PLACE;

IF OBJECT_ID('MATCH_STATS', 'U') IS NOT NULL DROP TABLE MATCH_STATS;
IF OBJECT_ID('MATCH_PLAYER', 'U') IS NOT NULL DROP TABLE MATCH_PLAYER;
IF OBJECT_ID('MATCH', 'U') IS NOT NULL DROP TABLE MATCH;
IF OBJECT_ID('PLAYER', 'U') IS NOT NULL DROP TABLE PLAYER;
IF OBJECT_ID('FOOTBALL_TEAM', 'U') IS NOT NULL DROP TABLE FOOTBALL_TEAM;

IF OBJECT_ID('DEPARTURE', 'U') IS NOT NULL DROP TABLE DEPARTURE;
IF OBJECT_ID('ROUTE_STATIONS', 'U') IS NOT NULL DROP TABLE ROUTE_STATIONS;
IF OBJECT_ID('ROUTE_TRANSPORT', 'U') IS NOT NULL DROP TABLE ROUTE_TRANSPORT;
IF OBJECT_ID('STATION', 'U') IS NOT NULL DROP TABLE STATION;
IF OBJECT_ID('VEHICLE', 'U') IS NOT NULL DROP TABLE VEHICLE;

IF OBJECT_ID('COMPONENT_ATTRIBUTES', 'U') IS NOT NULL DROP TABLE COMPONENT_ATTRIBUTES;
IF OBJECT_ID('CONNECTION', 'U') IS NOT NULL DROP TABLE CONNECTION;
IF OBJECT_ID('ASSEMBLY', 'U') IS NOT NULL DROP TABLE ASSEMBLY;
IF OBJECT_ID('COMPONENT', 'U') IS NOT NULL DROP TABLE COMPONENT;
IF OBJECT_ID('TYPE_ATTRIBUTES', 'U') IS NOT NULL DROP TABLE TYPE_ATTRIBUTES;
IF OBJECT_ID('COMPONENT_TYPE', 'U') IS NOT NULL DROP TABLE COMPONENT_TYPE;

IF OBJECT_ID('WROTE', 'U') IS NOT NULL DROP TABLE WROTE;
IF OBJECT_ID('BOOK_COPY', 'U') IS NOT NULL DROP TABLE BOOK_COPY;
IF OBJECT_ID('BOOK', 'U') IS NOT NULL DROP TABLE BOOK;
IF OBJECT_ID('AUTHOR', 'U') IS NOT NULL DROP TABLE AUTHOR;

IF OBJECT_ID('RESERVATION_HOTEL', 'U') IS NOT NULL DROP TABLE RESERVATION_HOTEL;
IF OBJECT_ID('HOTEL_ROOMTYPE', 'U') IS NOT NULL DROP TABLE HOTEL_ROOMTYPE;
IF OBJECT_ID('ROOM', 'U') IS NOT NULL DROP TABLE ROOM;
IF OBJECT_ID('ROOMTYPE', 'U') IS NOT NULL DROP TABLE ROOMTYPE;
IF OBJECT_ID('HOTEL', 'U') IS NOT NULL DROP TABLE HOTEL;

IF OBJECT_ID('DELIVERY', 'U') IS NOT NULL DROP TABLE DELIVERY;
IF OBJECT_ID('PRODUCT', 'U') IS NOT NULL DROP TABLE PRODUCT;
IF OBJECT_ID('CUSTOMER', 'U') IS NOT NULL DROP TABLE CUSTOMER;
IF OBJECT_ID('SUPPLIER', 'U') IS NOT NULL DROP TABLE SUPPLIER;

IF OBJECT_ID('CHILD', 'U') IS NOT NULL DROP TABLE CHILD;
IF OBJECT_ID('WORKS_AT_CLINIC', 'U') IS NOT NULL DROP TABLE WORKS_AT_CLINIC;
IF OBJECT_ID('DOCTOR', 'U') IS NOT NULL DROP TABLE DOCTOR;
IF OBJECT_ID('CLINIC', 'U') IS NOT NULL DROP TABLE CLINIC;

-- ============================================
-- 1. INSURANCE SYSTEM
-- ============================================

CREATE TABLE POLICYHOLDER (
    PERSONID INT PRIMARY KEY,
    SURNAME VARCHAR(50),
    NAME VARCHAR(50),
    ADDRESS VARCHAR(100),
    CITY VARCHAR(50)
);

CREATE TABLE INSURANCE_TYPE (
    TYPEID INT PRIMARY KEY,
    NAME VARCHAR(100),
    TARIFF DECIMAL(10,2)
);

CREATE TABLE POLICY (
    POLICYID INT PRIMARY KEY,
    ISSUE_DATE DATE,
    TOTAL_AMOUNT DECIMAL(12,2),
    PERSONID INT,
    INSURANCE_TYPEID INT,
    NUM_MONTHLY_INSTALLMENTS INT,
    PREMIUM_CLASS VARCHAR(20),
    REPLACEMENT_POLICYID INT,
    FOREIGN KEY (PERSONID) REFERENCES POLICYHOLDER(PERSONID),
    FOREIGN KEY (INSURANCE_TYPEID) REFERENCES INSURANCE_TYPE(TYPEID),
    FOREIGN KEY (REPLACEMENT_POLICYID) REFERENCES POLICY(POLICYID)
);

CREATE TABLE PAYMENT_BY_POLICY (
    POLICYID INT,
    PAYMENT_DATE DATE,
    AMOUNT DECIMAL(10,2),
    PRIMARY KEY (POLICYID, PAYMENT_DATE),
    FOREIGN KEY (POLICYID) REFERENCES POLICY(POLICYID)
);

-- Insert test data for Insurance System
INSERT INTO POLICYHOLDER VALUES 
(1, 'Smith', 'John', '123 Main St', 'New York'),
(2, 'Johnson', 'Mary', '456 Oak Ave', 'Chicago'),
(3, 'Williams', 'Robert', '789 Pine St', 'Los Angeles'),
(4, 'Brown', 'Linda', '321 Elm St', 'Houston'),
(5, 'Davis', 'Michael', '654 Maple Ave', 'Phoenix');

INSERT INTO INSURANCE_TYPE VALUES 
(1, 'VEHICLE INSURANCE', 500.00),
(2, 'PROPERTY INSURANCE', 800.00),
(3, 'LIFE INSURANCE', 1200.00),
(4, 'HEALTH INSURANCE', 600.00);

INSERT INTO POLICY VALUES 
(1, '2024-01-15', 5000.00, 1, 1, 12, 'PREMIUM', NULL),
(2, '2024-08-20', 8000.00, 2, 2, 24, 'STANDARD', NULL),
(3, '2023-12-10', 12000.00, 3, 3, 36, 'PREMIUM', NULL),
(4, '2024-09-05', 3000.00, 4, 1, 6, 'BASIC', NULL),
(5, '2021-06-15', 4000.00, 5, 1, 12, 'STANDARD', NULL);

INSERT INTO PAYMENT_BY_POLICY VALUES 
(1, '2024-02-15', 416.67),
(1, '2024-03-15', 416.67),
(2, '2024-09-20', 333.33),
(3, '2024-01-10', 333.33),
(3, '2024-02-10', 333.33),
(4, '2024-10-05', 500.00);

-- ============================================
-- 2. AIRPORT SYSTEM
-- ============================================

CREATE TABLE AIRPORT (
    AEROID INT PRIMARY KEY,
    NAME VARCHAR(100),
    COUNTRY VARCHAR(50),
    CITY VARCHAR(50)
);

CREATE TABLE ROUTE (
    ROUTEID INT PRIMARY KEY,
    DEPARTURE_AERO INT,
    DESTINATION_AERO INT,
    FOREIGN KEY (DEPARTURE_AERO) REFERENCES AIRPORT(AEROID),
    FOREIGN KEY (DESTINATION_AERO) REFERENCES AIRPORT(AEROID)
);

CREATE TABLE AIRCRAFT (
    PLANEID INT PRIMARY KEY,
    DESIGNATION VARCHAR(50),
    AIRCRAFT_TYPE VARCHAR(50),
    NUM_SEATS INT
);

CREATE TABLE FLIGHT (
    ROUTEID INT,
    DATE_TIME DATETIME,
    PLANEID INT,
    PRIMARY KEY (ROUTEID, DATE_TIME),
    FOREIGN KEY (ROUTEID) REFERENCES ROUTE(ROUTEID),
    FOREIGN KEY (PLANEID) REFERENCES AIRCRAFT(PLANEID)
);

CREATE TABLE RESERVATION (
    ROUTEID INT,
    DATE_TIME DATETIME,
    SEQ_NUM INT,
    STATUS VARCHAR(20),
    PRIMARY KEY (ROUTEID, DATE_TIME, SEQ_NUM),
    FOREIGN KEY (ROUTEID, DATE_TIME) REFERENCES FLIGHT(ROUTEID, DATE_TIME)
);

-- Insert test data for Airport System
INSERT INTO AIRPORT VALUES 
(1, 'Heathrow', 'GREAT BRITAIN', 'London'),
(2, 'Frankfurt Main', 'GERMANY', 'Frankfurt'),
(3, 'Zurich Airport', 'SWITZERLAND', 'Zurich'),
(4, 'Charles de Gaulle', 'FRANCE', 'Paris'),
(5, 'JFK International', 'USA', 'New York'),
(6, 'Madrid Barajas', 'SPAIN', 'Madrid');

INSERT INTO AIRCRAFT VALUES 
(1, 'Boeing 737', 'Commercial', 180),
(2, 'Airbus A320', 'Commercial', 150),
(3, 'Boeing 747', 'Commercial', 400),
(4, 'Airbus A380', 'Commercial', 550),
(5, 'Embraer 190', 'Regional', 100);

INSERT INTO ROUTE VALUES 
(1, 1, 2), -- London to Frankfurt
(2, 1, 3), -- London to Zurich  
(3, 2, 6), -- Frankfurt to Madrid
(4, 3, 4), -- Zurich to Paris
(5, 5, 1); -- New York to London

INSERT INTO FLIGHT VALUES 
(1, '2025-04-15 08:30:00', 1),
(1, '2025-05-20 14:15:00', 2),
(2, '2003-11-15 10:00:00', 3),
(2, '2003-11-15 16:30:00', 4),
(3, '2025-06-10 12:00:00', 5),
(4, '2025-05-25 18:45:00', 1),
(5, '2025-04-28 22:15:00', 3);

INSERT INTO RESERVATION VALUES 
(1, '2025-04-15 08:30:00', 1, 'OK'),
(1, '2025-04-15 08:30:00', 2, 'OK'),
(2, '2003-11-15 10:00:00', 1, 'Postponed'),
(2, '2003-11-15 16:30:00', 1, 'OK'),
(3, '2025-06-10 12:00:00', 1, 'CANCELLED');

-- ============================================
-- 3. PROJECT MANAGEMENT SYSTEM
-- ============================================

CREATE TABLE COMPANY (
    COMPANY_CODE INT PRIMARY KEY,
    NAME VARCHAR(100),
    CITY VARCHAR(50)
);

CREATE TABLE WORKER (
    WORKER_CODE INT PRIMARY KEY,
    WORKER_NAME VARCHAR(100),
    BIRTH_DATE DATE,
    HIRE_DATE DATE,
    SALARY DECIMAL(10,2),
    COMPANY_CODE INT,
    IS_MANAGER BIT,
    FOREIGN KEY (COMPANY_CODE) REFERENCES COMPANY(COMPANY_CODE)
);

CREATE TABLE PROJECT (
    PROJ_CODE INT PRIMARY KEY,
    PROJECT_NAME VARCHAR(100),
    BUDGET DECIMAL(12,2),
    PROJECT_LEADER INT,
    FOREIGN KEY (PROJECT_LEADER) REFERENCES WORKER(WORKER_CODE)
);

CREATE TABLE ENGAGEMENT (
    WORKER_CODE INT,
    PROJ_CODE INT,
    PRIMARY KEY (WORKER_CODE, PROJ_CODE),
    FOREIGN KEY (WORKER_CODE) REFERENCES WORKER(WORKER_CODE),
    FOREIGN KEY (PROJ_CODE) REFERENCES PROJECT(PROJ_CODE)
);

-- Insert test data for Project Management System
INSERT INTO COMPANY VALUES 
(1, 'TechCorp', 'Belgrade'),
(2, 'DataSoft', 'Novi Sad'),
(3, 'DevSolutions', 'Nis'),
(4, 'InfoSystems', 'Belgrade'),
(5, 'CodeFactory', 'Kragujevac');

INSERT INTO WORKER VALUES 
(1, 'Petar Petrovic', '1985-03-15', '2014-05-01', 45000.00, 1, 1),
(2, 'Ana Nikolic', '1990-07-22', '2018-02-15', 32000.00, 1, 0),
(3, 'Marko Jovanovic', '1982-11-08', '2012-09-10', 38000.00, 2, 0),
(4, 'Milica Stojanovic', '1988-01-30', '2015-03-20', 42000.00, 2, 1),
(5, 'Stefan Milic', '1992-05-14', '2020-06-01', 28000.00, 3, 0),
(6, 'Jovana Radic', '1987-12-03', '2013-08-15', 35000.00, 1, 0);

INSERT INTO PROJECT VALUES 
(1, 'Web Platform', 500000.00, 1),
(2, 'Mobile App', 300000.00, 4),
(3, 'Database Migration', 200000.00, 1),
(4, 'AI Analytics', 800000.00, 4),
(5, 'Cloud Infrastructure', 450000.00, 1);

INSERT INTO ENGAGEMENT VALUES 
(1, 1), (1, 3), (1, 5),
(2, 1), (2, 2),
(3, 2), (3, 4),
(4, 2), (4, 4),
(5, 3),
(6, 1), (6, 5);

-- ============================================
-- 4. UNIVERSITY SYSTEM
-- ============================================

CREATE TABLE UNIVERSITY (
    UniversityCode INT PRIMARY KEY,
    UniversityName VARCHAR(100)
);

CREATE TABLE FACULTY (
    FacultyCode INT PRIMARY KEY,
    FacultyName VARCHAR(100),
    UniversityCode INT,
    FOREIGN KEY (UniversityCode) REFERENCES UNIVERSITY(UniversityCode)
);

CREATE TABLE PROFESSOR (
    SSN VARCHAR(13) PRIMARY KEY,
    Name VARCHAR(50),
    Surname VARCHAR(50),
    HireDate DATE,
    HomeFaculty INT,
    FOREIGN KEY (HomeFaculty) REFERENCES FACULTY(FacultyCode)
);

CREATE TABLE ENGAGEMENT_PROF (
    SSN VARCHAR(13),
    FacultyCode INT,
    PRIMARY KEY (SSN, FacultyCode),
    FOREIGN KEY (SSN) REFERENCES PROFESSOR(SSN),
    FOREIGN KEY (FacultyCode) REFERENCES FACULTY(FacultyCode)
);

CREATE TABLE PAYMENT (
    SSN VARCHAR(13),
    FacultyCode INT,
    PaymentDate DATE,
    Amount DECIMAL(10,2),
    PRIMARY KEY (SSN, FacultyCode, PaymentDate),
    FOREIGN KEY (SSN, FacultyCode) REFERENCES ENGAGEMENT_PROF(SSN, FacultyCode)
);

-- Insert test data for University System
INSERT INTO UNIVERSITY VALUES 
(1, 'University of Belgrade'),
(2, 'University of Novi Sad'),
(3, 'University of Nis');

INSERT INTO FACULTY VALUES 
(1, 'Faculty of Mathematics', 1),
(2, 'Faculty of Computer Science', 1),
(3, 'Faculty of Engineering', 2),
(4, 'Faculty of Medicine', 1),
(5, 'Faculty of Economics', 3);

INSERT INTO PROFESSOR VALUES 
('1234567890123', 'Petar', 'Petrovic', '2015-09-01', 1),
('2345678901234', 'Ana', 'Nikolic', '2018-02-15', 2),
('3456789012345', 'Marko', 'Jovanovic', '2014-03-10', 1),
('4567890123456', 'Milica', 'Stojanovic', '2016-09-01', 3),
('5678901234567', 'Stefan', 'Milic', '2017-10-01', 2);

INSERT INTO ENGAGEMENT_PROF VALUES 
('1234567890123', 1), ('1234567890123', 2),
('2345678901234', 2), ('2345678901234', 3),
('3456789012345', 1),
('4567890123456', 3), ('4567890123456', 4),
('5678901234567', 2);

INSERT INTO PAYMENT VALUES 
('1234567890123', 1, '2024-01-31', 80000.00),
('1234567890123', 2, '2024-01-31', 25000.00),
('2345678901234', 2, '2024-02-29', 75000.00),
('2345678901234', 3, '2024-02-29', 30000.00),
('3456789012345', 1, '2024-03-31', 82000.00);

-- ============================================
-- 5. STUDENT ADMISSION SYSTEM
-- ============================================

CREATE TABLE PLACE (
    PlaceCode INT PRIMARY KEY,
    PlaceName VARCHAR(100)
);

CREATE TABLE SCHOOL (
    SchoolCode INT PRIMARY KEY,
    SchoolName VARCHAR(100),
    SchoolType VARCHAR(50),
    PlaceCode INT,
    FOREIGN KEY (PlaceCode) REFERENCES PLACE(PlaceCode),
    CHECK (SchoolType IN ('Agricultural', 'Technical', 'Gymnasium', 'other'))
);

CREATE TABLE CANDIDATE (
    SSN VARCHAR(13) PRIMARY KEY,
    Surname VARCHAR(50),
    ParentName VARCHAR(50),
    Name VARCHAR(50),
    BirthDate DATE,
    OverallSuccess VARCHAR(20),
    CompletedSchool INT,
    ApplicationDate DATE,
    ApplicationNumber INT,
    FOREIGN KEY (CompletedSchool) REFERENCES SCHOOL(SchoolCode)
);

CREATE TABLE CERTIFICATE (
    SSN VARCHAR(13),
    Year INT,
    Average DECIMAL(3,2),
    SchoolCode INT,
    PRIMARY KEY (SSN, Year),
    FOREIGN KEY (SSN) REFERENCES CANDIDATE(SSN),
    FOREIGN KEY (SchoolCode) REFERENCES SCHOOL(SchoolCode)
);

CREATE TABLE GRADE (
    SSN VARCHAR(13),
    Year INT,
    Seq INT,
    SubjectName VARCHAR(50),
    Grade INT,
    PRIMARY KEY (SSN, Year, Seq),
    FOREIGN KEY (SSN, Year) REFERENCES CERTIFICATE(SSN, Year),
    CHECK (Grade IN (2,3,4,5))
);

-- Insert test data for Student Admission System
INSERT INTO PLACE VALUES 
(1, 'Belgrade'),
(2, 'Novi Sad'),
(3, 'Nis'),
(4, 'Kragujevac'),
(5, 'Subotica');

INSERT INTO SCHOOL VALUES 
(1, 'Technical School Belgrade', 'Technical', 1),
(2, 'Gymnasium Novi Sad', 'Gymnasium', 2),
(3, 'Agricultural School Nis', 'Agricultural', 3),
(4, 'Technical School Kragujevac', 'Technical', 4),
(5, 'Medical School Belgrade', 'other', 1);

INSERT INTO CANDIDATE VALUES 
('1234567890123', 'Petrovic', 'Milos', 'Stefan', '2005-03-15', 'excellent', 1, '2003-06-25', 1001),
('2345678901234', 'Nikolic', 'Petar', 'Ana', '2005-07-22', 'very good', 2, '2003-06-20', 1002),
('3456789012345', 'Jovanovic', 'Milan', 'Marko', '2005-11-08', 'good', 1, '2003-06-25', 1003),
('4567890123456', 'Stojanovic', 'Dragan', 'Milica', '2005-01-30', 'excellent', 4, '2003-06-15', 1004),
('5678901234567', 'Milic', 'Zoran', 'Jovana', '2005-05-14', 'sufficient', 3, '2003-06-25', 1005);

INSERT INTO CERTIFICATE VALUES 
('1234567890123', 4, 4.75, 1),
('2345678901234', 4, 4.25, 2),
('3456789012345', 4, 3.80, 1),
('4567890123456', 4, 4.95, 4),
('5678901234567', 4, 3.50, 3);

INSERT INTO GRADE VALUES 
('1234567890123', 4, 1, 'Mathematics', 5),
('1234567890123', 4, 2, 'Physics', 5),
('1234567890123', 4, 3, 'Chemistry', 4),
('2345678901234', 4, 1, 'Mathematics', 4),
('2345678901234', 4, 2, 'Literature', 5),
('3456789012345', 4, 1, 'Mathematics', 4),
('3456789012345', 4, 2, 'Physics', 3);

-- ============================================
-- 6. FOOTBALL LEAGUE SYSTEM
-- ============================================

CREATE TABLE FOOTBALL_TEAM (
    TEAMID INT PRIMARY KEY,
    NAME VARCHAR(100),
    CITY VARCHAR(50)
);

CREATE TABLE PLAYER (
    PLAYERID INT PRIMARY KEY,
    NAME_SURNAME VARCHAR(100),
    BIRTH_DATE DATE,
    TEAMID INT,
    FOREIGN KEY (TEAMID) REFERENCES FOOTBALL_TEAM(TEAMID)
);

CREATE TABLE MATCH (
    ROUND_NUM INT,
    PAIR_NUM INT,
    HOME_TEAM INT,
    AWAY_TEAM INT,
    DATE DATE,
    TOTAL_HOME INT,
    TOTAL_AWAY INT,
    PRIMARY KEY (ROUND_NUM, PAIR_NUM),
    FOREIGN KEY (HOME_TEAM) REFERENCES FOOTBALL_TEAM(TEAMID),
    FOREIGN KEY (AWAY_TEAM) REFERENCES FOOTBALL_TEAM(TEAMID)
);

CREATE TABLE MATCH_PLAYER (
    ROUND_NUM INT,
    PAIR_NUM INT,
    PLAYERID INT,
    MINUTES_PLAYED INT,
    PRIMARY KEY (ROUND_NUM, PAIR_NUM, PLAYERID),
    FOREIGN KEY (ROUND_NUM, PAIR_NUM) REFERENCES MATCH(ROUND_NUM, PAIR_NUM),
    FOREIGN KEY (PLAYERID) REFERENCES PLAYER(PLAYERID)
);

CREATE TABLE MATCH_STATS (
    ROUND_NUM INT,
    PAIR_NUM INT,
    TIME VARCHAR(10),
    PLAYERID INT,
    EVENT VARCHAR(20),
    PRIMARY KEY (ROUND_NUM, PAIR_NUM, TIME, PLAYERID, EVENT),
    FOREIGN KEY (ROUND_NUM, PAIR_NUM) REFERENCES MATCH(ROUND_NUM, PAIR_NUM),
    FOREIGN KEY (PLAYERID) REFERENCES PLAYER(PLAYERID),
    CHECK (EVENT IN ('GOAL', 'OWN GOAL', 'YELLOW CARD', 'RED CARD'))
);

-- Insert test data for Football League System
INSERT INTO FOOTBALL_TEAM VALUES 
(1, 'PARTIZAN', 'Belgrade'),
(2, 'Real Madrid', 'Madrid'),
(3, 'Barcelona', 'Barcelona'),
(4, 'Bayern Munich', 'Munich'),
(5, 'Liverpool', 'Liverpool');

INSERT INTO PLAYER VALUES 
(1, 'Milos Petrovic', '1995-03-15', 1),
(2, 'Stefan Nikolic', '1992-07-22', 1),
(3, 'Karim Benzema', '1987-12-19', 2),
(4, 'Vinicius Junior', '2000-07-12', 2),
(5, 'Lionel Messi', '1987-06-24', 3),
(6, 'Robert Lewandowski', '1988-08-21', 4),
(7, 'Mohamed Salah', '1992-06-15', 5);

INSERT INTO MATCH VALUES 
(1, 1, 1, 2, '2024-08-15', 2, 1),
(4, 1, 1, 3, '2024-09-10', 1, 1),
(7, 1, 2, 4, '2024-10-05', 3, 2),
(7, 2, 3, 5, '2024-10-05', 2, 0);

INSERT INTO MATCH_PLAYER VALUES 
(1, 1, 1, 90), (1, 1, 2, 75), (1, 1, 3, 90), (1, 1, 4, 85),
(4, 1, 1, 90), (4, 1, 2, 88), (4, 1, 5, 90),
(7, 1, 3, 90), (7, 1, 4, 82), (7, 1, 6, 90);

INSERT INTO MATCH_STATS VALUES 
(1, 1, '25:30', 1, 'GOAL'),
(1, 1, '67:15', 2, 'GOAL'),
(1, 1, '45:00', 3, 'GOAL'),
(4, 1, '30:45', 1, 'GOAL'),
(4, 1, '55:20', 5, 'GOAL'),
(7, 1, '15:30', 3, 'GOAL'),
(7, 1, '22:45', 3, 'GOAL'),
(7, 1, '78:10', 4, 'GOAL');

-- ============================================
-- 7. PUBLIC TRANSPORTATION SYSTEM
-- ============================================

CREATE TABLE VEHICLE (
    VehicleCode INT PRIMARY KEY,
    VehicleType VARCHAR(20),
    CHECK (VehicleType IN ('Bus', 'Tram', 'Trolleybus'))
);

CREATE TABLE STATION (
    StationCode INT PRIMARY KEY,
    StationName VARCHAR(100)
);

CREATE TABLE ROUTE_TRANSPORT (
    RouteNumber INT PRIMARY KEY,
    StartStation INT,
    EndStation INT,
    FOREIGN KEY (StartStation) REFERENCES STATION(StationCode),
    FOREIGN KEY (EndStation) REFERENCES STATION(StationCode)
);

CREATE TABLE ROUTE_STATIONS (
    RouteNumber INT,
    StationCode INT,
    SEQ_NUM INT,
    PRIMARY KEY (RouteNumber, StationCode),
    FOREIGN KEY (RouteNumber) REFERENCES ROUTE_TRANSPORT(RouteNumber),
    FOREIGN KEY (StationCode) REFERENCES STATION(StationCode)
);

CREATE TABLE DEPARTURE (
    VehicleCode INT,
    RouteNumber INT,
    DateTime DATETIME,
    Status VARCHAR(20),
    PRIMARY KEY (VehicleCode, RouteNumber, DateTime),
    FOREIGN KEY (VehicleCode) REFERENCES VEHICLE(VehicleCode),
    FOREIGN KEY (RouteNumber) REFERENCES ROUTE_TRANSPORT(RouteNumber),
    CHECK (Status IN ('Successful', 'Cancelled', 'Delayed'))
);

-- Insert test data for Public Transportation System
INSERT INTO VEHICLE VALUES 
(1, 'Bus'), (2, 'Bus'), (3, 'Tram'), (4, 'Tram'), (5, 'Trolleybus');

INSERT INTO STATION VALUES 
(1, 'Central Station'), (2, 'Airport'), (3, 'University'), 
(4, 'Shopping Mall'), (5, 'Hospital'), (6, 'Stadium');

INSERT INTO ROUTE_TRANSPORT VALUES 
(18, 1, 2), (25, 3, 4), (7, 5, 6);

INSERT INTO ROUTE_STATIONS VALUES 
(18, 1, 1), (18, 3, 2), (18, 2, 3),
(25, 3, 1), (25, 4, 2), (25, 5, 3),
(7, 5, 1), (7, 6, 2);

INSERT INTO DEPARTURE VALUES 
(1, 18, '2003-04-01 13:30:00', 'Cancelled'),
(2, 18, '2003-04-01 15:45:00', 'Cancelled'),
(1, 18, '2003-04-07 14:15:00', 'Successful'),
(3, 25, '2025-09-08 08:30:00', 'Successful'),
(4, 25, '2025-09-08 12:45:00', 'Delayed'),
(5, 7, '2025-01-15 10:30:00', 'Successful');

-- ============================================
-- 8. COMPONENT MANAGEMENT SYSTEM
-- ============================================

CREATE TABLE COMPONENT_TYPE (
    TYPEID INT PRIMARY KEY,
    NAME VARCHAR(100),
    DESCRIPTION VARCHAR(255)
);

CREATE TABLE TYPE_ATTRIBUTES (
    TYPEID INT,
    ATTRIBUTEID INT,
    NAME VARCHAR(100),
    DOMAIN VARCHAR(50),
    REQUIRED BIT,
    PRIMARY KEY (TYPEID, ATTRIBUTEID),
    FOREIGN KEY (TYPEID) REFERENCES COMPONENT_TYPE(TYPEID)
);

CREATE TABLE COMPONENT (
    COMPID INT PRIMARY KEY,
    SERIALNUMBER VARCHAR(50),
    COMPONENTTYPE INT,
    FOREIGN KEY (COMPONENTTYPE) REFERENCES COMPONENT_TYPE(TYPEID)
);

CREATE TABLE COMPONENT_ATTRIBUTES (
    COMPID INT,
    ATTRIBUTEID INT,
    VALUE VARCHAR(255),
    PRIMARY KEY (COMPID, ATTRIBUTEID),
    FOREIGN KEY (COMPID) REFERENCES COMPONENT(COMPID)
);

CREATE TABLE CONNECTION (
    PARENT_COMPID INT,
    CHILD_COMPID INT,
    PRIMARY KEY (PARENT_COMPID, CHILD_COMPID),
    FOREIGN KEY (PARENT_COMPID) REFERENCES COMPONENT(COMPID),
    FOREIGN KEY (CHILD_COMPID) REFERENCES COMPONENT(COMPID)
);

CREATE TABLE ASSEMBLY (
    COMPONENTID INT PRIMARY KEY,
    NAME VARCHAR(100),
    FOREIGN KEY (COMPONENTID) REFERENCES COMPONENT(COMPID)
);

-- Insert test data for Component Management System
INSERT INTO COMPONENT_TYPE VALUES 
(1, 'AAA', 'Type AAA components'),
(2, 'BBB', 'Type BBB components'),
(3, 'CCC', 'Type CCC components');

INSERT INTO TYPE_ATTRIBUTES VALUES 
(1, 1, 'Weight', 'INTEGER', 1),
(1, 2, 'Color', 'STRING', 0),
(2, 1, 'Length', 'INTEGER', 1),
(2, 2, 'Material', 'STRING', 1),
(3, 1, 'Voltage', 'INTEGER', 0);

INSERT INTO COMPONENT VALUES 
(1, 'SN001', 1),
(2, 'SN002', 1),
(3, 'SN003', 2),
(4, 'SN004', 3),
(5, 'SN005', 2);

INSERT INTO COMPONENT_ATTRIBUTES VALUES 
(1, 1, '100'),
(1, 2, 'Red'),
(2, 1, '150'),
(3, 1, '200'),
(3, 2, 'Steel'),
(4, 1, '220');

INSERT INTO CONNECTION VALUES 
(1, 2), (1, 3), (3, 4);

INSERT INTO ASSEMBLY VALUES 
(2, 'Main Assembly'),
(3, 'Sub Assembly');

-- ============================================
-- 9. LIBRARY SYSTEM
-- ============================================

CREATE TABLE AUTHOR (
    [AUTHOR-ID] INT PRIMARY KEY,
    NAME VARCHAR(100)
);

CREATE TABLE BOOK (
    [BOOK-ID] INT PRIMARY KEY,
    TITLE VARCHAR(255),
    YEAR_PUBLISHED INT,
    NUM_AUTHORS INT,
    NUM_COPIES INT
);

CREATE TABLE BOOK_COPY (
    [BOOK-ID] INT,
    [INVENTORY-NUMBER] INT,
    STATUS VARCHAR(50),
    PRIMARY KEY ([BOOK-ID], [INVENTORY-NUMBER]),
    FOREIGN KEY ([BOOK-ID]) REFERENCES BOOK([BOOK-ID])
);

CREATE TABLE WROTE (
    [BOOK-ID] INT,
    [AUTHOR-ID] INT,
    PRIMARY KEY ([BOOK-ID], [AUTHOR-ID]),
    FOREIGN KEY ([BOOK-ID]) REFERENCES BOOK([BOOK-ID]),
    FOREIGN KEY ([AUTHOR-ID]) REFERENCES AUTHOR([AUTHOR-ID])
);

-- Insert test data for Library System
INSERT INTO AUTHOR VALUES 
(1, 'J.K. Rowling'),
(2, 'George Orwell'),
(3, 'Jane Austen'),
(4, 'Mark Twain'),
(5, 'Charles Dickens'),
(6, 'Virginia Woolf'),
(7, 'James Joyce');

INSERT INTO BOOK VALUES 
(1, 'Harry Potter and the Philosophers Stone', 1997, 1, 5),
(2, '1984', 1949, 1, 3),
(3, 'Pride and Prejudice', 1813, 1, 4),
(4, 'The Adventures of Tom Sawyer', 1876, 2, 6),
(5, 'Great Expectations', 1861, 3, 8),
(6, 'To the Lighthouse', 1927, 1, 2);

INSERT INTO BOOK_COPY VALUES 
(1, 1001, 'Available'), (1, 1002, 'Borrowed'), (1, 1003, 'Available'),
(2, 2001, 'Available'), (2, 2002, 'Damaged'), (2, 2003, 'Available'),
(3, 3001, 'Available'), (3, 3002, 'Borrowed'),
(4, 4001, 'Available'), (4, 4002, 'Available'), (4, 4003, 'Borrowed'),
(5, 5001, 'Available'), (5, 5002, 'Available'), (5, 5003, 'Borrowed');

INSERT INTO WROTE VALUES 
(1, 1), (2, 2), (3, 3), (4, 4), (4, 5), (5, 5), (5, 6), (5, 7), (6, 6);

-- ============================================
-- 10. HOTEL MANAGEMENT SYSTEM
-- ============================================

CREATE TABLE HOTEL (
    HOTELID INT PRIMARY KEY,
    NAME VARCHAR(100),
    ADDRESS VARCHAR(255),
    CITY VARCHAR(50),
    CLASS VARCHAR(10)
);

CREATE TABLE ROOMTYPE (
    ROOMTYPEID INT PRIMARY KEY,
    NAME VARCHAR(50),
    DESCRIPTION VARCHAR(255)
);

CREATE TABLE ROOM (
    HOTELID INT,
    ROOMNUMBER VARCHAR(10),
    ROOMTYPE INT,
    STATUS VARCHAR(20),
    PRIMARY KEY (HOTELID, ROOMNUMBER),
    FOREIGN KEY (HOTELID) REFERENCES HOTEL(HOTELID),
    FOREIGN KEY (ROOMTYPE) REFERENCES ROOMTYPE(ROOMTYPEID)
);

CREATE TABLE HOTEL_ROOMTYPE (
    HOTELID INT,
    ROOMTYPEID INT,
    TOTAL_ROOMS INT,
    PRICE DECIMAL(10,2),
    PRIMARY KEY (HOTELID, ROOMTYPEID),
    FOREIGN KEY (HOTELID) REFERENCES HOTEL(HOTELID),
    FOREIGN KEY (ROOMTYPEID) REFERENCES ROOMTYPE(ROOMTYPEID)
);

CREATE TABLE RESERVATION_HOTEL (
    HOTELID INT,
    SEQ_NUM INT,
    DATE_FROM DATE,
    DATE_TO DATE,
    ROOMTYPEID INT,
    STATUS VARCHAR(20),
    ROOMNUMBER VARCHAR(10),
    PRIMARY KEY (HOTELID, SEQ_NUM),
    FOREIGN KEY (HOTELID) REFERENCES HOTEL(HOTELID),
    FOREIGN KEY (ROOMTYPEID) REFERENCES ROOMTYPE(ROOMTYPEID)
);

-- Insert test data for Hotel Management System
INSERT INTO HOTEL VALUES 
(1, 'Grand Hotel', '123 Main Street', 'Paris', '4-star'),
(2, 'City Plaza', '456 Central Ave', 'Belgrade', '3-star'),
(3, 'Beach Resort', '789 Ocean Drive', 'Miami', '5-star'),
(4, 'Mountain View', '321 Hill Road', 'Denver', '3-star'),
(5, 'Business Center', '654 Corporate Blvd', 'New York', '4-star');

INSERT INTO ROOMTYPE VALUES 
(1, 'Single', 'Single occupancy room'),
(2, 'Double', 'Double occupancy room'),
(3, 'Triple', 'Triple occupancy room'),
(4, 'Suite', 'Luxury suite');

INSERT INTO ROOM VALUES 
(1, '101', 1, 'Available'), (1, '102', 2, 'Occupied'),
(2, '201', 1, 'Available'), (2, '202', 1, 'Available'), (2, '203', 2, 'Maintenance'),
(3, '301', 4, 'Available'), (3, '302', 2, 'Occupied'),
(4, '401', 1, 'Available'), (4, '402', 2, 'Available'),
(5, '501', 1, 'Available'), (5, '502', 2, 'Available');

INSERT INTO HOTEL_ROOMTYPE VALUES 
(1, 1, 25, 120.00), (1, 2, 30, 180.00), (1, 4, 5, 350.00),
(2, 1, 15, 80.00), (2, 2, 20, 120.00),
(3, 2, 40, 250.00), (3, 4, 10, 500.00),
(4, 1, 30, 90.00), (4, 2, 25, 140.00),
(5, 1, 35, 150.00), (5, 2, 25, 200.00);

INSERT INTO RESERVATION_HOTEL VALUES 
(1, 1, '2024-12-01', '2024-12-05', 1, 'Confirmed', '101'),
(2, 1, '2024-11-15', '2024-11-20', 2, 'Confirmed', '203'),
(3, 1, '2025-01-10', '2025-01-15', 4, 'Pending', NULL);

-- ============================================
-- 11. SUPPLY CHAIN SYSTEM
-- ============================================

CREATE TABLE SUPPLIER (
    SUP INT PRIMARY KEY,
    SNAME VARCHAR(100),
    STATUS VARCHAR(20),
    CITY VARCHAR(50)
);

CREATE TABLE CUSTOMER (
    CUS INT PRIMARY KEY,
    CNAME VARCHAR(100),
    CITY VARCHAR(50)
);

CREATE TABLE PRODUCT (
    PRD INT PRIMARY KEY,
    PNAME VARCHAR(100),
    COLOR VARCHAR(30),
    WEIGHT DECIMAL(8,2)
);

CREATE TABLE DELIVERY (
    SUP INT,
    PRD INT,
    CUS INT,
    DATE DATE,
    QUANTITY INT,
    PRIMARY KEY (SUP, PRD, CUS, DATE),
    FOREIGN KEY (SUP) REFERENCES SUPPLIER(SUP),
    FOREIGN KEY (PRD) REFERENCES PRODUCT(PRD),
    FOREIGN KEY (CUS) REFERENCES CUSTOMER(CUS)
);

-- Insert test data for Supply Chain System
INSERT INTO SUPPLIER VALUES 
(1, 'TechSupply Co', 'ACTIVE', 'Belgrade'),
(2, 'Global Parts', 'HIGH', 'Novi Sad'),
(3, 'Quality Components', 'ACTIVE', 'Nis'),
(4, 'Premium Electronics', 'ACTIVE', 'Kragujevac');

INSERT INTO CUSTOMER VALUES 
(1, 'Retail Chain A', 'Belgrade'),
(2, 'Electronics Store B', 'Novi Sad'),
(3, 'Tech Solutions C', 'Subotica'),
(4, 'Consumer Electronics D', 'Belgrade');

INSERT INTO PRODUCT VALUES 
(1, 'Philips TV 51', 'Black', 25.50),
(2, 'Samsung Monitor', 'Black', 8.20),
(3, 'Yellow Cable', 'Yellow', 0.50),
(4, 'Blue Connector', 'Blue', 0.30),
(5, 'Yellow Adapter', 'Yellow', 1.20);

INSERT INTO DELIVERY VALUES 
(1, 1, 1, '2025-02-15', 150),
(1, 1, 2, '2025-01-20', 75),
(2, 3, 3, '2000-08-15', 500),
(2, 5, 1, '2000-09-10', 200),
(3, 2, 4, '2025-03-01', 100),
(4, 1, 1, '2025-01-15', 50);

-- ============================================
-- 12. MEDICAL CLINIC SYSTEM
-- ============================================

CREATE TABLE CLINIC (
    CLINICID INT PRIMARY KEY,
    NAME VARCHAR(100),
    ADDRESS VARCHAR(255),
    CITY VARCHAR(50)
);

CREATE TABLE DOCTOR (
    DOCTORID INT PRIMARY KEY,
    GENDER CHAR(1),
    NAME VARCHAR(100),
    SPECIALTY VARCHAR(100),
    WORK_EXPERIENCE INT
);

CREATE TABLE WORKS_AT_CLINIC (
    DOCTORID INT,
    CLINICID INT,
    HOURS INT,
    PRIMARY KEY (DOCTORID, CLINICID),
    FOREIGN KEY (DOCTORID) REFERENCES DOCTOR(DOCTORID),
    FOREIGN KEY (CLINICID) REFERENCES CLINIC(CLINICID)
);

CREATE TABLE CHILD (
    DOCTORID INT,
    CHILDID INT,
    NAME VARCHAR(50),
    GENDER CHAR(1),
    AGE INT,
    GRADE INT,
    PRIMARY KEY (DOCTORID, CHILDID),
    FOREIGN KEY (DOCTORID) REFERENCES DOCTOR(DOCTORID)
);

-- Insert test data for Medical Clinic System
INSERT INTO CLINIC VALUES 
(1, 'City Medical Center', '123 Health St', 'Belgrade'),
(2, 'Suburban Clinic', '456 Care Ave', 'Novi Sad'),
(3, 'Specialized Medical', '789 Doctor Blvd', 'Nis');

INSERT INTO DOCTOR VALUES 
(1, 'M', 'Dr. Petar Petrovic', 'CARDIOLOGY', 15),
(2, 'F', 'Dr. Ana Nikolic', 'OPHTHALMOLOGIST', 12),
(3, 'M', 'Dr. Marko Jovanovic', 'PEDIATRICS', 8),
(4, 'F', 'Dr. Milica Stojanovic', 'OPHTHALMOLOGIST', 18),
(5, 'M', 'Dr. Stefan Milic', 'NEUROLOGY', 20);

INSERT INTO WORKS_AT_CLINIC VALUES 
(1, 1, 40), (1, 2, 20),
(2, 1, 35), (2, 3, 25),
(3, 2, 40),
(4, 1, 30), (4, 3, 35),
(5, 3, 45);

INSERT INTO CHILD VALUES 
(1, 1, 'Marko', 'M', 12, 6),
(1, 2, 'Ana', 'F', 8, 2),
(2, 1, 'Stefan', 'M', 15, 9),
(2, 2, 'Milica', 'F', 10, 4),
(2, 3, 'Jovana', 'F', 6, 1),
(4, 1, 'Petar', 'M', 14, 8),
(5, 1, 'Nikola', 'M', 16, 10);

-- ============================================
-- ADDITIONAL SYSTEMS FOR REMAINING TASKS
-- ============================================

-- Asset Inventory System
CREATE TABLE INVENTORY_COMMISSION (
    CommissionID INT PRIMARY KEY,
    CommissionChairman VARCHAR(100),
    NumberOfMembers INT
);

CREATE TABLE LOCATION (
    LocationID INT PRIMARY KEY,
    Name VARCHAR(100),
    LocationType VARCHAR(50),
    CHECK (LocationType IN ('PRODUCTION FACILITY', 'ADMINISTRATIVE BUILDINGS', 'AUXILIARY BUILDINGS'))
);

CREATE TABLE INVENTORY_LIST (
    ListID INT PRIMARY KEY,
    InventoryDate DATE,
    CommissionID INT,
    LocationID INT,
    FOREIGN KEY (CommissionID) REFERENCES INVENTORY_COMMISSION(CommissionID),
    FOREIGN KEY (LocationID) REFERENCES LOCATION(LocationID)
);

CREATE TABLE FIXED_ASSET (
    InventoryNumber INT PRIMARY KEY,
    Name VARCHAR(100),
    PurchaseDate DATE,
    PurchaseValue DECIMAL(12,2),
    DepreciatedValue DECIMAL(12,2),
    DepreciationGroup VARCHAR(50),
    CHECK (DepreciationGroup IN ('CONSTRUCTION BUILDINGS', 'EQUIPMENT', 'AUTOMOBILES', 'COMPUTER EQUIPMENT'))
);

CREATE TABLE INVENTORY_LIST_ITEM (
    ListID INT,
    Seq INT,
    InventoryQuantity INT,
    InventoryNumber INT,
    PRIMARY KEY (ListID, Seq),
    FOREIGN KEY (ListID) REFERENCES INVENTORY_LIST(ListID),
    FOREIGN KEY (InventoryNumber) REFERENCES FIXED_ASSET(InventoryNumber),
    CHECK (InventoryQuantity IN (0,1))
);

-- Insert test data for Asset Inventory System
INSERT INTO INVENTORY_COMMISSION VALUES 
(1, 'Milos Petrovic', 5),
(2, 'Ana Nikolic', 4),
(3, 'Marko Jovanovic', 6);

INSERT INTO LOCATION VALUES 
(1, 'Factory Floor A', 'PRODUCTION FACILITY'),
(2, 'Main Office', 'ADMINISTRATIVE BUILDINGS'),
(3, 'Warehouse B', 'AUXILIARY BUILDINGS'),
(4, 'Production Line 2', 'PRODUCTION FACILITY');

INSERT INTO FIXED_ASSET VALUES 
(1001, 'Industrial Machine A', '2020-01-15', 500000.00, 100000.00, 'EQUIPMENT'),
(1002, 'Office Building', '2015-06-01', 2000000.00, 800000.00, 'CONSTRUCTION BUILDINGS'),
(1003, 'Company Car', '2022-03-10', 80000.00, 30000.00, 'AUTOMOBILES'),
(1004, 'Server Rack', '2021-09-15', 150000.00, 75000.00, 'COMPUTER EQUIPMENT'),
(1005, 'Laptop Dell', '2023-01-20', 120000.00, 120000.00, 'COMPUTER EQUIPMENT');

INSERT INTO INVENTORY_LIST VALUES 
(1, '2004-12-31', 1, 1),
(2, '2007-12-31', 2, 1),
(3, '2008-12-31', 3, 2);

INSERT INTO INVENTORY_LIST_ITEM VALUES 
(1, 1, 1, 1001), (1, 2, 1, 1003),
(2, 1, 0, 1001), (2, 2, 1, 1004),
(3, 1, 1, 1002), (3, 2, 1, 1005);

-- Book Fair System
CREATE TABLE PUBLISHER (
    PublisherCode INT PRIMARY KEY,
    Name VARCHAR(100),
    HallNumber INT
);

CREATE TABLE LITERATURE_TYPE (
    LiteratureTypeCode INT PRIMARY KEY,
    LiteratureTypeName VARCHAR(100)
);

CREATE TABLE BOOK_FAIR (
    BookCode INT PRIMARY KEY,
    Title VARCHAR(255),
    Circulation INT,
    Price DECIMAL(10,2),
    FairDiscount DECIMAL(5,2),
    DiscountApprovalDate DATE,
    LiteratureTypeCode INT,
    PublisherCode INT,
    FOREIGN KEY (LiteratureTypeCode) REFERENCES LITERATURE_TYPE(LiteratureTypeCode),
    FOREIGN KEY (PublisherCode) REFERENCES PUBLISHER(PublisherCode)
);

CREATE TABLE AUTHOR_FAIR (
    AuthorCode INT PRIMARY KEY,
    NameSurname VARCHAR(100),
    Country VARCHAR(50)
);

CREATE TABLE WROTE_FAIR (
    AuthorCode INT,
    BookCode INT,
    PRIMARY KEY (AuthorCode, BookCode),
    FOREIGN KEY (AuthorCode) REFERENCES AUTHOR_FAIR(AuthorCode),
    FOREIGN KEY (BookCode) REFERENCES BOOK_FAIR(BookCode)
);

CREATE TABLE DAILY_SALES_FAIR (
    BookCode INT,
    Date DATE,
    NumberOfCopies INT,
    PRIMARY KEY (BookCode, Date),
    FOREIGN KEY (BookCode) REFERENCES BOOK_FAIR(BookCode)
);

-- Insert test data for Book Fair System
INSERT INTO PUBLISHER VALUES 
(1, 'Narodna knjiga', 101),
(2, 'Laguna', 102),
(3, 'Vulkan', 103);

INSERT INTO LITERATURE_TYPE VALUES 
(1, 'PROFESSIONAL LITERATURE'),
(2, 'FICTION'),
(3, 'SCIENCE');

INSERT INTO AUTHOR_FAIR VALUES 
(1, 'Professor Petrovic', 'Serbia'),
(2, 'Milan Kundera', 'Czech Republic'),
(3, 'Stephen King', 'USA'),
(4, 'Ivo Andric', 'Bosnia');

INSERT INTO BOOK_FAIR VALUES 
(1, 'Advanced Programming', 5000, 2500.00, 15.00, '2024-09-08', 1, 1),
(2, 'The Unbearable Lightness of Being', 8000, 1800.00, NULL, NULL, 2, 2),
(3, 'The Shining', 12000, 2200.00, 20.00, '2024-09-07', 2, 3),
(4, 'Database Systems', 3000, 3200.00, NULL, NULL, 1, 1);

INSERT INTO WROTE_FAIR VALUES 
(1, 1), (1, 4), (2, 2), (3, 3), (4, 1);

INSERT INTO DAILY_SALES_FAIR VALUES 
(1, '2024-09-01', 50), (1, '2024-09-02', 30),
(2, '2024-09-01', 120), (2, '2024-09-02', 95),
(3, '2024-09-01', 200), (3, '2024-09-02', 150),
(4, '2024-09-01', 15), (4, '2024-09-02', 8);

-- Theater System
CREATE TABLE SEASON (
    SEASON_NAME VARCHAR(20) PRIMARY KEY,
    DATE_FROM DATE,
    DATE_TO DATE
);

CREATE TABLE PLAY (
    PLAYID INT PRIMARY KEY,
    NAME VARCHAR(100),
    TYPE VARCHAR(20),
    CHECK (TYPE IN ('COMEDY', 'DRAMA', 'TRAGEDY'))
);

CREATE TABLE REPERTOIRE (
    PLAYID INT,
    DATE_TIME DATETIME,
    STATUS VARCHAR(20),
    PRIMARY KEY (PLAYID, DATE_TIME),
    FOREIGN KEY (PLAYID) REFERENCES PLAY(PLAYID),
    CHECK (STATUS IN ('PERFORMED', 'CANCELLED', 'PLANNED'))
);

CREATE TABLE ACTOR (
    ACTORID INT PRIMARY KEY,
    NAME VARCHAR(50),
    SURNAME VARCHAR(50)
);

CREATE TABLE CAST (
    PLAYID INT,
    ACTORID INT,
    SEASON_NAME VARCHAR(20),
    ROLE VARCHAR(100),
    PRIMARY KEY (PLAYID, ACTORID, SEASON_NAME),
    FOREIGN KEY (PLAYID) REFERENCES PLAY(PLAYID),
    FOREIGN KEY (ACTORID) REFERENCES ACTOR(ACTORID),
    FOREIGN KEY (SEASON_NAME) REFERENCES SEASON(SEASON_NAME)
);

-- Insert test data for Theater System
INSERT INTO SEASON VALUES 
('2005/2006', '2005-09-01', '2006-06-30'),
('2006/2007', '2006-09-01', '2007-06-30'),
('2008/2009', '2008-09-01', '2009-06-30'),
('2009/2010', '2009-09-01', '2010-06-30');

INSERT INTO PLAY VALUES 
(1, 'Much Ado About Nothing', 'COMEDY'),
(2, 'Hamlet', 'TRAGEDY'),
(3, 'Candles', 'DRAMA'),
(4, 'Romeo and Juliet', 'TRAGEDY'),
(5, 'The Importance of Being Earnest', 'COMEDY');

INSERT INTO ACTOR VALUES 
(1, 'Petar', 'Petrovic'),
(2, 'Ana', 'Nikolic'),
(3, 'Marko', 'Jovanovic'),
(4, 'Milica', 'Stojanovic'),
(5, 'Stefan', 'Milic');

INSERT INTO REPERTOIRE VALUES 
(1, '2006-03-15 19:30:00', 'PERFORMED'),
(1, '2006-04-20 19:30:00', 'PERFORMED'),
(2, '2006-02-10 20:00:00', 'PERFORMED'),
(3, '2025-06-15 19:30:00', 'PLANNED'),
(3, '2025-06-20 19:30:00', 'PERFORMED'),
(4, '2009-12-25 19:30:00', 'PERFORMED'),
(5, '2009-11-15 19:30:00', 'CANCELLED');

INSERT INTO CAST VALUES 
(1, 1, '2006/2007', 'Benedick'),
(1, 2, '2006/2007', 'Beatrice'),
(2, 3, '2005/2006', 'Hamlet'),
(3, 4, '2009/2010', 'Olivia'),
(3, 5, '2008/2009', 'Oscar'),
(4, 1, '2009/2010', 'Romeo'),
(5, 2, '2009/2010', 'Gwendolen');

-- Real Estate System
CREATE TABLE PARTNER (
    PARTNER_CODE INT PRIMARY KEY,
    NAME VARCHAR(100),
    CITY VARCHAR(50)
);

CREATE TABLE BUYER (
    BUYER_CODE INT PRIMARY KEY,
    NAME VARCHAR(50),
    SURNAME VARCHAR(50),
    BIRTH_YEAR INT,
    CITY VARCHAR(50)
);

CREATE TABLE BUILDING (
    BUILDING_CODE INT PRIMARY KEY,
    STATUS VARCHAR(30),
    NUM_FLOORS INT,
    LOCATION VARCHAR(100),
    INVESTOR_PARTNER_CODE INT,
    CONTRACTOR_PARTNER_CODE INT,
    FOREIGN KEY (INVESTOR_PARTNER_CODE) REFERENCES PARTNER(PARTNER_CODE),
    FOREIGN KEY (CONTRACTOR_PARTNER_CODE) REFERENCES PARTNER(PARTNER_CODE),
    CHECK (STATUS IN ('UNDER_CONSTRUCTION', 'COMPLETED'))
);

CREATE TABLE APARTMENT (
    BUILDING_CODE INT,
    APARTMENT_CODE INT,
    FLOOR INT,
    AREA DECIMAL(6,2),
    PRICE_PER_M2 DECIMAL(8,2),
    NUM_ROOMS INT,
    PRIMARY KEY (BUILDING_CODE, APARTMENT_CODE),
    FOREIGN KEY (BUILDING_CODE) REFERENCES BUILDING(BUILDING_CODE)
);

CREATE TABLE PURCHASE (
    BUILDING_CODE INT,
    APARTMENT_CODE INT,
    BUYER_CODE INT,
    CONTRACT_DATE DATE,
    PAYMENT_DATE DATE,
    PRIMARY KEY (BUILDING_CODE, APARTMENT_CODE, BUYER_CODE),
    FOREIGN KEY (BUILDING_CODE, APARTMENT_CODE) REFERENCES APARTMENT(BUILDING_CODE, APARTMENT_CODE),
    FOREIGN KEY (BUYER_CODE) REFERENCES BUYER(BUYER_CODE)
);

-- Insert test data for Real Estate System
INSERT INTO PARTNER VALUES 
(1, 'Belgrade Construction', 'Belgrade'),
(2, 'Premium Builders', 'Belgrade'),
(3, 'Modern Invest', 'Belgrade'),
(4, 'Quality Contractors', 'Novi Sad');

INSERT INTO BUYER VALUES 
(1, 'Marko', 'Petrovic', 1995, 'Belgrade'),
(2, 'Ana', 'Nikolic', 1998, 'Belgrade'),
(3, 'Stefan', 'Jovanovic', 1990, 'Belgrade'),
(4, 'Milica', 'Stojanovic', 1985, 'Novi Sad');

INSERT INTO BUILDING VALUES 
(1, 'UNDER_CONSTRUCTION', 10, 'Belgrade Center', 1, 2),
(2, 'COMPLETED', 8, 'Belgrade South', 3, 4),
(3, 'UNDER_CONSTRUCTION', 15, 'Belgrade North', 1, 2);

INSERT INTO APARTMENT VALUES 
(1, 1, 5, 65.50, 2500.00, 2),
(1, 2, 8, 120.00, 2800.00, 4),
(2, 1, 3, 85.20, 2200.00, 3),
(3, 1, 10, 95.00, 2600.00, 3);

INSERT INTO PURCHASE VALUES 
(1, 1, 1, '2024-07-15', '2024-08-15'),
(1, 2, 2, '2024-08-20', NULL),
(2, 1, 3, '2007-11-10', '2008-01-15');

