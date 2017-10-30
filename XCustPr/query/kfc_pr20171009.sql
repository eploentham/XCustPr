-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: kfc_pr
-- ------------------------------------------------------
-- Server version	5.5.5-10.1.25-MariaDB

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `xcust_linfox_pr_tbl`
--
use kfc_pr;
DROP TABLE IF EXISTS `xcust_linfox_pr_tbl`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `xcust_linfox_pr_tbl` (
  `COMPANY` varchar(5) COLLATE utf8_bin NOT NULL,
  `PO_NUMBER` varchar(25) COLLATE utf8_bin NOT NULL,
  `LINE_NUMBER` decimal(17,2) NOT NULL COMMENT 'Number (7,3)\n-	Format : 9.999\n-	Ex : 1.000\n',
  `SUPPLIER_CODE` varchar(8) COLLATE utf8_bin NOT NULL,
  `ORDER_DATE` date NOT NULL COMMENT 'Date\n-	Format yyyymmdd\n',
  `ORDER_TIME` time NOT NULL COMMENT 'Time \n-	Format hh24miss\n',
  `ITEM_NUMBER` varchar(20) COLLATE utf8_bin NOT NULL,
  `QTY` decimal(17,2) NOT NULL COMMENT 'ปริมาณสั่งซื้อ',
  `UOM` varchar(2) COLLATE utf8_bin NOT NULL,
  `DELIVERY_INSTRUCTION` varchar(60) COLLATE utf8_bin NOT NULL,
  `VALIDATE_FLAG` varchar(1) COLLATE utf8_bin DEFAULT NULL COMMENT 'Y = VALIDATE PASS\nE = VALIDATE NOT PASS\nN = NO VALIDATE\n',
  `PROCESS_FLAG` varchar(1) COLLATE utf8_bin DEFAULT NULL COMMENT 'N = NO PROCESS\nP = PROCESSING\nY = PROCESS COMPLETE\nE = PROCESS ERROR\n',
  `ERROR_MSG` varchar(255) COLLATE utf8_bin DEFAULT NULL COMMENT 'รายละเอียด Error'
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `xcust_linfox_pr_tbl`
--

LOCK TABLES `xcust_linfox_pr_tbl` WRITE;
/*!40000 ALTER TABLE `xcust_linfox_pr_tbl` DISABLE KEYS */;
/*!40000 ALTER TABLE `xcust_linfox_pr_tbl` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `xcust_por_req_dist_int_all`
--

DROP TABLE IF EXISTS `xcust_por_req_dist_int_all`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `xcust_por_req_dist_int_all` (
  `PO_NUMBER` varchar(50) COLLATE utf8_bin NOT NULL COMMENT 'ระบุเลขที่เลขที่ PO ที่มาจากระบบ Linfox',
  `PO_LINE_NUMBER` varchar(50) COLLATE utf8_bin NOT NULL COMMENT 'ระบุเลขที่เลขที่ PO ที่มาจากระบบ Linfox',
  `Program_running` varchar(50) COLLATE utf8_bin DEFAULT NULL COMMENT 'ระบุ Running Distribution ตาม Interface line Key',
  `QTY` decimal(17,2) NOT NULL COMMENT 'Purcurement >(P) Purchase Requisition > Billing  > (F) Quatity',
  `CHARGE_ACCOUNT_SEGMENT1` varchar(25) COLLATE utf8_bin NOT NULL COMMENT 'Purcurement >(P) Purchase Requisition > Billing  > (F) Charge Account Segment',
  `CHARGE_ACCOUNT_SEGMENT2` varchar(25) COLLATE utf8_bin NOT NULL COMMENT 'Purcurement >(P) Purchase Requisition > Billing  > (F) Charge Account Segment',
  `CHARGE_ACCOUNT_SEGMENT3` varchar(25) COLLATE utf8_bin NOT NULL COMMENT 'Purcurement >(P) Purchase Requisition > Billing  > (F) Charge Account Segment',
  `CHARGE_ACCOUNT_SEGMENT4` varchar(25) COLLATE utf8_bin NOT NULL COMMENT 'Purcurement >(P) Purchase Requisition > Billing  > (F) Charge Account Segment',
  `CHARGE_ACCOUNT_SEGMENT5` varchar(25) COLLATE utf8_bin NOT NULL COMMENT 'Purcurement >(P) Purchase Requisition > Billing  > (F) Charge Account Segment',
  `CHARGE_ACCOUNT_SEGMENT6` varchar(25) COLLATE utf8_bin NOT NULL COMMENT 'Purcurement >(P) Purchase Requisition > Billing  > (F) Charge Account Segment',
  `ATTRIBUTE_CATEGORY` varchar(30) COLLATE utf8_bin DEFAULT NULL,
  `ATTRIBUTE1` varchar(150) COLLATE utf8_bin DEFAULT NULL,
  `ATTRIBUTE_NUMBER1` decimal(17,2) DEFAULT NULL,
  `ATTRIBUTE_DATE1` date DEFAULT NULL COMMENT 'DATE \nFORMAT : YYYY-MON-DD\n',
  `ATTRIBUTE_TIMESTAMP1` datetime DEFAULT NULL COMMENT 'DATETIME\nFORMAT : YYYY-MON-DD HH24:MI:SS\n',
  `PROCESS_FLAG` varchar(1) COLLATE utf8_bin NOT NULL COMMENT 'PROGRAM ระบุ\nN = NO PROCESS\nP = PROCESSING\nY = PROCESS COMPLETE\nE = PROCESS ERRORs\n'
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `xcust_por_req_dist_int_all`
--

LOCK TABLES `xcust_por_req_dist_int_all` WRITE;
/*!40000 ALTER TABLE `xcust_por_req_dist_int_all` DISABLE KEYS */;
/*!40000 ALTER TABLE `xcust_por_req_dist_int_all` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `xcust_por_req_header_int_all`
--

DROP TABLE IF EXISTS `xcust_por_req_header_int_all`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `xcust_por_req_header_int_all` (
  `PO_NUMBER` varchar(50) COLLATE utf8_bin NOT NULL COMMENT 'ระบุเลขที่เลขที่ PO ที่มาจากระบบ Linfox',
  `import_source` varchar(25) COLLATE utf8_bin NOT NULL,
  `Requisitioning_BU` varchar(240) COLLATE utf8_bin DEFAULT NULL COMMENT 'Purcurement >(P) Purchase Requisition >(F)Requisition BU',
  `Batch_ID` int(11) DEFAULT NULL COMMENT 'YYYYMMDD+RUNNING 2 หลัก',
  `PR_STATAUS` varchar(25) COLLATE utf8_bin DEFAULT NULL COMMENT 'Purcurement >(P) Purchase Requisition >(F)Status',
  `PR_APPROVER` varchar(240) COLLATE utf8_bin DEFAULT NULL,
  `ENTER_BY` varchar(240) COLLATE utf8_bin DEFAULT NULL,
  `Requisition_Number` varchar(64) COLLATE utf8_bin DEFAULT NULL COMMENT 'Purcurement >(P) Purchase Requisition >(F)Requisition',
  `Description` varchar(240) COLLATE utf8_bin DEFAULT NULL,
  `LINFOX_PR` varchar(30) COLLATE utf8_bin DEFAULT NULL,
  `ATTRIBUTE1` varchar(150) COLLATE utf8_bin DEFAULT NULL,
  `ATTRIBUTE_DATE1` date DEFAULT NULL COMMENT 'DATE \nFORMAT : YYYY-MON-DD\n',
  `ATTRIBUTE_TIMESTAMP1` datetime DEFAULT NULL COMMENT 'DATETIME\nFORMAT : YYYY-MON-DD HH24:MI:SS\n',
  `PROCESS_FLAG` varchar(1) COLLATE utf8_bin NOT NULL COMMENT 'PROGRAM ระบุ\nN = NO PROCESS\nP = PROCESSING\nY = PROCESS COMPLETE\nE = PROCESS ERRORs\n'
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `xcust_por_req_header_int_all`
--

LOCK TABLES `xcust_por_req_header_int_all` WRITE;
/*!40000 ALTER TABLE `xcust_por_req_header_int_all` DISABLE KEYS */;
/*!40000 ALTER TABLE `xcust_por_req_header_int_all` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `xcust_por_req_line_int_all`
--

DROP TABLE IF EXISTS `xcust_por_req_line_int_all`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `xcust_por_req_line_int_all` (
  `PO_NUMBER` varchar(50) COLLATE utf8_bin NOT NULL COMMENT 'ระบุเลขที่เลขที่ PO ที่มาจากระบบ Linfox',
  `PO_LINE_NUMBER` varchar(50) COLLATE utf8_bin NOT NULL COMMENT 'ระบุเลขที่เลขที่ PO ที่มาจากระบบ Linfox',
  `INVENTORY` varchar(25) COLLATE utf8_bin NOT NULL,
  `Requisitioning_BU` varchar(240) COLLATE utf8_bin NOT NULL COMMENT 'Purcurement >(P) Purchase Requisition > Requisiton Lines > (F) Requisitioning BU',
  `Deliver_to_Location` varchar(60) COLLATE utf8_bin NOT NULL COMMENT 'Location ที่ต้องการให้ส่งของ\nPurcurement >(P) Purchase Requisition > Requisiton Lines > (F) Deriver to location',
  `Deliver_to_Organization` varchar(18) COLLATE utf8_bin NOT NULL COMMENT 'ระบุ Org Code ที่ต้องการให้ส่งของ\nPurcurement >(P) Purchase Requisition > Requisiton Lines > (F) Deriver to Orgainzation',
  `PR_APPROVER` varchar(240) COLLATE utf8_bin DEFAULT NULL COMMENT 'ระบุ approver ของ pr',
  `Subinventory` varchar(10) COLLATE utf8_bin DEFAULT NULL COMMENT 'ระบุ Subinventory Code ที่ต้องการของ',
  `requester` varchar(240) COLLATE utf8_bin NOT NULL COMMENT 'ระบุ E-mail คนที่ขอซื้อ',
  `Category_Name` varchar(250) COLLATE utf8_bin NOT NULL COMMENT 'Purcurement >(P) Purchase Requisition > Requisiton Lines > (F) Category Name',
  `Need_by_Date` date DEFAULT NULL COMMENT 'วันที่ต้องการสินค้า',
  `ITEM_NUMBER` varchar(300) COLLATE utf8_bin DEFAULT NULL COMMENT 'Purcurement >(P) Purchase Requisition > Requisiton Lines > (F) Item',
  `Goods` varchar(30) COLLATE utf8_bin NOT NULL COMMENT 'ระบุ Line Type ของ PR\nPurcurement >(P) Purchase Requisition > Requisiton Lines > (F) Destination Type',
  `QTY` decimal(17,2) NOT NULL COMMENT 'ระบุ qty ของ line pr\nPurcurement >(P) Purchase Requisition > Requisiton Lines > (F) Quantity',
  `CURRENCY_CODE` varchar(15) COLLATE utf8_bin NOT NULL COMMENT 'Currency ของ PR Line\nPurcurement >(P) Purchase Requisition > Requisiton Lines > (F) Quantity',
  `Price` decimal(17,2) NOT NULL COMMENT 'ราคาของ Unit Price ของ Item\n\nPurcurement >(P) Purchase Requisition > Requisiton Lines > (F)Price',
  `Procurement_BU` varchar(240) COLLATE utf8_bin NOT NULL COMMENT 'BU ของ PR',
  `SUPPLIER_CODE` varchar(360) COLLATE utf8_bin NOT NULL COMMENT 'Purcurement >(P) Purchase Requisition > Requisiton Lines > (F) Suggest Supplier',
  `Supplier_Site` varchar(240) COLLATE utf8_bin NOT NULL COMMENT 'Supplier Site Code',
  `LINFOX_PR` varchar(30) COLLATE utf8_bin DEFAULT NULL COMMENT 'ระบุ  ATTRIBUTE CATEGORY (กรณีใช้ Flexfiled)',
  `ATTRIBUTE1` varchar(150) COLLATE utf8_bin DEFAULT NULL COMMENT 'ข้อมูลเพิ่มเติม (รองรับอนาคต)',
  `ATTRIBUTE_NUMBER1` decimal(17,2) DEFAULT NULL COMMENT 'ข้อมูลเพิ่มเติม (รองรับอนาคต)',
  `ATTRIBUTE_DATE1` date DEFAULT NULL COMMENT 'DATE \nFORMAT : YYYY-MON-DD\n',
  `ATTRIBUTE_TIMESTAMP1` datetime DEFAULT NULL COMMENT 'DATETIME\nFORMAT : YYYY-MON-DD HH24:MI:SS\n',
  `PROCESS_FLAG` varchar(1) COLLATE utf8_bin NOT NULL COMMENT 'PROGRAM ระบุ\nN = NO PROCESS\nP = PROCESSING\nY = PROCESS COMPLETE\nE = PROCESS ERRORs\n'
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `xcust_por_req_line_int_all`
--

LOCK TABLES `xcust_por_req_line_int_all` WRITE;
/*!40000 ALTER TABLE `xcust_por_req_line_int_all` DISABLE KEYS */;
/*!40000 ALTER TABLE `xcust_por_req_line_int_all` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-10-09  7:40:14
