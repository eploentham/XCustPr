-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: kfc_gl
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
-- Table structure for table `xcust_gl_int_tbl`
--

DROP TABLE IF EXISTS `xcust_gl_int_tbl`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `xcust_gl_int_tbl` (
  `Status_Code` varchar(50) COLLATE utf8_bin NOT NULL DEFAULT 'NEW',
  `Ledger_ID` decimal(17,2) NOT NULL,
  `Accounting_Date` date NOT NULL COMMENT 'ต้องอยู่ใน period ที่ยังเปิดอยู่\nDate (yyyy/mm/dd)\n\nจากระบบ MMX',
  `Journal_Source` varchar(25) COLLATE utf8_bin NOT NULL,
  `Journal_Category` varchar(25) COLLATE utf8_bin NOT NULL,
  `Currency_Code` varchar(25) COLLATE utf8_bin NOT NULL,
  `Created_Date` date NOT NULL,
  `Actual_Flag` varchar(1) COLLATE utf8_bin NOT NULL DEFAULT 'A',
  `SEGMENT1` varchar(25) COLLATE utf8_bin DEFAULT NULL,
  `SEGMENT2` varchar(25) COLLATE utf8_bin NOT NULL COMMENT 'Mapping Finainal Code จาก XCUST_MMX_DFT_TBL.FINANCE_CODE\n กับ VALUE SET = “XCUST_FINANCE” \nXCUST_ACC_SEGMENT_TBL. FLEX_VALUE\n',
  `SEGMENT3` varchar(25) COLLATE utf8_bin NOT NULL COMMENT 'Validate ข้อมูล XCUST_MMX_DFT_TBL .STORE_CODE\n กับ VALUE SET = “XCUST_STORE” \nXCUST_ACC_SEGMENT_TBL. FLEX_VALUE\n',
  `SEGMENT4` varchar(25) COLLATE utf8_bin NOT NULL,
  `SEGMENT5` varchar(25) COLLATE utf8_bin NOT NULL,
  `SEGMENT6` varchar(25) COLLATE utf8_bin NOT NULL,
  `Entered_Debit_Amount` decimal(17,2) NOT NULL COMMENT '1.	การคำนวณจะคำนวณตาม Financial Code ตรวจสอบเงื่อนไขจาก XCUST_FIN_CAL_TBL โดยใช้สูตรตามที่ระบุ XCUST_FIN_CAL_TBL.CAL_TEXT 2.	XCUST_FIN_CAL_TBL.ACCOUNT_TYPE = “DR”',
  `Entered_Credit_Amount` decimal(17,2) NOT NULL COMMENT '1.	การคำนวณจะคำนวณตาม Financial Code ตรวจสอบเงื่อนไขจาก XCUST_FIN_CAL_TBL โดยใช้สูตรตามที่ระบุ XCUST_FIN_CAL_TBL.CAL_TEXT 2.	XCUST_FIN_CAL_TBL.ACCOUNT_TYPE = “CR”'
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `xcust_gl_int_tbl`
--

LOCK TABLES `xcust_gl_int_tbl` WRITE;
/*!40000 ALTER TABLE `xcust_gl_int_tbl` DISABLE KEYS */;
/*!40000 ALTER TABLE `xcust_gl_int_tbl` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `xcust_mmx_dft_tbl`
--

DROP TABLE IF EXISTS `xcust_mmx_dft_tbl`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `xcust_mmx_dft_tbl` (
  `FILE_NAME` varchar(200) COLLATE utf8_bin NOT NULL COMMENT 'อ่านเฉพาะ File ที่ลงท้ายจาก Parameter File Type\nต้วอย่าง File Type = “dft” \nFile ที่อ่านได้คือ 83003dft.801\n',
  `STORE_CODE` char(5) COLLATE utf8_bin NOT NULL COMMENT 'เลขที่สาขา',
  `FINANCE_CODE` int(11) NOT NULL COMMENT 'ประเภทการขาย',
  `TRAN_DATE` date NOT NULL COMMENT 'Date (mm-dd-yy)',
  `AMT` decimal(17,2) DEFAULT NULL COMMENT 'ยอดขาย\nจากระบบ MMX column4',
  `VALIDATE_FLAG` char(1) COLLATE utf8_bin NOT NULL COMMENT 'Y = VALIDATE PASS\nE = VALIDATE NOT PASS\nN = NO VALIDATE\n',
  `PROCESS_FLAG` char(1) COLLATE utf8_bin NOT NULL COMMENT 'แสดงสถานะการ Import ข้อมูลเข้า ERP\nN = NO PROCESS\nP = PROCESSING\nY = PROCESS COMPLETE\nE = PROCESS ERROR\n',
  `ERROR_MSG` varchar(240) COLLATE utf8_bin NOT NULL COMMENT 'รายละเอียด Error\nError GL001-001: Not found File \nError GL001-002 : Date Format not correct \nError GL001-003 : Not found Store Code\nError GL001-004 : Not found Finance Code\nError GL001-005 : Amount are zero\nError GL001-006 : Transaction Date Not in Period Open\nError GL001-007 :Standard Interface Error'
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `xcust_mmx_dft_tbl`
--

LOCK TABLES `xcust_mmx_dft_tbl` WRITE;
/*!40000 ALTER TABLE `xcust_mmx_dft_tbl` DISABLE KEYS */;
/*!40000 ALTER TABLE `xcust_mmx_dft_tbl` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-10-09  7:40:28
