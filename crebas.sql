/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     2017-12-29 19:50:14                          */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('cc') and o.name = 'FK_CC_CC_COACH')
alter table cc
   drop constraint FK_CC_CC_COACH
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('cc') and o.name = 'FK_CC_CC2_CAR')
alter table cc
   drop constraint FK_CC_CC2_CAR
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('exam') and o.name = 'FK_EXAM_EXAM_STUDENT')
alter table exam
   drop constraint FK_EXAM_EXAM_STUDENT
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('exam') and o.name = 'FK_EXAM_EXAM2_SUBJECT')
alter table exam
   drop constraint FK_EXAM_EXAM2_SUBJECT
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('expenditure') and o.name = 'FK_EXPENDIT_CEX_CAR')
alter table expenditure
   drop constraint FK_EXPENDIT_CEX_CAR
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('expenditure') and o.name = 'FK_EXPENDIT_EXKIND_E_KIND')
alter table expenditure
   drop constraint FK_EXPENDIT_EXKIND_E_KIND
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('income') and o.name = 'FK_INCOME_INKIND_I_KIND')
alter table income
   drop constraint FK_INCOME_INKIND_I_KIND
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('income') and o.name = 'FK_INCOME_SIN_STUDENT')
alter table income
   drop constraint FK_INCOME_SIN_STUDENT
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('sc') and o.name = 'FK_SC_SC_COACH')
alter table sc
   drop constraint FK_SC_SC_COACH
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('sc') and o.name = 'FK_SC_SC2_STUDENT')
alter table sc
   drop constraint FK_SC_SC2_STUDENT
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('sc') and o.name = 'FK_SC_SC3_SUBJECT')
alter table sc
   drop constraint FK_SC_SC3_SUBJECT
go

if exists (select 1
            from  sysobjects
           where  id = object_id('car')
            and   type = 'U')
   drop table car
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('cc')
            and   name  = 'cc2_FK'
            and   indid > 0
            and   indid < 255)
   drop index cc.cc2_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('cc')
            and   name  = 'cc_FK'
            and   indid > 0
            and   indid < 255)
   drop index cc.cc_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('cc')
            and   type = 'U')
   drop table cc
go

if exists (select 1
            from  sysobjects
           where  id = object_id('coach')
            and   type = 'U')
   drop table coach
go

if exists (select 1
            from  sysobjects
           where  id = object_id('e_kind')
            and   type = 'U')
   drop table e_kind
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('exam')
            and   name  = 'exam2_FK'
            and   indid > 0
            and   indid < 255)
   drop index exam.exam2_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('exam')
            and   name  = 'exam_FK'
            and   indid > 0
            and   indid < 255)
   drop index exam.exam_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('exam')
            and   type = 'U')
   drop table exam
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('expenditure')
            and   name  = 'exkind_FK'
            and   indid > 0
            and   indid < 255)
   drop index expenditure.exkind_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('expenditure')
            and   name  = 'cex_FK'
            and   indid > 0
            and   indid < 255)
   drop index expenditure.cex_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('expenditure')
            and   type = 'U')
   drop table expenditure
go

if exists (select 1
            from  sysobjects
           where  id = object_id('i_kind')
            and   type = 'U')
   drop table i_kind
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('income')
            and   name  = 'inkind_FK'
            and   indid > 0
            and   indid < 255)
   drop index income.inkind_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('income')
            and   name  = 'sin_FK'
            and   indid > 0
            and   indid < 255)
   drop index income.sin_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('income')
            and   type = 'U')
   drop table income
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('sc')
            and   name  = 'sc3_FK'
            and   indid > 0
            and   indid < 255)
   drop index sc.sc3_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('sc')
            and   name  = 'sc2_FK'
            and   indid > 0
            and   indid < 255)
   drop index sc.sc2_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('sc')
            and   name  = 'sc_FK'
            and   indid > 0
            and   indid < 255)
   drop index sc.sc_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('sc')
            and   type = 'U')
   drop table sc
go

if exists (select 1
            from  sysobjects
           where  id = object_id('student')
            and   type = 'U')
   drop table student
go

if exists (select 1
            from  sysobjects
           where  id = object_id('subject')
            and   type = 'U')
   drop table subject
go

/*==============================================================*/
/* Table: car                                                   */
/*==============================================================*/
create table car (
   LIC                  char(8)              not null,
   COLOR                char(8)              not null,
   MILE                 double precision     not null,
   BRAND                char(24)             null,
   BUYTIME              datetime             null,
   constraint PK_CAR primary key nonclustered (LIC)
)
go

/*==============================================================*/
/* Table: cc                                                    */
/*==============================================================*/
create table cc (
   CNO                  char(10)             not null,
   LIC                  char(8)              not null,
   constraint PK_CC primary key (CNO, LIC)
)
go

/*==============================================================*/
/* Index: cc_FK                                                 */
/*==============================================================*/
create index cc_FK on cc (
CNO ASC
)
go

/*==============================================================*/
/* Index: cc2_FK                                                */
/*==============================================================*/
create index cc2_FK on cc (
LIC ASC
)
go

/*==============================================================*/
/* Table: coach                                                 */
/*==============================================================*/
create table coach (
   CNO                  char(10)             not null,
   CNAME                char(8)              not null,
   SEX                  char(2)              not null,
   PHONE                char(11)             not null,
   constraint PK_COACH primary key nonclustered (CNO)
)
go

