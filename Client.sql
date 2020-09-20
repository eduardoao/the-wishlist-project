CREATE TABLE client (
	id UUID  PRIMARY KEY,
	name VARCHAR ( 150 )  NOT NULL,	
	email VARCHAR ( 255 ) UNIQUE NOT NULL,
	dateCreate DATE NOT NULL,
    dateUpdate DATE 
);

