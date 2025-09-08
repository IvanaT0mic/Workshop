# SQL Tasks - Database Query Exercises

## 1. Insurance System

Given relational schema:

- POLICYHOLDER(PERSONID, SURNAME, NAME, ADDRESS, CITY)
- INSURANCE_TYPE(TYPEID, NAME, TARIFF)
- POLICY(POLICYID, ISSUE_DATE, TOTAL_AMOUNT, PERSONID, INSURANCE_TYPEID, NUM_MONTHLY_INSTALLMENTS, PREMIUM_CLASS, REPLACEMENT_POLICYID)
- PAYMENT_BY_POLICY(POLICYID, PAYMENT_DATE, AMOUNT)

Write SQL:1999 commands that implement the following requirements:

a) Display all data about issued vehicle insurance policies (insurance type name = "VEHICLE INSURANCE") in the current month sorted by premium class.

b) Delete all policies for which there was no payment after three months from the policy issue date.

c) Create a view that displays by years, and within them by months, the total number of issued policies, total value of policies, for all types of property and life insurance (these are insurance types with names "PROPERTY INSURANCE", "LIFE INSURANCE") from 2000 onwards.

d) Create a view "DISPUTED*CLAIMS" that displays POLICY_NUMBER, POLICY_DATE, TOTAL_LIABILITY, TOTAL_PAID, NUMBER_OF_APPROVED*

## 2. Airport System

Given relational schema:

- AIRPORT(AEROID, NAME, COUNTRY, CITY)
- ROUTE(ROUTEID, DEPARTURE_AERO, DESTINATION_AERO)
- FLIGHT(ROUTEID, DATE_TIME, PLANEID)
- AIRCRAFT(PLANEID, DESIGNATION, AIRCRAFT_TYPE, NUM_SEATS)
- RESERVATION(ROUTEID, DATE_TIME, SEQ_NUM, STATUS)

Write SQL:1999 commands that:

a) Postpone all flights to Zurich on November 15, 2003 (update the STATUS attribute to 'Postponed').

b) Display all direct flights from GREAT BRITAIN to GERMANY: date, time, route number, departure airport name, destination airport name sorted by departure date and time for all flights in the second quarter of the current year.

c) Create a REALIZATION view that displays total realization for all routes in the current year. Display RouteNumber, NumberOfCompletedFlights, TotalNumberOfPassengers, TotalCapacities, AverageFlightOccupancy.

## 3. Project Management System

Given relational schema:

- PROJECT(PROJ_CODE, PROJECT_NAME, BUDGET, PROJECT_LEADER)
- ENGAGEMENT(WORKER_CODE, PROJ_CODE)
- WORKER(WORKER_CODE, WORKER_NAME, BIRTH_DATE, HIRE_DATE, SALARY, COMPANY_CODE, IS_MANAGER)
- COMPANY(COMPANY_CODE, NAME, CITY)

Write SQL:1999 commands that:

a) Display all data about workers who completed 10 years of service in the current year and are engaged on at least 2 projects. OR

b) Display all data about workers who have a salary greater than 30,000 dinars and are engaged on at least one project.

c) Create a MANAGER view (CompanyName, WorkerName, WorkerCode, HireDate) that displays for all companies the names of managers who are not engaged on any project.

d) Display age structure of workers by cities where they work (number of workers under 20 years, number of workers between 20 and 45 years, and number of workers over 45 years).

## 4. University System

Given relational schema:

- UNIVERSITY(UniversityCode, UniversityName)
- FACULTY(FacultyCode, FacultyName, UniversityCode)
- PROFESSOR(SSN, Name, Surname, HireDate, HomeFaculty)
- ENGAGEMENT(SSN, FacultyCode)
- PAYMENT(SSN, FacultyCode, PaymentDate, Amount)

Write SQL:1999 commands that:

a) Display Surname, Name, YearsOfService, FacultyName for professors employed at a university with the name "Belgrade" in the last 10 years. Sort the result in descending order of years of service and ascending surname.

b) Display for all universities the faculties that have the number of employed professors between 50 and 100, sorted in descending order within universities.

c) Create a FEES view (Surname, Name, NumberOfFaculties, Month, TotalMonthlyPayment) that displays monthly payments to professors in the previous year who are engaged outside their home faculty. The NumberOfFaculties column shows the number of different faculties that paid fees to the professor in the given month.

## 5. Student Admission System

Given relational schema:

- PLACE(PlaceCode, PlaceName)
- SCHOOL(SchoolCode, SchoolName, SchoolType, PlaceCode)
- CANDIDATE(SSN, Surname, ParentName, Name, BirthDate, OverallSuccess, CompletedSchool, ApplicationDate, ApplicationNumber)
- CERTIFICATE(SSN, Year, Average, SchoolCode)
- GRADE(SSN, Year, Seq, SubjectName, Grade)

SchoolType in ('Agricultural', 'Technical', 'Gymnasium', 'other')
Grade in (2,3,4,5)

Write SQL:1999 commands that:

a) Display surname, name, school name, overall success and application number of all candidates who completed a technical school and applied on June 25, 2003. Sort results in ascending order of surname.

b) Create a GeneralSuccess view (Success, NumberOfCandidates, AverageSuccess) that displays achieved success of candidates (excellent, very good, good, sufficient), number of candidates, average success of those candidates. Consider only certificates from the fourth year (final year).

c) Create a SuccessByPlaces view (PlaceName, Success, NumberOfCandidates) that displays for each place the number of candidates for the specified school successes (excellent, very good, good, sufficient).

