CREATE PROCEDURE INSERT_INCOME(@sn char(10),@mon int,@dat datetime,@ope char(8),@ik char(8),@in char(10))
AS 
INSERT INTO income(SNO,CASH,EDATE,OPERATOR,IKNO,INO)
values (@sn,@mon,@dat,@ope,@ik,@in)

CREATE FUNCTION GET_INCOME_NUMBER ()
RETURNS integer
AS
BEGIN
	declare @in int
	SELECT @in = COUNT(*)
	FROM income
	return @in
END
----获取下一个id
CREATE FUNCTION GET_INCOME_NEXTID()
RETURNS CHAR(10)
as
BEGIN
	DECLARE @no char(10)
	SELECT  TOP 1 @no = INO
	FROM income
	ORDER BY INO DESC
	return @no
END
---收入
---SELECT dbo.GET_INCOME_NUMBER()
CREATE FUNCTION GET_EXPEN_NUMBER ()
RETURNS integer
AS
BEGIN
	declare @in int
	SELECT @in = COUNT(*)
	FROM expenditure
	return @in
END

CREATE FUNCTION GET_EXPENDITURE_NEXTID()
RETURNS CHAR(10)
as
BEGIN
	DECLARE @no char(10)
	SELECT  TOP 1 @no = BNO
	FROM expenditure
	ORDER BY BNO DESC
	return @no
END

CREATE PROCEDURE INSERT_EXPENDITURE(@LI char(8),@mon int,@dat datetime,@ope char(8),@ek char(8),@in char(10))
AS 
INSERT INTO expenditure(LIC,CASH,EDATE,OPERATOR,EKNO,BNO)
values (@LI,@mon,@dat,@ope,@ek,@in)

---支出

/*
考试：新建，更新，按照时间段查询，按照
*/

USE [DriverSchool]
GO
/****** Object:  StoredProcedure [dbo].[INSERT_EXAM]    Script Date: 01/03/2018 09:05:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[INSERT_EXAM](@SNO char(10),@dat datetime,@SJ smallint,@GRADE int)
AS 
BEGIN
	DECLARE @yes int
	SELECT @yes = COUNT(*)
	FROM exam
	WHERE SNO = @SNO and SJNAME = @SJ
	
	if(@yes = 1)
	BEGIN
		UPDATE exam
		SET GRADE = @GRADE , EDATE = @dat
		WHERE SNO = @SNO and SJNAME = @SJ
	END
	else
	BEGIN
		INSERT INTO exam(SNO,SJNAME,EDATE,GRADE)
		values (@SNO,@SJ,@dat,@GRADE)
	END
END


SELECT *
FROM exam
---@SNO char(8),@dat datetime,@SJ char(8),@GRADE int)
EXEC INSERT_EXAM 'XY000005' ,'2009-9-30',1,22 


----获取教练学生

---女教练
CREATE VIEW c_woman
as
select *
from coach
WHERE SEX = '女'
----男教练
CREATE VIEW c_woman
as
select *
from coach
WHERE SEX = '女'

----获取某教练有多少学生（按照姓名）
CREATE FUNCTION GET_COACH_TEACH_NUMBER (@cname char(8))
RETURNS integer
AS
BEGIN
	DECLARE @in int
	SELECT  @in = COUNT(*)
	FROM sc
	GROUP BY CNO
	HAVING CNO in (
		SELECT CNO
		FROM coach WHERE CNAME = @cname
	)
	return @in
END
-----获取某教练当前有有多少学生 
CREATE FUNCTION [dbo].[GET_COACH_TEACH_NUMBER_NOW] (@cno char(10))
RETURNS integer
AS
BEGIN
	DECLARE @in int
	SELECT  @in = COUNT(*)
	FROM now_sc
	GROUP BY CNO
	HAVING CNO = @cno
	return @in
END
-----获取某教练当前学生
CREATE FUNCTION GET_C_S (@cno char(10))
RETURNS TABLE
AS
	return (
	SELECT  *
	FROM sc_now
	WHERE CNO = @cno
	)

-----获取某教练所有
CREATE FUNCTION GET_C_S (@cno char(10))
RETURNS TABLE
AS
	return (
	SELECT  *
	FROM sc
	WHERE CNO = @cno
	)


---在学人员
CREATE VIEW s_atschool
AS SELECT * FROM student
WHERE PROGRESS < 5
---男学员
CREATE VIEW s_man
as
select *
from student
WHERE SEX = '男'
---女学员
CREATE VIEW s_woman
as
select *
from student
WHERE SEX = '女'
---没交费的
CREATE VIEW s_unpayed
as
select *
from student
WHERE pay = 0
----付钱了的
CREATE VIEW s_payed
as
select *
from student
WHERE pay = 1
---不在校人员
CREATE VIEW s_not_atschool
as
select *
from student
EXCEPT
select *
from s_atschool

---有效sc
CREATE VIEW now_sc
as
select *
from sc
where SNO in (
	SELECT SNO
	FROM s_atschool
	WHERE PROGRESS = sc.SJNAME
)

------教练学员
CREATE PROCEDURE INSERT_C_S(@SNO char(10),@SJ char(8),@CNO char(10))
AS
BEGIN
	DECLARE @nu int
	SELECT @nu = COUNT(*)
	FROM now_sc
	GROUP BY CNO
	HAVING CNO = @CNO
	if(@nu<5)
		BEGIN
			INSERT INTO sc(SNO,SJNAME,CNO)
			values (@SNO,@SJ,@CNO)
		END
	else
		PRINT 'teaching limit'
END



-----教练车辆

CREATE PROCEDURE INSERT_C_C(@LIC char(8),@CNO char(10))
AS
BEGIN
	DECLARE @cn int
	SELECT @cn = COUNT(*)
	FROM cc WHERE CNO = @CNO and LIC = @LIC
	if(@cn = 0) BEGIN
		INSERT INTO cc(CNO,LIC)
		VALUES (@CNO,@LIC) END
	else
		PRINT 'repeat.'
END





/*
统计：
学生统计 ： 考试记录，缴费记录(按类型，时间，管理员来算)
车辆统计 ： 消费记录(按类型，时间，管理员来算)
教练统计 :	曾经学员，现在学员，及格率
入账统计 : 	时间，金额，操作员
出账统计 ：	同上
考试统计 ： 时间，人
*/
----学生和老师是否匹配
create function IS_S(@sno char(10),@cno char(10))
returns int
as
begin
	declare @a int
	select @a = COUNT(*)
	from sc
	where CNO = @cno and SNO = @sno
	
	return @a