/*==============================================================*/
/* Table: e_kind                                                */
/*==============================================================*/
create table e_kind (
   ENAME                char(24)             not null,
   EKNO                 char(8)              not null,
   EINTRO               varchar(64)          null,
   constraint PK_E_KIND primary key nonclustered (EKNO)
)
go

/*==============================================================*/
/* Table: exam                                                  */
/*==============================================================*/
create table exam (
   SNO                  char(10)             not null,
   SJNAME               smallint             not null,
   GRADE                int                  not null,
   DATE                 datetime             not null,
   constraint PK_EXAM primary key (SNO, SJNAME)
)
go

/*==============================================================*/
/* Index: exam_FK                                               */
/*==============================================================*/
create index exam_FK on exam (
SNO ASC
)
go

/*==============================================================*/
/* Index: exam2_FK                                              */
/*==============================================================*/
create index exam2_FK on exam (
SJNAME ASC
)
go

/*==============================================================*/
/* Table: expenditure                                           */
/*==============================================================*/
create table expenditure (
   CASH                 int                  not null,
   BNO                  char(10)             not null,
   EKNO                 char(8)              null,
   LIC                  char(8)              null,
   DATE                 datetime             not null,
   OPERATOR             char(8)              not null,
   KIND                 char(24)             not null,
   constraint PK_EXPENDITURE primary key nonclustered (BNO)
)
go

/*==============================================================*/
/* Index: cex_FK                                                */
/*==============================================================*/
create index cex_FK on expenditure (
LIC ASC
)
go

/*==============================================================*/
/* Index: exkind_FK                                             */
/*==============================================================*/
create index exkind_FK on expenditure (
EKNO ASC
)
go

/*==============================================================*/
/* Table: i_kind                                                */
/*==============================================================*/
create table i_kind (
   INAME                char(24)             not null,
   IINTRO               varchar(64)          null,
   IKNO                 char(8)              not null,
   constraint PK_I_KIND primary key nonclustered (IKNO)
)
go

/*==============================================================*/
/* Table: income                                                */
/*==============================================================*/
create table income (
   INO                  char(10)             not null,
   SNO                  char(10)             null,
   IKNO                 char(8)              null,
   CASH                 int                  not null,
   DATE                 datetime             not null,
   OPERATOR             char(8)              not null,
   KIND                 char(24)             not null,
   constraint PK_INCOME primary key nonclustered (INO)
)
go

/*==============================================================*/
/* Index: sin_FK                                                */
/*==============================================================*/
create index sin_FK on income (
SNO ASC
)
go

/*==============================================================*/
/* Index: inkind_FK                                             */
/*==============================================================*/
create index inkind_FK on income (
IKNO ASC
)
go

/*==============================================================*/
/* Table: sc                                                    */
/*==============================================================*/
create table sc (
   CNO                  char(10)             not null,
   SNO                  char(10)             not null,
   SJNAME               smallint             not null,
   constraint PK_SC primary key (CNO, SNO, SJNAME)
)
go

/*==============================================================*/
/* Index: sc_FK                                                 */
/*==============================================================*/
create index sc_FK on sc (
CNO ASC
)
go

/*==============================================================*/
/* Index: sc2_FK                                                */
/*==============================================================*/
create index sc2_FK on sc (
SNO ASC
)
go

/*==============================================================*/
/* Index: sc3_FK                                                */
/*==============================================================*/
create index sc3_FK on sc (
SJNAME ASC
)
go

/*==============================================================*/
/* Table: student                                               */
/*==============================================================*/
create table student (
   SNO                  char(10)             not null,
   SNAME                char(10)             not null,
   PHONE                char(11)             not null,
   PAY                  tinyint              not null,
   SEX                  char(2)              not null,
   PROGRESS             smallint             not null,
   constraint PK_STUDENT primary key nonclustered (SNO)
)
go

/*==============================================================*/
/* Table: subject                                               */
/*==============================================================*/
create table subject (
   SJNAME               smallint             not null,
   PLACE                char(40)             null,
   SINTRO               varchar(64)          null,
   constraint PK_SUBJECT primary key nonclustered (SJNAME)
)
go

alter table cc
   add constraint FK_CC_CC_COACH foreign key (CNO)
      references coach (CNO)
go

alter table cc
   add constraint FK_CC_CC2_CAR foreign key (LIC)
      references car (LIC)
go

alter table exam
   add constraint FK_EXAM_EXAM_STUDENT foreign key (SNO)
      references student (SNO)
go

alter table exam
   add constraint FK_EXAM_EXAM2_SUBJECT foreign key (SJNAME)
      references subject (SJNAME)
go

alter table expenditure
   add constraint FK_EXPENDIT_CEX_CAR foreign key (LIC)
      references car (LIC)
go

alter table expenditure
   add constraint FK_EXPENDIT_EXKIND_E_KIND foreign key (EKNO)
      references e_kind (EKNO)
go

alter table income
   add constraint FK_INCOME_INKIND_I_KIND foreign key (IKNO)
      references i_kind (IKNO)
go

alter table income
   add constraint FK_INCOME_SIN_STUDENT foreign key (SNO)
      references student (SNO)
go

alter table sc
   add constraint FK_SC_SC_COACH foreign key (CNO)
      references coach (CNO)
go

alter table sc
   add constraint FK_SC_SC2_STUDENT foreign key (SNO)
      references student (SNO)
go

alter table sc
   add constraint FK_SC_SC3_SUBJECT foreign key (SJNAME)
      references subject (SJNAME)
go