## 6. Football League System

Given relational schema:

- FOOTBALL_TEAM(TEAMID, NAME, CITY)
- PLAYER(PLAYERID, NAME_SURNAME, BIRTH_DATE, TEAMID)
- MATCH(ROUND_NUM, PAIR_NUM, HOME_TEAM, AWAY_TEAM, DATE, TOTAL_HOME, TOTAL_AWAY)
- MATCH_PLAYER(ROUND_NUM, PAIR_NUM, PLAYERID, MINUTES_PLAYED)
- MATCH_STATS(ROUND_NUM, PAIR_NUM, TIME, PLAYERID, EVENT)

(Note: EVENT attribute can contain the following values: "GOAL", "OWN GOAL", "YELLOW CARD", "RED CARD". TIME attribute is of type INTERVAL MINUTE TO SECOND.)

Write SQL:1999 commands that:

a) Display statistical data about players of football club "PARTIZAN" for the match played in the 4th round (display minutes played for all club players). OR a) Display data about players of football team "Real Madrid" who did NOT play in the 7th round match.

b) Display the best scorers by rounds played (display round number, player name_surname, team name and number of goals scored). The best scorer is the player who scored the most goals in the match (there can be multiple such players).

c) Create a "LEAGUE_TABLE" view containing Team_Name, TotalPoints, ScoredGoals, ConcededGoals, Goal_Difference, MatchesPlayed, which displays the football team name, points earned based on match results (if match result is a draw, teams get 1 point each, winner gets 3 points, loser gets 0 points), data on total scored and conceded goals, goal difference, and number of matches played.

## 7. Public Transportation System

Given relational schema:

- VEHICLE(VehicleCode, VehicleType)
- ROUTE(RouteNumber, StartStation, EndStation)
- STATION(StationCode, StationName)
- ROUTE_STATIONS(RouteNumber, StationCode, SEQ_NUM)
- DEPARTURE(VehicleCode, RouteNumber, DateTime, Status)

VehicleType in ('Bus', 'Tram', 'Trolleybus')
Status in ('Successful', 'Cancelled', 'Delayed')
SEQ_NUM in route defines the order of stations on the route

Write SQL:1999 commands that:

a) Display VehicleCode, VehicleType, Date and Time on route 18, between 13:00 and 17:00 hours for April 1st and 7th, 2003 where the departure was cancelled (Status attribute).

b) Create a DailyDepartureSchedule view (RouteNumber, VehicleType, StartStationName, EndStationName, TotalVehicleCount) that displays planned daily vehicle departures for the current date.

c) Display all data about the route, day and number of departures, for the route that had the fewest departures from the set of maximally realized routes by days in January of the current year. The maximally realized route for a specific day is the one with the highest number of realized departures.

d) Provide an overview by quarters, then by vehicle types, total number of planned departures, number of successful departures, number of cancelled departures and number of delayed departures for 2002.

## 8. Component Management System

Given relational schema:

- COMPONENT_TYPE(TYPEID, NAME, DESCRIPTION)
- TYPE_ATTRIBUTES(TYPEID, ATTRIBUTEID, NAME, DOMAIN, REQUIRED)
- COMPONENT(COMPID, SERIALNUMBER, COMPONENTTYPE)
- COMPONENT_ATTRIBUTES(COMPID, ATTRIBUTEID, VALUE)
- CONNECTION(PARENT_COMPID, CHILD_COMPID)
- ASSEMBLY(COMPONENTID, NAME)

Write SQL:1999 commands that:

a) Display all components and names and values of their required attributes that belong to component type named "AAA".

b) Provide average, minimum and maximum values of all attributes with domain "INTEGER" for all component types.

c) Create an INCORRECT_ATTRIBUTES view (COMPID, ATTRIBUTEID, ATTRIBUTE_NAME, ERROR) that displays all incorrectly entered attributes for each component. If a component is assigned an attribute that is not defined for the component type to which the component belongs, enter 'N/A' in the ERROR field. If a required attribute is not assigned to a component, enter 'REQUIRED' in the ERROR field.

d) Insert into the ASSEMBLY relation all "primary" components that are not in the relation. Primary components are those that are not built into any assembly.

## 9. Library System

Given relational schema:

- BOOK(BOOK-ID, TITLE, YEAR_PUBLISHED, NUM_AUTHORS, NUM_COPIES)
- BOOK_COPY(BOOK-ID, INVENTORY-NUMBER, STATUS)
- AUTHOR(AUTHOR-ID, NAME)
- WROTE(BOOK-ID, AUTHOR-ID)

Write SQL:1999 commands that:

a) For all authors, display the number of books they wrote. Sort the result by number of books.

b) Display titles of books written by an odd number of authors and which exist in a larger number of copies (use data on number of copies from BOOK_COPY relation, use data on number of authors from WROTE relation).

c) Check if the value of the NUM_AUTHORS attribute of the BOOK relation equals the number of tuples in the WROTE relation. If the attribute value is correct, the query should display the book title and text "Correct number of authors", otherwise the book title and text "Incorrect number of authors".

## 10. Hotel Management System

Given relational schema:

- HOTEL(HOTELID, NAME, ADDRESS, CITY, CLASS)
- ROOMTYPE(ROOMTYPEID, NAME, DESCRIPTION)
- ROOM(HOTELID, ROOMNUMBER, ROOMTYPE, STATUS)
- HOTEL_ROOMTYPE(HOTELID, ROOMTYPEID, TOTAL_ROOMS, PRICE)
- RESERVATION(HOTELID, SEQ_NUM, DATE_FROM, DATE_TO, ROOMTYPEID, STATUS, ROOMNUMBER)

