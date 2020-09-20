CREATE TABLE product (
	id UUID  PRIMARY KEY,
	title VARCHAR ( 150 )  NOT NULL,	
	picture VARCHAR ( 255 )  NOT NULL,
	price DECIMAL  NOT NULL,
	dateCreate TIMESTAMP NOT NULL,
    dateUpdate TIMESTAMP 
);
