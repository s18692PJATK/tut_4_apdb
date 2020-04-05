alter procedure PromoteProcedure(@Studies nvarchar(50), @semester int)
as
declare @count int;
	 set @count = (select count(1)
	from Enrollment e
	join Studies s
	on s.IdStudy = e.IdStudy
	where e.Semester = @semester+1
	and s.Name = @semester);

	declare @idstudy int;
	set @idstudy = (select idstudy from studies where name = @Studies);
	declare @id int;
	if(@count = 0)
		begin
		set @id = (select max(idEnrollment) +1 from Enrollment);
		insert into Enrollment(IdEnrollment,Semester,IdStudy,StartDate) values (@id,@semester+1,@idstudy,GETDATE());
		end;
	else
		begin
		set @id = (select IdEnrollment
		from Enrollment e
		join Studies s
		on s.IdStudy = e.IdStudy
		where e.Semester = @semester+1
		and s.Name = @semester);
		end;

		declare @currentId int = (select IdEnrollment
		from Enrollment e
		join Studies s
		on s.IdStudy = e.IdStudy
		where e.Semester = @semester
		and s.Name = @semester);

		
		update student
		set IdEnrollment = @id where IdEnrollment = @currentId;