Write SQL:1999 commands that:

a) Display the following data about hotel capacities (hotelName, roomTypeName, numberOfRooms and roomPrice) for "3-star" hotels that have at least 2 single rooms. Sort the query result in descending order of "roomTypeName" and within them in ascending order of room prices.

b) Create a view that displays for all cities the number of hotels by categories. The view contains the following attributes: cityName, hotelClass, totalNumberOfHotels, totalNumberOfRooms.

c) Based on room data (ROOM relation), update the capacities of all hotels (attribute "TOTAL_ROOMS" of "HOTEL_ROOMTYPE" relation).

## 11. Supply Chain System

Given relational schema:

- SUPPLIER(SUP, SNAME, STATUS, CITY)
- CUSTOMER(CUS, CNAME, CITY)
- PRODUCT(PRD, PNAME, COLOR, WEIGHT)
- DELIVERY(SUP, PRD, CUS, DATE, QUANTITY)

Write SQL commands that:

a) Display customer names and received quantities of yellow products for deliveries in 2000.

b) Create an EXCHANGE view with attributes SNAME, SCITY, CNAME, CCITY that displays supplier name, supplier city, customer name and customer city, for suppliers and customers who had trade exchange in the current year but are not from the same city.

c) Display names, cities and quantities for suppliers who delivered more than 100 pieces of product 'Philips TV 51' in the first quarter of the current year. Display the result in descending order of quantities.

d) Write a program in C (exceptionally in pseudo code) that sets the STATUS attribute value to "HIGH" for suppliers who had more than 100 deliveries in the first quarter of the current year. (Note: The relational system supports only single-table queries.)

## 12. Medical Clinic System

Given relational model:

- CLINIC(CLINICID, NAME, ADDRESS, CITY)
- DOCTOR(DOCTORID, GENDER, NAME, SPECIALTY, WORK_EXPERIENCE)
- WORKS_AT_CLINIC(DOCTORID, CLINICID, HOURS)
- CHILD(DOCTORID, CHILDID, NAME, GENDER, AGE, GRADE)

Write SQL commands that:

a) Display all data about doctors who work at multiple clinics and have work experience greater than 10 years.

b) Display Doctor name, number of male children, number of female children and total number of children for each doctor whose specialty is "OPHTHALMOLOGIST".

c) Create a CLINIC_AVERAGE view with attributes Clinic, Name, Min_Hours, Average_Hours, Max_Hours that displays clinic ID, clinic name and minimum, average and maximum number of hours of engagement for doctors who work exclusively at those clinics.

## 13. Project Management System (Extended)

Given relational schema:

- PROJECT(PROJ_CODE, PROJECT_NAME, BUDGET, PROJECT_LEADER)
- ENGAGEMENT(WORKER_CODE, PROJ_CODE)
- WORKER(WORKER_CODE, WORKER_NAME, BIRTH_DATE, HIRE_DATE, SALARY, COMPANY_CODE, IS_MANAGER)
- COMPANY(COMPANY_CODE, NAME, CITY)

Write SQL:1999 commands that:

a) Display all data about workers who completed 10 years of service in the current year and are engaged on at least 4 projects.

b) Display age structure of workers by cities where they work (number of workers under 20 years, number of workers between 20 and 50 years, and number of workers over 50 years).

c) Create a MANAGER view (CompanyName, ManagerFullName, HireDate, Salary) that displays for all companies all managers whose first and last name starts with letter 'M' and ends with 'Ć' and have a salary greater than 45,000 dinars, and manage at least one project with at least 30 engaged workers.

## 14. Extended Airport System

Given relational schema:

- AIRPORT(AEROID, NAME, COUNTRY, CITY)
- ROUTE(ROUTEID, DEPARTURE_AERO, DESTINATION_AERO)
- FLIGHT(ROUTEID, DATE_TIME, PLANEID)
- AIRCRAFT(PLANEID, DESIGNATION, AIRCRAFT_TYPE, NUM_SEATS)
- RESERVATION(ROUTEID, DATE_TIME, SEQ_NUM, STATUS)

STATUS in {'OK', 'CANCELLED'}

Write SQL:1999 commands that:

a) Display all data about aircraft that have more than 200 seats, on all flights on October 5, 2004 except on flights to "NEW YORK" and "LONDON".

b) Display all direct flights from SWITZERLAND to SPAIN: date, time, route number, departure airport name, destination airport name sorted by departure date and time for all flights in the second quarter of the current year.

c) Create a REALIZATION view that displays total realization for all routes in the current year. Display RouteNumber, NumberOfCompletedFlights, TotalNumberOfPassengers, TotalCapacities, AverageFlightOccupancy.

## 15. Extended Hotel System

Given relational schema:

- HOTEL(HOTELID, NAME, ADDRESS, CITY, CLASS)
- ROOMTYPE(ROOMTYPEID, NAME, DESCRIPTION)
- ROOM(HOTELID, ROOMNUMBER, ROOMTYPE, STATUS)
- HOTEL_ROOMTYPE(HOTELID, ROOMTYPEID, TOTAL_ROOMS, PRICE)
- RESERVATION(HOTELID, SEQ_NUM, DATE_FROM, DATE_TO, ROOMTYPEID, STATUS, ROOMNUMBER)

Write SQL:1999 commands that:

