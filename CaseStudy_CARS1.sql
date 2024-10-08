--creating database CARS
create database CARS1

--creating tables
create table Incidents(
IncidentID int primary key,
IncidentType varchar(30),
IncidentDate date,
[Location] varchar(50),
[Description] varchar(100),
[Status] varchar(25),
VictimID int,foreign key (VictimID) references Victims(VictimID),
SuspectID int, foreign key(SuspectID) references Suspects(SuspectID))

create table Victims(
VictimID int primary key,
FirstName varchar(30),
LastName varchar(30),
DateOfBirth date,
Gender varchar(10),
[Address] varchar(50),
PhoneNo int
)
create table Suspects(
SuspectID int primary key,
FirstName varchar(30),
LastName varchar(30),
DateOfBirth date,
Gender varchar(10),
[Address] varchar(50),
PhoneNo int
)
create table Law_Enforcement_Agencies(
AgencyID int primary key,
AgencyName varchar(30),
Jurisdiction varchar(100),
[Address] varchar(50),
PhoneNo int
)
create table Officers(
OfficerID int primary key,
FirstName varchar(30),
LastName varchar(30),
BadgeNumber int,
[Rank] int,
[Address] varchar(50),
PhoneNo int,
AgencyID int, foreign key(AgencyID) references  Law_Enforcement_Agencies(AgencyID)
)
create table Evidence(
EvidenceID int primary key,
[Description] varchar(100),
Location_Found varchar(50),
IncidentID int,foreign key(IncidentID) references Incidents(IncidentID))

create table Reports(
ReportID int primary key,
IncidentID int,foreign key(IncidentID) references Incidents(IncidentID),
ReportingOfficer int,foreign Key(ReportingOfficer) references Officers(OfficerID),
ReportDate date,
ReportDetails varchar(200),
[Status] varchar(15)
)
alter table reports alter column [status] varchar(25)
create table [Case](
CaseID int primary key,
[Description] varchar(100),
DateCreate datetime,
IncidentID int
)
--inserting data into tables
insert into Incidents values(1,'Robbery','2024-09-01','Rewa','Inicdent happened at midnight when the victim was sleeping','Open',2,3),
(2,'Robbery','2024-08-01','Bhopal','Inicdent happened at TT market during afternoon','Under Investigation',1,4),
(3,'Homicide','2023-09-02','Burhanpur','Inicdent happened during a ceremony','Open',3,1),
(4,'Robbery','2024-05-06','Jhabua','Inicdent happened at midnight when the victim was not at home','Closed',5,2),
(5,'Cybercrime','2023-07-01','Rewa','Inicdent happened during visitng a website link','Under Investigation',6,3),
(6,'Kidnapping','2024-09-05','Manpur','Inicdent happened at a graden during afternoon','Open',4,5),
(7,'Cybercrime','2024-02-06','Khandwa','Inicdent happened through email link','Closed',7,6),
(8,'Robbery','2024-04-11','Sendhwa','Inicdent happened at midnight when the victim was sleeping','Under Investigation',9,7),
(9,'Dowry','2023-09-04','Dhamnod','Inicdent happened after a fight over family issues','Open',8,8),
(10,'Kidnapping','2023-09-01','Panna','Inicdent happened during a protest','Open',10,9),
(11,'Robbery','2024-09-01','Dhar','Inicdent happened in afternoon','Closed',11,10)

