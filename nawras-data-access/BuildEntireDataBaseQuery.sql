use NawrasDB;



create table continents
(

continent_id smallint IDENTITY(1,1) PRIMARY KEY,
continent_name nvarchar(100) UNIQUE  not null

);


create table countries
(

country_id smallint IDENTITY(1,1) PRIMARY KEY,
continent_id smallint not null references continents(continent_id),
country_name nvarchar(255) UNIQUE not null


);

create table cities
(

city_id int IDENTITY(1,1) PRIMARY KEY,
country_id smallint not null references countries(country_id),
city_name nvarchar(255),


);

create table marriage_statuses
(
marriage_status_id smallint IDENTITY(1,1) PRIMARY KEY,
marriage_status_name nvarchar(10) not null,

)

create table people
(

person_id int IDENTITY(1,1) PRIMARY KEY,
first_name nvarchar(255),
father_name nvarchar(255),
grandfather_name nvarchar(255),
last_name nvarchar(255),
national_id int not null  UNIQUE ,
date_of_birth date not null,
gender bit not null,
country_id smallint not null references countries(country_id),
city_id int not null references cities(city_id),
address_district nvarchar(500) not null,
email nvarchar(254) not null UNIQUE ,
phone nvarchar (30) not null UNIQUE ,
personal_image_path nvarchar(max) not null,
marriage_status_id smallint not null references marriage_statuses(marriage_status_id),
created_at datetime2 not null,

)
create table employees
(
employee_id int not null IDENTITY(1,1) PRIMARY KEY,
person_id int not null references people(person_id),
salary smallmoney not null,
job nvarchar(500) not null,
created_at datetime2 not null,

)

create table system_users
(
system_user_id int not null IDENTITY(1,1) PRIMARY KEY,
employee_id int not null references employees(employee_id),
username nvarchar(200) not null,
password_hash nvarchar(300) not null,
is_active bit not null,
persmession int not null,
created_at datetime2 not null,

)

create table standard_users
(
standard_user_id int not null IDENTITY(1,1) PRIMARY KEY,
person_id int not null references people(person_id),
username nvarchar(255) not null,
password_hash nvarchar(255) not null,
is_active bit not null,
permession int not null,
created_at datetime

)

create table tourists
(

tourist_id int not null IDENTITY(1,1) PRIMARY KEY,
person_id int not null references people(person_id),
standard_user_id int not null references standard_users(standard_user_id),
passport_number nvarchar(30) not null,
created_at datetime2

)


create table captains
(
captain_id int not null IDENTITY(1,1) PRIMARY KEY,
person_id int not null references people(person_id),
standard_user_id int not null references standard_users(standard_user_id),
started_sailing date not null,
license_number nvarchar(50) not null,
rating smallint not null,
trips_completed int not null,
is_available bit not null,
created_at datetime2 not null,

)



create table owners
(
owner_id int not null IDENTITY(1,1) PRIMARY KEY,
person_id int not null references people(person_id),
standard_user_id int not null references standard_users(standard_user_id),

is_business bit not null,
created_at datetime2,
number_of_assets int not null,

)





create table vessel_types
(
vessel_type_id int not null IDENTITY(1,1) PRIMARY KEY,
vessel_max_capacity int not null,
vessel_name nvarchar(500),
vessel_description nvarchar(max),
has_cabins bit not null, 

)

create table vessels
(
vessel_id int not null IDENTITY(1,1) PRIMARY KEY, 
vessel_type_id int not null references vessel_types(vessel_type_id),
rating smallint not null,
listing_date datetime2 not null,
created_at datetime2 not null,
representative_owner_id int not null references owners(owner_id),

)



create table trip_statuses
(

trip_status_id smallint not null IDENTITY(1,1) PRIMARY KEY,
trips_status_name nvarchar(80)

)

create table trips
(
trip_id int not null IDENTITY(1,1) PRIMARY KEY, 
trip_name nvarchar(500) not null,
trip_start_destination nvarchar(500),
trip_end_destination nvarchar(500),
trip_return_destination nvarchar(500),
vessel_id int not null references vessels(vessel_id),
trip_start_date datetime2 not null,
trip_end_date datetime2 not null,
average_rating smallint not null,
trip_price smallmoney not null,
trip_representative_owner_id int not null references owners(owner_id),
trip_status_id smallint not null references trip_statuses(trip_status_id),
created_at datetime2 not null

)

create table bill_types
(
bill_type_id smallint not null IDENTITY(1,1) PRIMARY KEY,
bill_type_name nvarchar(255),

)

create table booking_statuses
(
booking_status_id smallint not null IDENTITY(1,1) PRIMARY KEY,
booking_status_name nvarchar(50) not null,

)

create table bookings 
(
booking_id int not null IDENTITY(1,1) PRIMARY KEY,
trip_booking_price smallmoney not null,
tourist_id int not null references tourists(tourist_id),
trip_id int not null references trips(trip_id),
bill_type_id smallint not null references bill_types(bill_type_id),
booking_status_id smallint not null references booking_statuses(booking_status_id),
created_at datetime2

)

create table bill_payment_statuses
(
bill_payment_status_id smallint not null IDENTITY(1,1) PRIMARY KEY,
bill_payment_status_name nvarchar(100) not null,

)

create table generated_bills
(
generated_bill_id int not null IDENTITY(1,1) PRIMARY KEY,
generated_bill_fees smallmoney not null,
bill_type_id smallint not null references bill_types(bill_type_id),
booking_id int not null references bookings(booking_id),
generated_date datetime2 not null,
bill_payment_status_id smallint not null references bill_payment_statuses(bill_payment_status_id),
expected_payment_date datetime2 not null,
is_paid bit not null,
created_at datetime2

)

create table payment_statuses
(
payment_status_id smallint not null IDENTITY(1,1) PRIMARY KEY,
payment_status_name nvarchar(150) not null,

)

create table payment_methods
(
payment_method_id smallint not null IDENTITY(1,1) PRIMARY KEY,
payment_status_name nvarchar(150) not null,

)

create table payments
(
payment_id int not null IDENTITY(1,1) PRIMARY KEY,
payment_amonut smallmoney not null,
paid_by_tourist_id int not null references tourists(tourist_id),
generated_bill_id int not null references generated_bills(generated_bill_id),
recorded_by_system_user_id int not null references system_users(system_user_id),
received_by_vessel_owner_id int not null references owners(owner_id),
payment_date datetime2 not null,
payment_method_id smallint not null references payment_methods(payment_method_id),
payment_status_id smallint not null references payment_statuses(payment_status_id),
created_at datetime2 not null,

)

