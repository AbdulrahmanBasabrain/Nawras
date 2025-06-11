use NawrasDB;


create table continents
(

continent_id smallint not null,
continent_name nvarchar(100) not null

primary key (continent_id)
);


create table countries
(

country_id smallint not null,
continent_id smallint not null references continents(continent_id),
country_name nvarchar(255)

primary key (country_id)
);

create table cities
(

city_id int not null,
country_id smallint not null references countries(country_id),
city_name nvarchar(255),

primary key(city_id)
);

create table marriage_statuses
(
marriage_status_id smallint not null,
marriage_status_name nvarchar(10) not null,

primary key (marriage_status_id)
)

create table people
(

person_id int not null,
first_name nvarchar(255),
father_name nvarchar(255),
grandfather_name nvarchar(255),
last_name nvarchar(255),
national_id int not null,
date_of_birth date not null,
gender bit not null,
country_id smallint not null references countries(country_id),
city_id int not null references cities(city_id),
address_district nvarchar(500) not null,
email nvarchar(254) not null,
phone nvarchar (30) not null,
personal_image_path nvarchar(max) not null,
marriage_status_id smallint not null references marriage_statuses(marriage_status_id),
created_at datetime2 not null,

primary key (person_id)
)

create table employees
(
employee_id int not null,
person_id int not null references people(person_id),
salary smallmoney not null,
job nvarchar(500) not null,
created_at datetime2 not null,

primary key (employee_id)
)

create table system_users
(
system_user_id int not null,
employee_id int not null references employees(employee_id),
username nvarchar(200) not null,
password_hash nvarchar(300) not null,
is_active bit not null,
persmession int not null,
created_at datetime2 not null,

primary key (system_user_id)
)

create table tourists
(

tourist_id int not null,
person_id int not null references people(person_id),
passport_number nvarchar(30) not null,
created_at datetime

primary key (tourist_id)
)

create table user_tourists
(
user_tourist_id int not null,
tourist_id int not null references tourists(tourist_id),
username nvarchar(255) not null,
password_hash nvarchar(255) not null,
is_active bit not null,
permession int not null,
created_at datetime

primary key (user_tourist_id)
)

create table captains
(
captain_id int not null,
person_id int not null references people(person_id),
started_sailing date not null,
license_number nvarchar(500) not null,
rating smallint not null,
trips_completed int not null,
is_available bit not null,
created_at datetime2 not null,

primary key (captain_id)
)

create table user_captains
(
user_captain_id int not null,
captain_id int not null references captains(captain_id),
username nvarchar(255) not null,
password_hash nvarchar(255) not null,
is_active bit not null,
permession int not null,
created_at datetime

primary key (user_captain_id)
)

create table owners
(
owner_id int not null,
person_id int not null references people(person_id),
is_business bit not null,
created_at datetime,
number_of_assets int not null,

primary key (owner_id)
)

create table businesses 
(
business_id int not null,
representative_owner_id int not null references owners(owner_id),
business_name nvarchar(255) not null,
created_at datetime2

primary key (business_id)
)

create table vessel_types
(
vessel_type_id int not null,
vessel_max_capacity int not null,
vessel_name nvarchar(500),
vessel_description nvarchar(max),
has_cabins bit not null, 

primary key (vessel_type_id)
)

create table vessels
(
vessel_id int not null, 
vessel_type_id int not null references vessel_types(vessel_type_id),
rating smallint not null,
listing_date datetime2 not null,
created_at datetime2 not null,
representative_owner_id int not null references owners(owner_id),

primary key (vessel_id)
)

create table user_owners
(
user_owner_id int not null,
owner_id int not null references owners(owner_id),
username nvarchar(255) not null,
password_hash nvarchar(255) not null,
is_active bit not null,
permession int not null,
created_at datetime

primary key (user_owner_id)
)

create table ownerships
(
ownership_id int not null,
owner_id int not null references owners(owner_id),
vessel_id int not null references vessels(vessel_id),
created_at datetime not null

primary key(ownership_id)
)

create table trip_statuses
(

trip_status_id int not null,
trips_status_name nvarchar(80)

primary key (trip_status_id),
)

create table trips
(
trip_id int not null, 
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
trip_status_id int not null references trip_statuses(trip_status_id),
created_at datetime2 not null

primary key(trip_id)
)

create table bill_types
(
bill_type_id smallint not null,
bill_type_name nvarchar(255),

primary key (bill_type_id)
)

create table booking_statuses
(
booking_status_id smallint not null,
booking_status_name nvarchar(50) not null,

primary key(booking_status_id)
)

create table bookings 
(
booking_id int not null,
trip_booking_price smallmoney not null,
touris_id int not null references tourists(tourist_id),
bill_type_id smallint not null references bill_types(bill_type_id),
booking_status_id smallint not null references booking_statuses(booking_status_id),
created_at datetime2

primary key (booking_id)
)

create table bill_payment_statuses
(
bill_payment_status_id smallint not null,
bill_payment_status_name nvarchar(100) not null,

primary key (bill_payment_status_id)
)

create table generated_bills
(
generated_bill_id int not null,
generated_bill_fees smallmoney not null,
bill_type_id smallint not null references bill_types(bill_type_id),
booking_id int not null references bookings(booking_id),
generated_date datetime2 not null,
bill_payment_status_id smallint not null references bill_payment_statuses(bill_payment_status_id),
expected_payment_date datetime2 not null,
is_paid bit not null,
created_at datetime2

primary key(generated_bill_id)
)

create table payment_statuses
(
payment_status_id smallint not null,
payment_status_name nvarchar(150) not null,

primary key (payment_status_id)
)

create table payments
(
payment_id int not null,
payment_amonut smallmoney not null,
paid_by_tourist_id int not null references tourists(tourist_id),
generated_bill_id int not null references generated_bills(generated_bill_id),
recorded_by_system_user_id int not null references system_users(system_user_id),
received_by_vessel_owner_id int not null references owners(owner_id),
payment_date datetime2 not null,
payment_method nvarchar(255) not null,
payment_status_id smallint not null references payment_statuses(payment_status_id),
created_at datetime2 not null,

primary key (payment_id)
)