select * from Incidents
insert into Victims values
(1, 'Amit', 'Sharma', '1990-01-15', 'Male', '123 MG Road,Bhopal', 987654320),
(2, 'Priya', 'Verma', '1985-06-22', 'Female', '456 Nehru Nagar, Rewa', 912345679),
(3, 'Rajesh', 'Gupta', '1992-11-03', 'Male', '789 Park Street,Burhanpur', 923456890),
(4, 'Sneha', 'Patel', '1991-04-17', 'Female', '321 JP Nagar, Manpur', 934678901),
(5, 'Vikram', 'Kumar', '1989-09-09', 'Male', '654 Banjara Hills, Jhabua', 945679012),
(6, 'Aishwarya', 'Rao', '1993-03-30', 'Female', '876 Anna Salai, Rewa', 956789123),
(7, 'Manish', 'Singh', '1988-12-12', 'Male', '135 MG Road, Khandwa', 967890134),
(8, 'Pooja', 'Mehta', '1994-05-25', 'Female', '246 Gokhale Marg, Dhamnod', 978912345),
(9, 'Arun', 'Agarwal', '1990-08-18', 'Male', '357 Sector 17, Sendhwa', 989012356),
(10, 'Neha', 'Deshmukh', '1991-10-30', 'Female', '468 Law Garden,Panna', 990134567),
(11, 'Rohit', 'Jain', '1987-07-14', 'Male', '579 FC Road,Dhar', 901235678);
select * from Victims

insert into Suspects values
(1, 'Rahul', 'Yadav', '1985-07-21', 'Male', '123 Civil Lines, Kanpur', 987543210),
(2, 'Sunita', 'Nair', '1990-03-18', 'Female', '456 Lal Bagh, Bhopal', 913456789),
(3, 'Ajay', 'Reddy', '1992-11-15', 'Male', '789 Jubilee Hills, Dhar', 934567890),
(4, 'Geeta', 'Shah', '1987-09-25', 'Female', '321 Marine Drive, Mumbai', 934678901),
(5, 'Karan', 'Kapoor', '1989-01-12', 'Male', '654 MG Road, Mhow', 945679012),
(6, 'Rina', 'Chopra', '1991-04-04', 'Female', '876 Sector 5, Noida', 956890123),
(7, 'Suresh', 'Rana', '1994-08-20', 'Male', '135 Shyam Nagar, Jabalpur', 967801234),
(8, 'Anjali', 'Pillai', '1993-12-10', 'Female', '246 Nungambakkam, Dhamnod', 989012345),
(9, 'Pankaj', 'Ghosh', '1990-06-28', 'Male', '357 Salt Lake, Kolkata', 989012346),
(10, 'Sanchit', 'Joshi', '1988-05-30', 'Male', '468 Rani Bagh, Indore', 990123567),
(11, 'Vikas', 'Patil', '1992-10-19', 'Male', '579 Laxmi Nagar, Nagpur', 901234578);
select * from Suspects

insert into Law_Enforcement_Agencies values
(1, 'Bhopal Police', 'Bhopal, Madhya Pradesh', 'TT Nagar, Bhopal', 112123467),
(2, 'Indore Police', 'Indore, Madhya Pradesh', 'MG Road, Indore', 112235678),
(3, 'Manpur Police', 'Manpur, Madhya Pradesh', 'Lashkar, Manpur', 112345689),
(4, 'Jabalpur Police', 'Jabalpur, Madhya Pradesh', 'Napier Town, Jabalpur', 112456890),
(5, 'Ujjain Police', 'Ujjain, Madhya Pradesh', 'Kharakua, Ujjain', 112578901),
(6, 'Sagar Police', 'Sagar, Madhya Pradesh', 'Civil Lines, Sagar', 112689012),
(7, 'Rewa Police', 'Rewa, Madhya Pradesh', 'Sirmaur Chowk, Rewa', 112789123),
(8, 'Khandwa Police', 'Khandwa, Madhya Pradesh', 'Sirmaur Chowk, Khandwa', 11289423),
(9, 'Khargone Police', 'Khargone, Madhya Pradesh', 'Sanawad Road, Khargone', 118901234),
(10, 'Dhar Police', 'Dhar, Madhya Pradesh', 'Old Fort Road, Dhar', 112912345),
(11, 'Sendhwa Police', 'Sendhwa, Madhya Pradesh', 'AB Road, Sendhwa', 120123456)

select * from Law_Enforcement_Agencies

