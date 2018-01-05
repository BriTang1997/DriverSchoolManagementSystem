--创建视图cname_sname 包含教练姓名和学员姓名
create view cname_sname
as
	select (select sname from student where SNO=sc.SNO) as sname, 
			(select cname from coach where cNO=sc.cNO) as cname
	from sc


--创建视图cname_lic 包含教练姓名和学员姓名
create view cname_lic
as
	select (select lic from car where lic=cc.LIC) as lic, 
			(select cname from coach where cNO=cc.cNO) as cname
	from cc

--创建视图sname_grade 包含学员姓名和成绩相关信息的视图
create view sname_grade
as
	select (select sname from student where sno=exam.SNO) as sname, 
			SNO,SJNAME,grade
	from exam
	
--创建视图ename_exp 包含消耗名称和消耗相关信息的视图
create view ename_exp
as 
	select (select ename from e_kind where ekno=expenditure.ekno) as ename,
		cash,bno,lic,edate,operator
	from expenditure
--创建视图iname_sname_in 包含学员名称和收入名称和收入相关信息的视图
create view iname_sname_in
as 
	select (select iname from i_kind where ikno=income.ikno) as iname,
			(select sname from student where sno=income.sno) as sname,
		cash,ino,sno,edate,operator
	from income