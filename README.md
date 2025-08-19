1. crear base de datos con nombre db_cazuela_chapina
2. ejecutar las migraciones
3. ingresar estos valores en la base de datos
insert into product(name, price, isAtole)values
('Tamal', 10, 0),
('Bebida', 15, 1),
('Combo', 0, 0);

insert into unit_measurement(name, ConvertionBase) values
('Libra', 1),
('Gramo', 1),
('Litro', 1),
('Onza', 1);
4. Cambiar OpenRouter  KEY en appsetting.json (la que está es temporal y dejará de funcionar)
