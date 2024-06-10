Begin transaction;

drop table if exists task;

create table task
(taskId      INT IDENTITY NOT NULL  primary key,
 dueDate     date     not null,
 description text     not null,
 iscomplete  bit  default 0
)



Insert into task(dueDate, description, iscomplete) Values('06/12/2024','Complete Task Manager App', 0)
--
Insert into task (dueDate, description, iscomplete) Values('2024-12-24','Finish Holiday Shopping', 0)	

Insert into task (dueDate, description, iscomplete) Values('2023-10-28','Attend Jax and Rustom Wedding', 1)

Insert into task (dueDate, description, iscomplete) Values('2024-03-19','Plan Birthday Party', 0)	

Insert into task (dueDate, description, iscomplete) Values('2024-01-15','Plan trip to Gary and Kristen Wedding', 1)

Insert into task (dueDate, description, iscomplete) Values('2024-10-02','25th Anniversary Dinner', 0)

Insert into task (dueDate, description, iscomplete) Values('2023-11-11','Plan Thanksgiving Dinner', 0)

Insert into task (dueDate, description, iscomplete) Values('2024-09-15','Plan Kathy Birthday Party', 0)

commit