a) Display the following data (hotelName, hotelAddress and singleRoomPrice) of all "4-star" hotels in the city "Paris" that have at least 50 rooms of type "Single" but do not have rooms of type "Triple". Sort the query result in ascending order of singleRoomPrice.

b) Display the following data about hotel capacities (hotelName, roomTypeName, numberOfRooms and roomPrice) for "3-star" hotels that have at least 10 double rooms. Sort the query result in descending order of "roomTypeName" and within them in ascending order of room prices.

c) Create a view that displays for all cities the number of hotels by categories. The view contains the following attributes: cityName, hotelClass, totalNumberOfHotels, totalNumberOfRooms.

## 16. Payroll System

Given relational schema:

- INDIVIDUAL(INDIVIDUALID, SURNAME, NAME, ADDRESS, PLACE, SSN, STATUS)
- PAYMENT_TYPE(TYPEID, NAME)
- LIABILITY_TYPE(LIABILITYID, LIABILITY_NAME)
- CALCULATION(CALCID, PAYMENT_DATE, INDIVIDUALID, TYPEID, DATE_FROM, DATE_TO, NET_AMOUNT)
- CALCULATION_LIABILITY(CALCID, LIABILITYID, LIABILITY_BASE, LIABILITY_RATE, LIABILITY_AMOUNT, INCLUDED_IN_GROSS)

INCLUDED_IN_GROSS in {'YES', 'NO'}

Write SQL:1999 commands that:

a) Display data about paid wages (payment type name is 'WAGES') to employees (individuals with status 'EMPLOYED') whose name starts with letter 'C'. Sort result by surname and name of employee.

b) Display SSN, surname, name, TotalLiabilities, TotalGross, TotalNet, NumberOfPayments for all individuals whose total gross amount (TotalGross) is greater than 500,000.00 dinars. TotalLiabilities represents the net amount of all liabilities, and TotalGross consists of net amount increased by the amount of calculated liabilities that are included in gross.

c) Create a TAX_RETURN view (type, SSN, surname, name, net, base, tax, pension, health, unemployment) that displays for each individual the SSN, surname and name of individual, total paid net amount, base is the sum of bases on which only tax was calculated, as well as total amounts of individual liabilities: tax, pension, health, unemployment. The type attribute should display 'WORKER' if the individual's status is 'EMPLOYED' or 'PENSIONER', and 'THIRD PARTIES' in all other cases. The tax return should contain data only for 2003.

## 17. Procurement System

Given relational schema:

- OFFER(OfferID, Date, CompanyName, Phone, PaymentPeriod, CashDiscount, TenderID)
- OFFER_ITEM(OfferID, Seq, Manufacturer, Price, WarrantyPeriod, EquipmentID)
- PROCUREMENT_TENDER(TenderID, DateFrom, DateTo)
- TENDER_ITEM(TenderID, Seq, Quantity, EquipmentID)
- EQUIPMENT_TYPE(EquipmentID, EquipmentName)

Write SQL:1999 commands that:

a) Display data about offers and offer items (offer code, date, company name, equipment manufacturer name, offered equipment price, equipment name and warranty period) for all offers in the current year from companies that approve a payment period longer than one year or approve a cash discount of at least 5%. Payment period is expressed in months.

b) Display offer code, company name and payment terms for companies that offer products from multiple different manufacturers for equipment type 'COMPUTER EQUIPMENT'.

c) Create a TENDER_OVERVIEW view (EQUIPMENT_CODE, EQUIPMENT_NAME, TOTAL_QTY, YEAR) that displays for all equipment types, by years: equipment code, name and total quantity procured through tenders in that year. Display data for 2004 and 2005 (consider tender closing date). Display data also for equipment types that were not procured in that period.

OR

a) Display all data about offers sent in the previous year, with payment period up to two years and approved cash discount below 10%. Consider only offers sent for procurement tenders that lasted longer than 20 days. Sort result from latest to earliest offer date.

b) Display offer code and company name that sent the offer, where the offer for equipment type 'COMPUTER EQUIPMENT' includes products from multiple different manufacturers, and the price of that equipment is between 15,000 and 80,000 (excluding discount).

## 18. HR Management System

Given relational schema:

- EMPLOYEE(EmployeeCode, Name, Surname, PersonalNum, BirthDate, Gender, EducationCode)
- JOB_POSITION(PositionCode, PositionName, EducationCode)
- ENGAGEMENT(EmployeeCode, PositionCode, DateFrom, DateTo, StatusCode)
- WORK_STATUS(StatusCode, StatusName)
- EDUCATION_PROFILE(EducationCode, EducationLevel, ProfileName)

Gender in {'M','F'}
EducationLevel in {'V','VI','VII'}
StatusName in {'probationary work', 'internship', 'fixed-term contract', 'permanent contract', 'service contract'}

Write SQL:1999 commands that:

a) Display all data about employees (code, name, surname, personal number, education profile name) for employees who have at least 'VI' level of education and who will not acquire the right to retirement based on age in the current or next year (in the current or next year they turn: Men-65, Women-58).

b) Display code, job position name and education profile name required for the job position that had the fewest engaged workers.

c) Create a STATISTICS view (PROFILE_CODE, PROFILE_NAME, EMP_2004, EMP_2005, TREND, PERCENTAGE) that displays for all profile codes, number of people employed in 2005, number of people employed in 2004, description of observed trend 'GROWTH', 'DECLINE' or 'NO_CHANGE' and percentage difference between these two years.

OR

a) Display all data about employees (code, name, surname, personal number, education profile name) for employees who have 'V' level of education and have been on probationary work for more than 6 months.

