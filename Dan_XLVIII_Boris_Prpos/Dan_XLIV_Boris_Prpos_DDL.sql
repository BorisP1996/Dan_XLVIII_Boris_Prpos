--Creating database only if database is not created yet
IF DB_ID('Zadatak_44') IS NULL
CREATE DATABASE Zadatak_44
GO
USE Zadatak_44;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblPrice')
drop table tblPrice;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblOrder')
drop table tblOrder;

create table tblPrice (
PriceID int identity (1,1) primary key,
Meal nvarchar (20),
Price int 
)

create table tblOrder(
OrderID int identity (1,1) primary key,
BigPizza int,
MediumPizza int,
SmallPizza int,
FamilyPizza int,
SpecialPizza int,
OrderDate nvarchar (30),
CustomerJMBG nvarchar (30),
OrderStatus nvarchar (30),
TotalAmount int
)

Insert into tblPrice values ('BigPizza',630),('MediumPizza',490),('SmallPizza',350),('FamilyPizza',750),('SpecialPizza',980);


