#Script lavadero

```sql
drop database if exists lavadero;
create database lavadero;
use lavadero;

create table usuarios(
	id int auto_increment not null primary key,
	nombre varchar(100) not null,
	correo varchar(100) not null,
	pssword varchar(100) not null
);

create table empleados (
	id int not null primary key,
	nombre varchar(100) not null,
	apellido varchar(100) not null,
	telefono varchar(50) not null,
	disponible bool not null
);

create table servicios (
	nombre_cliente varchar(100) not null,
	documento_cliente varchar(100) not null,
	telefono_cliente varchar(100) not null,
	marca_vehiculo varchar(50) not null,
	modelo_vehiculo varchar(50) not null,
	matricula_vehiculo varchar(100) not null primary key,
	tipo_vehiculo varchar(50) not null,
	tipo_servicio varchar(20) not null,
	urgente bool not null,
	prioridad int not null,
	precio_adicional decimal(50, 6)
);

create table citas (
	id varchar(100) not null primary key,
	nombre_cliente varchar(100) not null,
	documento_cliente int not null,
	telefono_cliente varchar(30) not null,
	marca_vehiculo varchar(50) not null,
	modelo_vehiculo varchar(50) not null,
	matricula_vehiculo varchar(10) not null,
	tipo_vehiculo varchar(20) not null,
	tipo_servicio varchar(10) not null,
	fecha_cita datetime not null
);
```