b) Display code, job position name and education profile name required for the job position that had the highest worker turnover. Position turnover is defined by the number of engagements where the departure date (DateTo) is known, i.e., current engagements of employees at that position are not counted.

OR

a) Display all data about employees (code, name, surname, personal number, education profile name) for employees who have 'IV' level of education and have been on probationary work for less than 2 months.

b) Display code, job position name and education profile name required for the job position where worker turnover is exactly 5. Position turnover is defined by the number of engagements where the departure date (DateTo) is known, i.e., current engagements of employees at that position are not counted. Display only education profile names that start with 'S' and are no longer than 10 characters.

## 19. Asset Inventory System

Given relational schema:

- INVENTORY_COMMISSION(CommissionID, CommissionChairman, NumberOfMembers)
- LOCATION(LocationID, Name, LocationType)
- INVENTORY_LIST(ListID, InventoryDate, CommissionID, LocationID)
- INVENTORY_LIST_ITEM(ListID, Seq, InventoryQuantity, InventoryNumber)
- FIXED_ASSET(InventoryNumber, Name, PurchaseDate, PurchaseValue, DepreciatedValue, DepreciationGroup)

LocationType in {'PRODUCTION FACILITY', 'ADMINISTRATIVE BUILDINGS', 'AUXILIARY BUILDINGS'}
DepreciationGroup in {'CONSTRUCTION BUILDINGS', 'EQUIPMENT', 'AUTOMOBILES', 'COMPUTER EQUIPMENT'}
InventoryQuantity in {0,1}

Write SQL:1999 commands that:
a) Display location codes that are of type 'PRODUCTION FACILITY' and where according to the 2004 inventory there is no recorded shortage (inventory quantity is one for each item at that location).
b) Display by locations and within them by depreciation groups the total purchase value of fixed assets, total depreciated value and number of assets that have completely depreciated value. Display location codes, location names and depreciation group names. Display in descending order by location code.
c) Display workers who were chairmen of inventory commissions in the last two years at locations that in that year were considered locations with the lowest total purchase value of fixed assets.

OR

a) Display location codes that are of type 'PRODUCTION FACILITY' and where according to the 2007 inventory there is shortage (inventory quantity for at least one item at that location is zero).
b) Display by locations and within them by depreciation groups the minimum purchase value of fixed assets. Display location codes, location names and depreciation group names. Display in descending order by location code.
c) Display all fixed assets of depreciation group "COMPUTER EQUIPMENT" that are in the inventory list for 2008 but are not in the inventory list for 2007.

## 20. Book Fair System

Given relational schema:

- PUBLISHER(PublisherCode, Name, HallNumber)
- BOOK(BookCode, Title, Circulation, Price, FairDiscount, DiscountApprovalDate, LiteratureTypeCode)
- DAILY_SALES(BookCode, Date, NumberOfCopies)
- LITERATURE_TYPE(LiteratureTypeCode, LiteratureTypeName)
- AUTHOR(AuthorCode, NameSurname, Country)
- WROTE(AuthorCode, BookCode)

Write SQL:1999 commands that:

a) Display all data about books that belong to the type 'PROFESSIONAL LITERATURE', written by multiple authors, one of whom is professor 'Petrovic'.

b) From the current date until the end of the fair, approve (update) a 20% discount for the worst-selling edition of publishing house "Narodna knjiga". The worst-selling book is the one with the smallest percentage ratio of total sold copies and circulation.

c) Create a DISCOUNTED_BOOKS view with columns (PublisherName, LiteratureTypeName, TotalSoldCopies, TotalSalesRevenue, TotalApprovedDiscount, AverageDiscountAmount) that displays by publishers at the fair and for them by literature types total number of sold copies, total sales revenue, total amount the publisher lost due to approved discount and average discount percentage. Display data only for books that are on discount.

## 21. Extended Book Fair System

Given relational schema:

- PUBLISHER(PublisherCode, Name, HallNumber)
- BOOK(BookCode, Title, Circulation, Price, FairDiscount, DiscountApprovalDate, LiteratureTypeCode, PublisherCode)
- DAILY_SALES(BookCode, Date, NumberOfCopies)
- LITERATURE_TYPE(LiteratureTypeCode, LiteratureTypeName)
- AUTHOR(AuthorCode, NameSurname, Country)
- WROTE(AuthorCode, BookCode)

Write SQL:1999 commands that:

a) Display data about books: title, author name and surname, price, discounted price (if available, otherwise display full price), publisher name and hall number, for all books that are not on discount or were given discount on the current day.

b) Display the best-selling book from the set of worst-selling books by days.

c) Create a DISCOUNTED_BOOKS view with columns (PublisherName, LiteratureTypeName, SoldCopiesWithoutDiscount, SoldCopiesWithDiscount, TotalSalesRevenue, TotalApprovedDiscount, AverageDiscountAmount) that displays for all publishers at the fair, by all literature types publisher name, literature type name, number of sold copies without discount, number of sold copies with discount, total sales revenue (with and without discount), total amount the publisher lost due to approved discount and average discount percentage.

## 22. Extended Asset Inventory System

Given relational schema:

- InventoryCommission(CommissionID, CommissionChairman, NumberOfMembers)
- Location(LocationID, Name, LocationType)
- InventoryList(ListID, InventoryDate, CommissionID, LocationID)
- InventoryListItem(ListID, Seq, InventoryQuantity, InventoryNumber)
- FixedAsset(InventoryNumber, Name, PurchaseDate, PurchaseValue, DepreciatedValue, DepreciationGroup)

