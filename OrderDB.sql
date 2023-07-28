-- drop database if exists OrderDB;

create database OrderDB;

use OrderDB;

create table Customers(
	customer_id int auto_increment primary key,
    customer_name varchar(100) not null,
    customer_address varchar(200),
    customer_phone varchar(50)
);
-- drop table customers;

create table Staffs(
	Staff_id int auto_increment primary key,
    Staff_name varchar(100) not null,
    Password1 varchar(50)
);
create table Manufactorys(
	Manufactory_id int auto_increment primary key,
    Manufactory_addres varchar(100),
    Manufactory_website varchar(100)
);
create table Tabaccos(
	Tabacco_id int auto_increment primary key,
    amount int not null default 0,
    Manufactory_id int ,
    Tabacco_Price int,
    Tabacco_name varchar(50),
    Tabacco_pack int,
    Tabacco_date datetime default now() not null,
    foreign key (Manufactory_id) REFERENCES Manufactorys(Manufactory_id)
);
create table Orders(
	Order_id int auto_increment primary key,
    Seller_id int,
    Accountant_id int,
    Customer_id int,
    Order_data datetime default now() not null,
    Order_status int,
    foreign key (Accountant_id) REFERENCES Staffs(Staff_id)
);
create table OrderDetails(
	Order_id int not null,
    Tabacco_id int not null,
    Unit_Price decimal(20,2) not null,
    Quantity int not null default 1,
    constraint pk_OrderDetails primary key(order_id, Tabacco_id),
	constraint fk_OrderDetails_Orders foreign key(order_id) references Orders(order_id),
	constraint fk_OrderDetails_Tabaccos foreign key(Tabacco_id) references Tabaccos(Tabacco_id)
);

-- delimiter $$
-- create trigger tg_before_insert before insert
-- 	on Tabaccos for each row
--     begin
-- 		if new.amount < 0 then
--             signal sqlstate '45001' set message_text = 'tg_before_insert: amount must > 0';
--         end if;
--     end $$
-- delimiter ;

-- delimiter $$
-- create trigger tg_CheckAmount
-- 	before update on Tabaccos
-- 	for each row
-- 	begin
-- 		if new.amount < 0 then
--             signal sqlstate '45001' set message_text = 'tg_CheckAmount: amount must > 0';
--         end if;
--     end $$
-- delimiter ;

-- delimiter $$
-- create procedure sp_createCustomer(IN customerName varchar(100), IN customerAddress varchar(200), OUT customerId int)
-- begin
-- insert into Customers(customer_name, customer_address, customer_phone) values (customerName, customerAddress, customerPhone); 
--     select max(customer_id) into customerId from Customers;
-- end $$
-- delimiter ;

-- call sp_createCustomer('no name','any where', @cusId);
-- select @cusId;

-- /* INSERT DATA */
-- insert into Customers(customer_name, customer_address, customer_phone) values
-- ('Nguyen Thi X','Hai Duong', 3254234),
-- ('Nguyen Van N','Hanoi',1243214),
-- ('Nguyen Van B','Ho Chi Minh',23145432542),
-- ('Nguyen Van A','Hanoi',2341243235);
-- select * from Customers;

-- insert into Tabaccos(Tabacco_name, Tabacco_Price, amount, Tabacco_pack, Tabacco_date) values
-- ('Tabacco 1', 12.5, 8, 1, 2022-03-02),
-- ('Tabacco 2', 62.5, 6, 5, 2023-05-02),
-- ('Tabacco 3', 31.0, 10, 7, 2022-03-08),
-- ('Tabacco 4', 24.5, 5, 2, 2022-08-02),
-- ('Tabacco 5', 7.5, 9, 1, 2021-03-02);
-- select * from Tabaccos;

-- insert into Orders(customer_id, order_status) values
-- 	(1, 1), (2, 1), (1, 1);
-- select * from Orders;

-- insert into OrderDetails(order_id, Tabacco_id, unit_price, quantity) values
-- 	(1, 1, 12.5, 5), (1, 3, 31.0, 1), (1, 4, 24.5, 2),
--     (2, 2, 62.5, 1), (2, 3, 31.0, 2), (2, 5, 7.5, 4);
-- select * from OrderDetails;

-- /* CREATE & GRANT USER */
-- create user if not exists 'vtca'@'localhost' identified by 'vtcacademy';
-- grant all on OrderDB.* to 'vtca'@'localhost';
-- -- grant all on Items to 'vtca'@'localhost';
-- -- grant all on Customers to 'vtca'@'localhost';
-- -- grant all on Orders to 'vtca'@'localhost';
-- -- grant all on OrderDetails to 'vtca'@'localhost';

-- select Tabacco_id from Tabaccos order by Tabacco_id desc limit 1;

-- select customer_id, customer_name,
--     ifnull(customer_address, '') as customer_address
-- from Customers where customer_id=1;
--                         
-- select order_id from Orders order by order_id desc limit 1;

-- select LAST_INSERT_ID();
-- select customer_id from Customers order by customer_id desc limit 1;

-- update Tabaccos set amount=10 where Tabacco_id=3;
-- -- lock table Orders write;
-- -- unlock tables;
--  --  drop table orderdb;