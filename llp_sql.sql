CREATE TRIGGER  d_car on car
instead of delete 
as
 declare @LIC char(10)
    --在inserted表中查询已经插入记录信息
    select @LIC= LIC from deleted
    delete from cc where LIC=@LIC
    delete FROM dbo.expenditure where LIC=@LIC
    delete from car where LIC=@LIC
go


CREATE TRIGGER  d_coach on coach
instead of delete 
as
 declare @cno char(10)
    --在inserted表中查询已经插入记录信息
    select @cno= CNO from deleted
    delete FROM  cc  where cc.CNO=@cno
    delete FRom sc where sc.CNO=@cno
    delete from coach where CNO=@cno
     go
     
     
     
 CREATE TRIGGER  d_student on student  --为学员表添加删除触发器
instead of delete 
as
 declare @sno char(8)
    --在inserted表中查询已经插入记录信息
    select @sno= SNO from deleted
    delete from exam where sno=@sno
    delete FROM  SC  where SNO=@sno
    delete FRom income where income.sno=@sno 
    delete from sc where sno=@sno
    delete from student where sno=@sno
    
    
 go   
  CREATE TRIGGER  d_subject on subject  --为科目表添加删除触发器
instead of delete 
as
 declare @sjname char(8)
    select @sjname= sjname from deleted
    delete from exam where sjname=@sjname
    delete FROM  SC  where sjname=@sjname
    delete FRom subject where sjname=@sjname
 go   
 
  CREATE TRIGGER  d_i_kind on i_kind  --为缴费表添加删除触发器
instead of delete 
as
 declare @ikno char(8)
    select @ikno= ikno from deleted
    delete FROM  income  where ikno=@ikno
    delete from i_kind where ikno=@ikno
go



  CREATE TRIGGER  d_e_kind on e_kind  --为科目表添加删除触发器
instead of delete 
as
 declare @ekno char(8)
    select @ekno= ekno from deleted
    delete FROM  expenditure where ekno=@ekno
    delete from e_kind where ekno=@ekno
