-- Database: products_database

-- DROP DATABASE IF EXISTS products_database;

CREATE DATABASE products_database
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'Spanish_Peru.1252'
    LC_CTYPE = 'Spanish_Peru.1252'
    LOCALE_PROVIDER = 'libc'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;


CREATE TABLE "Products"(
	id SERIAL PRIMARY KEY,
	name VARCHAR (100) NOT NULL,
	description TEXT NOT NULL,
	price NUMERIC(10, 2) NOT NULL,
	stock INT NOT NULL,
	category VARCHAR(100) NOT NULL
)


SELECT * FROM "Products";

CREATE TABLE "Users"(
	id SERIAL PRIMARY KEY,
	name VARCHAR(50) NOT NULL,
	lastName VARCHAR(50) NOT NULL,
	age INT NOT NULL,
	telephone VARCHAR(20) NOT NULL,
	email VARCHAR(100) NOT NULL UNIQUE,
	password VARCHAR(100) NOT NULL
);

SELECT * FROM "Users";

ALTER TABLE "Users" RENAME COLUMN lastName TO lastname;

