insert dbo.subject(PLACE,SJNAME,SINTRO)
values ('车管所安排考场',1,'科目一,考试形式为上机考试,100道题,90分及以上过关。'),
('训练场',2,'科目二,考试项目包括倒车入库,坡道定点停车起步,直角转弯,曲线行驶。'),
('马路',3,'科目三,主要考察上车准备,灯光模拟考试,起步,行驶,加减挡位操作等技能。'),
('车管所安排考场',4,'科目四,安全文明驾驶,复杂条件下驾驶技能,紧急情况处置等。')

---修理，加油，保养，购买，其他
insert dbo.e_kind(EKNO,ENAME,EINTRO)
values ('ekind001','修理','汽车修理所产生的消费'),
('ekind002','加油','汽车加油所产生的消费'),
('ekind003','保养','洗车，换零件之类的保养活动所产生的费用'),
('ekind004','购买','购买车辆所花的费用'),
('ekind005','其他','待填写')

---学费，考试费，重修缴费
insert dbo.i_kind(IKNO,INAME,IINTRO)
values ('ikind001','考试','学生考试缴纳的费用'),
('ikind002','入学费','学生入学所缴纳的费用'),
('ikind003','重修费','学生重修所缴纳的费用'),
('ikind004','其他','待填写')

---教练
insert dbo.coach(CNO,CNAME,PHONE,SEX)
values ('JL000001','秦教练','15112124343','男'),
('JL000002','刘教练','15012124343','女'),
('JL000003','李教练','15212124343','男')

---学员
insert dbo.student(SNO,SNAME,SEX,PHONE,PAY,PROGRESS)
values ('XY000001','张三','男','13311122222',1,1),
('XY000002','李四','男','13322223333',1,5),
('XY000003','王五','女','13344341244',1,3),
('XY000004','赵六','女','13111426222',1,2),
('XY000005','侯七','男','13317777722',0,1),
('XY000006','朱八','男','13113377222',1,1)
---车辆

insert dbo.car(LIC,COLOR,BRAND,BUYTIME,MILE)
values ('苏A01001','黄','一汽大众','2001-1-1',10000),
('苏A01002','白','宝马','2005-1-1',10000),
('苏A01003','白','奥迪','2004-1-1',10000),
('苏A01004','黑','五菱宏光','2008-1-1',10000)

---考试

insert dbo.exam(SJNAME,SNO,GRADE,EDATE)
values (1,'XY000002',90,'2009-10-1'),
(2,'XY000002',99,'2009-11-2'),
(4,'XY000002',99,'2010-1-7'),
(3,'XY000002',99,'2010-1-5'),
(1,'XY000003',99,'2009-10-1'),
(2,'XY000003',90,'2009-11-1'),
(1,'XY000004',90,'2009-12-1')

---学生教练

insert dbo.sc(SNO,CNO,SJNAME)
values ('XY000001','JL000001',1),
('XY000002','JL000001',1),
('XY000002','JL000001',2),
('XY000002','JL000003',3),
('XY000002','JL000003',4),
('XY000003','JL000002',1),
('XY000003','JL000002',2),
('XY000003','JL000003',3),
('XY000004','JL000002',1),
('XY000004','JL000002',2),
('XY000006','JL000001',1)

---教练车辆
insert dbo.cc(CNO,LIC)
values ('JL000001','苏A01001'),
 ('JL000002','苏A01001'),
 ('JL000003','苏A01001'),
 ('JL000001','苏A01004'), 
 ('JL000002','苏A01003'),
 ('JL000003','苏A01002')

---管理员
insert dbo.manager(MNAME,PASSWORD,POWER)
values ('tsy','000',4),
('test3','000',3),
('test2','000',2),
('test1','000',1)
---收入

insert dbo.income(SNO,CASH,EDATE,IKNO,INO,OPERATOR)
values ('XY000001',3000,'2009-10-1','ikind002','IN00000001','tsy'),
('XY000002',3000,'2009-11-1','ikind002','IN00000002','tsy'),
('XY000003',3000,'2009-9-1','ikind002','IN00000003','tsy'),
('XY000004',3000,'2009-9-2','ikind002','IN00000004','tsy'),
('XY000006',3000,'2009-9-3','ikind002','IN00000005','tsy'),

('XY000002',1000,'2009-9-30','ikind001','IN00000006','tsy'),
('XY000002',1000,'2009-10-29','ikind001','IN00000007','tsy'),
('XY000002',1000,'2010-1-1','ikind001','IN00000008','tsy'),
('XY000002',1000,'2010-1-4','ikind001','IN00000009','tsy'),
('XY000002',1000,'2010-1-6','ikind001','IN00000010','tsy'),
('XY000003',1000,'2009-9-30','ikind001','IN00000011','tsy'),
('XY000003',1000,'2009-10-31','ikind001','IN00000012','tsy'),
('XY000001',1000,'2009-9-30','ikind001','IN00000013','tsy'),
('XY000001',1000,'2009-10-30','ikind001','IN00000014','tsy'),
('XY000001',1000,'2009-11-30','ikind001','IN00000015','tsy')



---支出

insert dbo.expenditure(EKNO,BNO,CASH,EDATE,LIC,OPERATOR)
values ('ekind003','EX00000001',200,'2009-10-1','苏A01002','tsy'),
('ekind003','EX00000002',200,'2009-10-1','苏A01001','tsy'),
('ekind001','EX00000003',200,'2009-9-1','苏A01003','tsy'),
('ekind002','EX00000004',200,'2009-12-1','苏A01004','tsy'),
('ekind002','EX00000005',200,'2009-10-1','苏A01002','tsy'),
('ekind004','EX00000006',20000,'2009-7-1','苏A01001','tsy'),
('ekind004','EX00000007',20000,'2009-7-1','苏A01002','tsy'),
('ekind004','EX00000008',20000,'2009-7-1','苏A01003','tsy'),
('ekind004','EX00000009',20000,'2009-8-1','苏A01004','tsy')


