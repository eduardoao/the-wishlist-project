CREATE TABLE wishclientitem (
  wishclient_id UUID NOT NULL,
  product_id UUID NOT NULL,
  dateCreate TIMESTAMP NOT NULL,
  dateUpdate TIMESTAMP,
  
  PRIMARY KEY (wishclient_id, product_id),
  FOREIGN KEY (wishclient_id)
      REFERENCES wishclient (id),
  FOREIGN KEY (product_id)
      REFERENCES product (id)
);
