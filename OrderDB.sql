drop database if exists OrderDB;

create database OrderDB;

use OrderDB;

create table Customers(
	customer_id int auto_increment primary key,
    customer_name varchar(100) not null,
    customer_address varchar(200),
    customer_phone varchar(50)
);
insert into Customers(customer_name, customer_address, customer_phone) values	
('Nguyen Thi X','Hai Duong', 0311111111),
('Nguyen Van N','Hanoi', 0322222222),
('Nguyen Van B','Ho Chi Minh', 0333333333),
('Nguyen Van B','Hanoi', 0344444444),
('Nguyen Van s','Hanoi', 0355555555);
-- drop table customers;

create table Staffs(
	Staff_id int auto_increment primary key,
    Staff_name varchar(100) not null,
    Password1 varchar(50)
);
insert into Staffs(Staff_name, Password1) values ('nguyen','827CCB0EEA8A706C4C34A16891F84E7B'), ('staff1','827CCB0EEA8A706C4C34A16891F84E7B'), ('hai','827CCB0EEA8A706C4C34A16891F84E7B');

create table Tabaccos(
	Tabacco_id int auto_increment primary key,
    amount decimal not null default 0,
    Manufactory  varchar(200) not null,
    Tabacco_Price decimal not null,
    Tabacco_name varchar(100) not null,
    Tabacco_pack int,
    Tabacco_date datetime default now() not null
);
create index idx_name_product on Tabaccos(Tabacco_name);
create index idx_price_product on Tabaccos(Tabacco_Price);

insert into Tabaccos(Tabacco_name, Manufactory, Tabacco_Price, amount, Tabacco_pack, Tabacco_date) values
('Tabacco 1', 'Company Thang Long', 12500, 8, 50, 20220302),
('Tabacco 2', 'Company Bac Son', 62500, 6, 5342, 20230502),
('Tabacco 3', 'Company Thanh Hoa', 30000, 1012, 7, 20220308),
('Tabacco 4', 'Company Long An', 24500, 5, 232, 20220802),
('Tabacco 5', 'Company Cuu Long', 75000, 9, 1123, 20210302),
('Tabacco 6', 'Company Cuu Long', 77000, 10, 13213, 20210302);
select Tabacco_name, Manufactory, Tabacco_Price, amount, Tabacco_pack, Tabacco_date  from Tabaccos;

create table Orders(
	Order_id int auto_increment primary key,
	Customer_id int,
	Customer_name varchar(100),
    Seller_id int,
    Order_data datetime default now() not null,
    Order_status varchar(50) not null,
    
    foreign key (Customer_id) references Customers(customer_id),
    
    foreign key (Seller_id) references Staffs(Staff_id)
);
UPDATE Orders o
JOIN Customers c ON o.Customer_id = c.customer_id
SET o.Customer_id = c.customer_id
WHERE o.Order_id > 0;
insert into Orders(Customer_name, order_status) values
	('Nguyen Thi X','Unpaid'), ('Nguyen Van N','Unpaid'), ('Nguyen Van B','Processing'), ('Nguyen Van B','Processing'),('Nguyen Van s','Paid');
create table OrderDetails(
	Order_id int not null,
    Tabacco_id int(50) not null,
    Quantity int not null,
    foreign key (Order_id) references Orders(Order_id),
    foreign key (Tabacco_id) references Tabaccos(Tabacco_id)

);
insert into OrderDetails(Order_id, Tabacco_id,  Quantity) values
	(1, 1, 5123), (1, 3, 1132), (1, 4, 23213),
    (2, 2, 1132), (2, 3, 2132), (2, 5, 4132);

create user if not exists 'nguyen'@'localhost' identified by 'vtcacademy';
grant all privileges on orderdb.* to 'nguyen'@'localhost';
