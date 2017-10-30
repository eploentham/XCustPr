create database kfc_gl DEFAULT CHARSET=utf8;

CREATE TABLE XCUST_LINFOX_PR_TBL
(
  COMPANY varchar(5),
  PO_NUMBER varchar(25),
  LINE_NUMBER decimal(17,2),
  SUPPLIER_CODE varchar(8),
  ORDER_DATE date,
  ORDER_TIME time,
  ITEM_NUMBER varchar(20),
  QTY decimal(17,2),
  UOM varchar(2),
  DELIVERY_INSTRUCTION varchar(60),
  VALIDATE_FLAG varchar(1),
  PROCESS_FLAG varchar(1),
  ERROR_MSG varchar(255)
  
  
)ENGINE=MyISAM AUTO_INCREMENT=0 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

CREATE TABLE XCUST_POR_REQ_HEADER_INT_ALL
(
  PO_NUMBER varchar(50),
  import_source varchar(25),
  BU varchar(240),
  Batch_ID  int(11),
  PR_STATAUS varchar(25),
  PR_APPROVER varchar(240),
  ENTER_BY varchar(240),
  Requisition_Number varchar(64),
  Description varchar(240),
  LINFOX_PR varchar(30),
  ATTRIBUTE1 varchar(150),
  ATTRIBUTE_DATE1 date,
  ATTRIBUTE_TIMESTAMP1 datetime,
  PROCESS_FLAG varchar(1)
  
  
)ENGINE=MyISAM AUTO_INCREMENT=0 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

CREATE TABLE XCUST_POR_REQ_LINE_INT_ALL
(
  PO_NUMBER varchar(50),
  PO_LINE_NUMBER varchar(50),
  INVENTORY varchar(25),
  BU  varchar(240),
  Deliver_to_Location varchar(60),
  Deliver_to_Organization varchar(240),
  PR_APPROVER varchar(240),
  Subinventory varchar(10),
  requester varchar(240),
  Category_Name varchar(250),
  Need_by_Date date,
  ITEM_NUMBER varchar(30),
  Goods varchar(30),
  QTY decimal(17,2),
  CURRENCY_CODE varchar(15),
  Price decimal(17,2),
  Procurement_BU varchar(240),
  SUPPLIER_CODE varchar(360),
  Supplier_Site varchar(240),
  LINFOX_PR varchar(30),
  ATTRIBUTE1 varchar(150),
  ATTRIBUTE_NUMBER1 decimal(17,2),
  ATTRIBUTE_DATE1 date,
  ATTRIBUTE_TIMESTAMP1 datetime,
  PROCESS_FLAG varchar(1)
  
  
)ENGINE=MyISAM AUTO_INCREMENT=0 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;


CREATE TABLE XCUST_POR_REQ_DIST_INT_ALL
(
  PO_NUMBER varchar(50),
  PO_LINE_NUMBER varchar(50),
  Program_running varchar(50),
  QTY  decimal(17,2),
  CHARGE_ACCOUNT_SEGMENT1 varchar(25),
  CHARGE_ACCOUNT_SEGMENT2 varchar(25),
  CHARGE_ACCOUNT_SEGMENT3 varchar(25),
  CHARGE_ACCOUNT_SEGMENT4 varchar(25),
  CHARGE_ACCOUNT_SEGMENT5 varchar(25),
  CHARGE_ACCOUNT_SEGMENT6 varchar(25),
  ATTRIBUTE_CATEGORY varchar(30),
  ATTRIBUTE1  varchar(150),
  ATTRIBUTE_NUMBER1  decimal(17,2),
  ATTRIBUTE_DATE1 date,
  ATTRIBUTE_TIMESTAMP1 datetime,
  PROCESS_FLAG varchar(1)
  
  
)ENGINE=MyISAM AUTO_INCREMENT=0 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
