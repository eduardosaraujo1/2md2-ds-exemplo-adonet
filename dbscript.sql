create schema bdCapacitacao;
use bdCapacitacao;
create table tblAgenda
(
agdid int not null primary key,
agdnome varchar(50) null,
agdemail varchar(200) null,
agdtelefone varchar(15) null,
agdcpf varchar(15)
);