insert into Officers values
(1, 'Rajesh', 'Sharma', 10123, 5, 'TT Nagar, Bhopal', 998876655, 1),
(2, 'Amit', 'Verma', 10234, 4, 'MG Road, Indore', 998865544, 2),
(3, 'Sunita', 'Dubey', 10345, 6, 'Lashkar, Manpur', 997754433, 3),
(4, 'Vikas', 'Patel', 10456, 3, 'Napier Town, Jabalpur', 997765522, 4),
(5, 'Pooja', 'Singh', 10567, 4, 'Kharakua, Ujjain', 996654411, 5),
(6, 'Anil', 'Yadav', 10678, 2, 'Civil Lines, Sagar', 995544322, 6),
(7, 'Ravi', 'Chouhan', 10789, 5, 'Sirmaur Chowk, Rewa', 994332211, 7),
(8, 'Neha', 'Jain', 10890, 6, 'Sanawad Road, Khargone', 993321100, 9),
(9, 'Deepak', 'Rathore', 10901, 3, 'Old Fort Road, Dhar', 992210099, 10),
(10, 'Arjun', 'Thakur', 11012, 2, 'AB Road, Sendhwa', 991100988, 11),
(11, 'Suresh', 'Mishra', 11023, 4, 'Bairagarh, Bhopal', 991233455, 1),
(12, 'Meera', 'Pandey', 11134, 5, 'Vijay Nagar, Indore', 990445566, 2),
(13, 'Ramesh', 'Saxena', 11245, 6, 'Thatipur, Khandwa', 989256677, 8),
(14, 'Kavita', 'Tripathi', 11356, 3, 'Garha, Jabalpur', 988366788, 4),
(15, 'Mohit', 'Solanki', 11467, 2, 'Madhav Nagar, Ujjain', 987478899, 5)

select * from Officers
select * from Incidents

insert into Evidence values
(1, 'Fingerprint found on door handle', 'Crime scene - Kitchen', 1),
(2, 'CCTV footage showing suspect', 'Nearby shop', 2),
(3, 'Blood sample from broken glass', 'Living room', 3),
(4, 'Knife with fingerprints', 'Crime scene - Bedroom', 9),
(5, 'Wallet belonging to the suspect', 'Abandoned car', 10),
(6, 'Footprints near window', 'Backyard', 4),
(7, 'same link was sent to 5 others in the area', 'Message', 5),
(8, 'Cell phone of victim', 'Crime scene - Garden', 6),
(9, 'Cigarette butt with DNA traces', 'Outside the house', 8),
(10, 'Torn fabric piece from suspect s clothing', 'Crime scene - Hallway', 11),
(11, 'link contained malicious software', 'Phising', 7)

select * from Evidence

insert into Reports values
(1, 1, 7, '2024-09-02', 'Reported robbery incident at midnight; victim interviewed.', 'Open'),
(2, 2, 1, '2024-08-02', 'Investigated robbery at TT market; CCTV footage analyzed.', 'Under Investigation'),
(3, 3, 6, '2023-09-03', 'Homicide report filed; family members questioned.', 'Open'),
(4, 4, 4, '2024-05-07', 'Robbery case closed; suspect arrested.', 'Closed'),
(5, 5, 7, '2023-07-02', 'Cybercrime incident logged; digital evidence collected.', 'Under Investigation'),
(6, 6, 3, '2024-09-06', 'Kidnapping report filed; area searched.', 'Open'),
(7, 7, 13, '2024-02-07', 'Cybercrime report closed; suspect apprehended.', 'Closed'),
(8, 8, 10, '2024-04-12', 'Robbery report filed; witnesses interviewed.', 'Under Investigation'),
(9, 9, 9, '2023-09-05', 'Dowry case reported; ongoing investigation.', 'Open'),
(10, 10, 4, '2023-09-02', 'Kidnapping case filed; protest attendees questioned.', 'Open'),
(11, 11, 9, '2024-09-02', 'Afternoon robbery report filed; evidence collected.', 'Closed')

select * from Reports