LocationType in {'PRODUCTION FACILITY', 'ADMINISTRATIVE BUILDINGS', 'AUXILIARY BUILDINGS'}
DepreciationGroup in {'CONSTRUCTION BUILDINGS', 'EQUIPMENT', 'AUTOMOBILES', 'COMPUTER EQUIPMENT'}
InventoryQuantity in {0,1}

Write SQL:1999 commands that:

a) Display all locations where according to the 2004 inventory there is no recorded shortage (inventory quantity is one for each item at that location).

b) Display workers who were chairmen of inventory commissions in the last two years at locations that in that year were considered locations with the highest total purchase value of fixed assets.

c) Create a InventoriedAssetValue view (LocationCode, LocationName, DepreciationGroup, TotalPurchaseValue, TotalDepreciatedValue, AvgDepreciationRate, TotalValueOfMissingInventory) that displays by all locations and all depreciation groups: location code, location name, depreciation group, total purchase value of assets, total depreciated value, average percentage of depreciated value (percentage ratio of depreciated and purchase value) and total current value of assets (difference between purchase and depreciated) that were not found at the corresponding location.

## 23. Film Festival System

Given relational schema:

- CINEMA(CinemaID, CinemaName)
- HALL(CinemaID, HallID, NumberOfSeats)
- FILM(FilmID, FilmName, DirectorName, Duration, Country, Year, Awards)
- FESTIVAL_PROGRAM(ProgramID, OpeningDate, ClosingDate, Editor)
- SCREENING(ProgramID, Seq, DateTime, TicketPrice, NumberOfVisitors, CinemaID, HallID, FilmID)

Write SQL:1999 commands that:

a) Display film name, year, author name, festival year, number of viewers, cinema name and hall name for all screenings of films by author named 'Pedro Almodovar' whose start time is from 12:00 to 18:00h, and attendance is greater than 80%.

b) Display all data about films that were shown on the last days of the festival, but are not films with which festivals were closed.

c) Create a FILM_VIEWERSHIP view (production, total_visitors, total_revenue, max_attendance) that displays for American production or co-production films: total number of visitors, total revenue from ticket sales and highest attendance percentage of screenings from all films of that production. If it's an American film, display 'United States of America' in the production field. If it's a co-production, display 'Co-production' in the production field (country attribute is in the form USA, USA/FRA/GB, ESP/USA, etc.).

## 24. Extended Film Festival System

Given relational schema:

- CINEMA(CINEMAID, CINEMA_NAME)
- HALL(CINEMAID, HALLID, NUMBER_OF_SEATS)
- FILM(FILMID, FILM_NAME, DIRECTOR_NAME, COUNTRY, YEAR, NUMBER_OF_AWARDS)
- FESTIVAL_PROGRAM(PROGRAMID, OPENING_DATE, CLOSING_DATE, EDITOR)
- SCREENING(PROGRAMID, SEQ, DATE_TIME, TICKET_PRICE, CINEMAID, HALLID, FILMID, NUMBER_OF_SOLD_TICKETS)

Write SQL:1999 commands that:

a) Display cinema name, hall name, film name, and director name for the film that opened FEST 2005. (It is assumed that there was only one screening at the earliest time on the festival opening day).

b) Increase ticket prices for all FEST 2006 screenings in 'Sava Centar' cinema halls, in time slots from 8 PM to midnight, for all award-winning films. For screenings in the large hall, ticket prices should be increased by 150 dinars, and for screenings in the small hall by 100 dinars. (Hall names are 'LARGE HALL' and 'SMALL HALL', respectively).

c) Display data about the film with the lowest recorded attendance from the set of most-watched films by days of the FEST-2004 festival.

## 25. Theater System

Given relational schema:

- SEASON(SEASON_NAME, DATE_FROM, DATE_TO)
- PLAY(PLAYID, NAME, TYPE)
- REPERTOIRE(PLAYID, DATE_TIME, STATUS)
- ACTOR(ACTORID, NAME, SURNAME)
- CAST(PLAYID, ACTORID, SEASON_NAME, ROLE)

STATUS values can be "PLANNED", "PERFORMED" or "CANCELLED".

Write SQL:1999 commands that:

a) Display all actors who in season "2006/2007" did not perform in the play "Much Ado About Nothing".

b) Display names of all plays that were performed exactly once in season "2005/2006".

c) Create a view that displays PLAY NAME, TOTAL NUMBER OF PERFORMED PLAYS, TOTAL NUMBER OF CANCELLED PLAYS that were performed the most times up to the current date, and the percentage of cancelled in relation to the total number of plays is less than 10 percent.

OR

a) Display name, date and time of all dramas that are on the repertoire in June of the current year. Sort the result in ascending order of date and time.

b) Display the name of the play that has the most actors in its cast in season 2009/2010.

c) Create a COMEDY_REPERTOIRE_REALIZATION view (YEAR, NUM_PERFORMED_COMEDIES, NUM_CANCELLED_COMEDIES, SUCCESS_PERCENTAGE) that displays for each calendar year (not season!!!) the number of performed comedies, number of cancelled comedies and the ratio of performed comedies to the total number of comedies planned in the repertoire. Display data only for years in which at least 20 comedy performances were planned in the repertoire. Round success percentage to two decimal places.

## 26. Extended Theater System

Given relational schema:

