--������ͼcname_sname ��������������ѧԱ����
create view cname_sname
as
	select (select sname from student where SNO=sc.SNO) as sname, 
			(select cname from coach where cNO=sc.cNO) as cname
	from sc


--������ͼcname_lic ��������������ѧԱ����
create view cname_lic
as
	select (select lic from car where lic=cc.LIC) as lic, 
			(select cname from coach where cNO=cc.cNO) as cname
	from cc

--������ͼsname_grade ����ѧԱ�����ͳɼ������Ϣ����ͼ
create view sname_grade
as
	select (select sname from student where sno=exam.SNO) as sname, 
			SNO,SJNAME,grade
	from exam
	
--������ͼename_exp �����������ƺ����������Ϣ����ͼ
create view ename_exp
as 
	select (select ename from e_kind where ekno=expenditure.ekno) as ename,
		cash,bno,lic,edate,operator
	from expenditure
--������ͼiname_sname_in ����ѧԱ���ƺ��������ƺ����������Ϣ����ͼ
create view iname_sname_in
as 
	select (select iname from i_kind where ikno=income.ikno) as iname,
			(select sname from student where sno=income.sno) as sname,
		cash,ino,sno,edate,operator
	from income