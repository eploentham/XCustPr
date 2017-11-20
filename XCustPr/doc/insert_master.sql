insert into XCUST_SUPPLIER_MST_TBL ([SUPPLIER_REG_ID]
      ,[VENDOR_ID]
      ,[SUPPLIER_NUMBER]
      ,[SUPPLIER_NAME]
      ,[LAST_UPDATE_DATE]
      ,[CREATION_DATE]
      ,[ATTRIBUTE1]
      ,[ATTRIBUTE2]
      ,[ATTRIBUTE3]
      ,[ATTRIBUTE4]
      ,[ATTRIBUTE5])
	  Values('123456','654321','S72','Test','',getdate(),'Y','','','','');

update xcust_PO_TBL
  set ITEM_ID = '300000001662281', VENDOR_ID = '654321'
  where PO_HEADER_ID = '300000001591624'

 update XCUST_ORGANIZATION_MST_TBL
  set INVENTORY_FLAG = 'Y'
  where ORGANIZATION_ID = '300000000949654'


 update xcust_item_mst_tbl
  set ITEM_CODE = 'THF2152'
  where ITEM_CODE =   'RDK0010T'