- SEASON(SEASON_NAME, DATE_FROM, DATE_TO)
- PLAY(PLAYID, NAME, TYPE)
- REPERTOIRE(PLAYID, DATE_TIME, STATUS)
- ACTOR(ACTORID, NAME, SURNAME)
- CAST(PLAYID, ACTORID, SEASON_NAME, ROLE)

STATUS in ('PERFORMED', 'CANCELLED')
TYPE in ('COMEDY', 'DRAMA', 'TRAGEDY')

Write SQL:1999 commands that:

a) Display actor's surname and name, as well as season name in which they played a role in the play "Candles" whose role name starts with the letter O. Sort the result in descending order of season name and ascending order of actor's surname.

b) Display code and name of the play that appears least frequently on the repertoire in the current year.

c) Create an ACTOR_OVERVIEW view (ACTORID, SURNAME, NAME, NUM_PLAYS_08_09, NUM_PLAYS_09_10) that displays for each actor the code, surname, name, number of plays in whose casts they were in season "2008/2009" and number of plays in whose casts they were in season "2009/2010". Consider only actors who have been in casts of more than 20 different plays in their career and where the number of comedies in whose casts they were in season "2009/2010" is greater than the number of comedies in whose casts they were in season "2008/2009".

## 27. Sports Club System

Given relational schema:

- SPORTS_ASSOCIATION(AssociationCode, Name, Address, FoundingDate)
- CLUB(ClubCode, Name, Address, FoundingDate, AssociationCode)
- COACH(CoachCode, Name, Surname, HireDate, HomeClub)
- ENGAGEMENT(ClubCode, CoachCode, Date, Hours)
- PAYMENT(PaymentCode, Date, Amount, ClubCode, CoachCode)

Write SQL:1999 commands that:

a) Display Surname, Name, YearsOfService, Club Name for coaches employed in the sports association named "Atleta" in the last 5 years. Sort the result in descending order of years of service and ascending surname.

b) Display for all sports associations the clubs that have the number of employed coaches between 5 and 10, and who were engaged only in their home club.

c) Create a FEES view (Surname, Name, NumberOfClubs, Month, TotalMonthlyPayment) that displays monthly payments to coaches in the previous year who were engaged outside their home club. The NumberOfClubs column shows the number of different clubs that paid fees to the coach in the given month.

## 28. Medical Center System

Given relational schema:

- DOCTOR(DoctorCode, Name, Surname, HireDate, HomeCenter)
- ENGAGEMENT(CenterCode, OfficeCode, DoctorCode, Date, Hours)
- MEDICAL_CENTER(CenterCode, Name, Address, FoundingDate)
- OFFICE(CenterCode, OfficeCode, Name, Address, FoundingDate)
- PAYMENT(PaymentCode, Date, Amount, CenterCode, OfficeCode, DoctorCode)

Write SQL:1999 commands that:

a) Display Surname, Name, YearsOfService, Office Name for doctors employed in the center named "Medicus" and who were engaged in the last 5 years. Sort the result in ascending order of years of service and descending name.

b) Display for all medical centers the offices that have the number of employed doctors less than 5 and who were engaged only in their home center.

c) Create a FEES view (Surname, Name, NumberOfCenters, Month, TotalMonthlyPayment) that displays monthly payments to doctors in the current year who were engaged outside their home center. The NumberOfCenters column shows the number of different offices that paid fees to doctors in the previous month.

## 29. Research Institute System

Given relational schema:

- SEMINAR(SEMINAR_CODE, SEMINAR_NAME, BUDGET, SEMINAR_LEADER)
- PARTICIPATION(RESEARCHER_CODE, SEMINAR_CODE)
- RESEARCHER(RESEARCHER_CODE, RESEARCHER_NAME, BIRTH_DATE, HIRE_DATE, SALARY, INSTITUTE_CODE, IS_MANAGER)
- INSTITUTE(INSTITUTE_CODE, NAME, CITY)

Write SQL:1999 commands that:

a) Display all data about researchers who completed exactly 5 years of service in the current year and participated in more than 60 seminars.

b) Display age structure of researchers by cities where they work (number of researchers under 22 years, number of researchers between 20 and 45 years, and number of researchers over 50 years).

c) Create a MANAGER view (InstituteName, ResearcherName, ResearcherCode, HireDate) that displays for all institutes the names of managers who participated in more than 10 seminars in the current year.

## 30. Music Production System

Given relational schema:

- PRODUCTION_HOUSE(ProductionCode, Name, HallNumber)
- ALBUM(AlbumCode, Title, Circulation, Price, FairDiscount, DiscountApprovalDate, GenreCode, ProductionCode)
- DAILY_SALES(AlbumCode, Date, NumberOfCopies)
- MUSIC_GENRE(GenreCode, GenreName)
- COMPOSER(ComposerCode, NameSurname, Country)
- COMPOSED(ComposerCode, AlbumCode)

Write SQL:1999 commands that:

a) Display data about albums: title, production house name, circulation and price for all albums whose circulation is greater than 50,000 or whose circulation is less than 50,000 but discount is greater than 10%.

OR

a) Display data about albums: title, composer name and surname, price, discounted price (if available, otherwise display full price), production house name and hall number, for all albums that are not on discount or were given discount on the current day.

b) Display the best-selling album from the set of worst-selling albums by days.

c) Create a DISCOUNTED_ALBUMS view with columns (ProductionHouseName, GenreName, SoldCopiesWithoutDiscount, SoldCopiesWithDiscount, TotalSalesRevenue, TotalApprovedDiscount, AverageDiscountAmount) that displays for all production houses at the fair, by all music genres production house name, music genre name, number of sold albums without discount, number of sold albums with discount, total sales revenue (with and without discount), total amount the production house lost due to approved discount and average discount percentage.