end


----
CREATE FUNCTION GET_EXAM_S (@SNO char(10))
RETURNS TABLE
AS
	RETURN(
		SELECT *
		FROM exam
		WHERE SNO = @SNO
	)
CREATE FUNCTION GET_EXAM_TIME (@st int,@en int)
RETURNS TABLE
AS
	RETURN(
		SELECT *
		FROM income
		WHERE EDATE >= @st and EDATE <= @en
	)

----单人合格率
CREATE FUNCTION GET_PASS_RATE(@SNO char(10))
RETURNS FLOAT
AS
BEGIN
	DECLARE @ans FLOAT
	SELECT @ans = 4.0/COUNT(*)
	FROM income
	GROUP BY SNO,IKNO
	HAVING SNO = @SNO and IKNO = 'ikind001'
	return @ans
END
-------人的账单
CREATE FUNCTION GET_S_MONEY (@st char(10))
RETURNS TABLE
AS
	RETURN(
		SELECT INO,SNO,(SELECT INAME FROM i_kind WHERE i_kind.IKNO = income.IKNO) AS KIND,CASH,EDATE
		FROM income
		WHERE SNO = @st
	)
----车的账单
CREATE FUNCTION GET_C_MONEY (@st char(8))
RETURNS TABLE
AS
	RETURN(
		SELECT BNO,LIC,(SELECT ENAME FROM e_kind WHERE e_kind.EKNO = expenditure.EKNO) AS KIND,CASH,EDATE
		FROM expenditure
		WHERE LIC =@st
	)
	

----教练的学员的平均及格率 
CREATE FUNCTION GET_C_PASS_RATE(@CNO char(10))
RETURNS FLOAT
AS
BEGIN
	DECLARE @ans FLOAT
	SELECT @ans = SUM(pin)/COUNT(*)
	FROM (
		SELECT SNO,dbo.PASS_RATE(SNO) as pin
		FROM sc
		GROUP BY SNO
		HAVING SNO in(
			SELECT SNO
			FROM sc WHERE CNO =@CNO
		)
	) as t1
	GROUP BY SNO,pin
	HAVING pin is not null
	return @ans
END

CREATE FUNCTION GET_INCOME_BY_TIME (@st datetime,@en datetime)
RETURNS TABLE
AS
	RETURN(
		SELECT *
		FROM income
		WHERE EDATE >= @st and EDATE <= @en
	)
CREATE FUNCTION GET_INCOME_BY_MONEY (@st int,@en int)
RETURNS TABLE
AS
	RETURN(
		SELECT *
		FROM income
		WHERE CASH >= @st and CASH <= @en
	)
CREATE FUNCTION GET_INCOME_BY_OPER (@st char(8))
RETURNS TABLE
AS
	RETURN(
		SELECT *
		FROM income
		WHERE OPERATOR = @st
	)
CREATE FUNCTION GET_EXPEN_BY_OPER (@st char(8))
RETURNS TABLE
AS
	RETURN(
		SELECT *
		FROM income
		WHERE OPERATOR = @st
	)
CREATE FUNCTION GET_EXPEN_BY_MONEY (@st int,@en int)
RETURNS TABLE
AS
	RETURN(
		SELECT *
		FROM income
		WHERE CASH >= @st and CASH <= @en
	)
CREATE FUNCTION GET_EXPEN_BY_TIME (@st int,@en int)
RETURNS TABLE
AS
	RETURN(
		SELECT *
		FROM income
		WHERE EDATE >= @st and EDATE <= @en
	)

----导出到一张表里面	
EXEC master..xp_cmdshell 'bcp DriverSchool.dbo.income out D:\Temp.xls -c -q -S"THEOLDOFTANG" -U"tsy" -P"111"'