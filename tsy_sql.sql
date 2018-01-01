CREATE PROCEDURE GET_INCOME_NUMBER
AS 
SELECT COUNT(*)
FROM income

CREATE PROCEDURE INSERT_INCOME(@sn char(10),@mon int,@dat datetime,@ope char(8),@ik char(8),@in char(10))
AS 
INSERT INTO income(SNO,CASH,EDATE,OPERATOR,IKNO,INO)
values (@sn,@mon,@dat,@ope,@ik,@in)

CREATE PROCEDURE INSERT_EXPENDITURE(@LI char(8),@mon int,@dat datetime,@ope char(8),@ek char(8),@in char(10))
AS 
INSERT INTO expenditure(LIC,CASH,EDATE,OPERATOR,EKNO,BNO)
values (@LI,@mon,@dat,@ope,@ek,@in)

CREATE PROCEDURE INSERT_EXAM(@SNO char(8),@dat datetime,@SJ char(8),@GRADE int)
AS 
INSERT INTO exam(SNO,SJNAME,EDATE,GRADE)
values (@SNO,@SJ,@dat,@GRADE)

SELECT *
FROM exam
---@SNO char(8),@dat datetime,@SJ char(8),@GRADE int)
EXEC INSERT_EXAM 'XY000005' ,'2009-9-30',1,22 