## 31. Software Company System

Given relational schema:

- SOFTWARE_COMPANY(SoftwareCompanyCode, Name, HallNumber, Country)
- SOFTWARE(SoftwareCode, Name, Price, FairDiscount, DiscountApprovalDate, TypeCode, SoftwareCompanyCode)
- DAILY_SALES(SoftwareCode, Date, NumberOfInstallations)
- SOFTWARE_TYPE(SoftwareTypeCode, Name)
- PROGRAMMER(ProgrammerCode, NameSurname, Country, HireDate, SoftwareCompanyCode)
- IMPLEMENTED(ProgrammerCode, SoftwareCode)

Write SQL:1999 commands that:

a) Display data about software: name, price, discounted price (fair discount is expressed as a percentage), software company name and hall number, for all software whose discount was approved in the current month and whose software name contains the phrase ERP.

b) Display the name and company name of the worst-selling software from the set of best-selling software by months.

c) Create a DISCOUNTED_SOFTWARE view with columns (SoftwareCompanyName, SoftwareTypeName, SoldInstallationsWithoutDiscount, SoldInstallationsWithDiscount, TotalSalesRevenue, TotalApprovedDiscount, AverageDiscountAmount) that displays for all software companies from Serbia exhibiting at the fair, by all software types, software company name, software name, number of sold installations without discount, number of sold installations with discount, total sales revenue (with and without discount), total amount the software company lost due to approved discount and average discount percentage.

## 32. Real Estate System

Given relational schema:

- PARTNER(PARTNER_CODE, NAME, CITY)
- BUYER(BUYER_CODE, NAME, SURNAME, BIRTH_YEAR, CITY)
- BUILDING(BUILDING_CODE, STATUS, NUM_FLOORS, LOCATION, INVESTOR_PARTNER_CODE, CONTRACTOR_PARTNER_CODE)
- APARTMENT(BUILDING_CODE, APARTMENT_CODE, FLOOR, AREA, PRICE_PER_M2, NUM_ROOMS)
- PURCHASE(BUILDING_CODE, APARTMENT_CODE, BUYER_CODE, CONTRACT_DATE, PAYMENT_DATE)

STATUS IN ('UNDER_CONSTRUCTION', 'COMPLETED')

Write SQL:1999 commands that implement the following requirements:

a) Display buyer name, area, price and apartment category for all purchased apartments under construction in Belgrade bought by buyers under 30 years old. If apartment has area greater than 100 m2, apartment category is LUX, otherwise it is STANDARD.

b) Display all data about contractors who were engaged in construction of only those buildings whose all apartments were sold in the last two months.

c) Create a STATISTICS view with columns (INVESTOR_CODE, NAME, REVENUE2007, REVENUE2008, TREND) that displays for each investor the observed trend of revenue change in 2008 compared to 2007. Possible trend values are: 'GROWTH', 'DECLINE', 'NO CHANGE'. Revenue is calculated based on apartment sales. Apartment price equals the product of area and price per square meter.

## 33. Football Management System

Given relational schema:

- TEAM(TeamID, Name, City)
- PLAYER(PlayerID, Name, Surname, BirthDate, Position)
- ENGAGEMENT(PlayerID, TeamID, DateFrom, DateTo)
- MATCH(MatchID, Date, HomeGoals, AwayGoals, HomeTeamID, AwayTeamID)
- PLAYER_STATISTICS(MatchID, PlayerID, TeamID, DateFrom, Goals, TotalShots, ShotsOnTarget, TotalPasses, SuccessfulPasses)

Position in {'goalkeeper', 'defense', 'midfield', 'attack'}

Write SQL:1999 commands that implement the following requirements:

a) Display name and surname, as well as team name, of all attackers over 28 years old who are currently engaged in a team whose name starts with a vowel. Sort the result in descending order of attacker's surname.

b) Display code and name of the team with the lowest player turnover, i.e., the team with the smallest number of player departures. Player turnover is defined by the number of engagements where the player's departure date (DateTo) is known, i.e., players currently in the team are not considered.

c) Create a HOME_STATISTICS view (TEAMID, TEAM_NAME, TOTAL_MATCHES, WINS, DRAWS, LOSSES, TOTAL_GOALS_SCORED, TOTAL_GOALS_CONCEDED) that displays for each team the code, name, total number of matches played, number of wins, draws and losses, as well as total goals scored and conceded, considering only matches the team played at home in the current year.

OR

a) Display name, surname, birth date and team name of players currently playing in London teams, in midfield position and have been in the team for at least one year.

b) For each player, display name, surname, position, name of the club where they started their career, and how many years they spent in that club (note that the player may still be playing in the club where they started their career).

OR

a) Display all data about players who are under 25 years old and joined Real Madrid during the current year. Sort the result in ascending order of the position the player plays and descending order of player's name.

b) Display teams (teamid and team name) that have more than 3 attackers who have been in the club for more than one year.

c) Create an ATTACKER_STATISTICS view (PLAYERID, SURNAME, NAME, TEAM_NAME, TOTAL_GOALS, AVERAGE_GOALS_PER_MATCH, SHOT_ON_TARGET_PERCENTAGE, REALIZATION_PERCENTAGE) that displays for each team where the attacker played the attacker's code, surname, name, team name, total goals scored, average goals scored per match, percentage of shots that went on target (relative to total shots), as well as realization percentage, i.e., what percentage of total shots resulted in a goal.
