
start loop

select PO.deliver_date,t.supplier_number,count(*)
from xcust_PO_TBL po
,XCUST_SUPPLIER_MST_TBL t
where po.VENDOR_ID = t.VENDOR_ID 
and t.ATTRIBUTE1 = 'Y'
and (po.gen_outboud_flag = '' or po.gen_outboud_flag  is null)
group by  PO.deliver_date,t.supplier_number





query ใน loop เพื่อดึงข้อมูล gen text
SELECT po.CREATION_DATE, po.PO_LINE_ID,po.SEGMENT1 as po_number,po.LINE_NUM, po.QUANTITY, po.VENDOR_ID, po.PRC_BU_ID, po.ITEM_ID 
, po.ITEM_DESCRIPTION, po.QUANTITY_RECEIPT, po.QUANTITY, po.UOM_CODE, po.UNIT_PRICE, po.LINE_TYPE_ID, po.PAYMENT_TERM, po.CURRENCY_CODE
, po.REVISION_NUM ,po.SEGMENT1, po.ACC_SEGMENT1, po.ACC_SEGMENT2,po.TAX_CODE, po.PO_HEADER_ID, po.DELIVER_DATE 
From xcust_po_tbl po Where po.VENDOR_ID =300000000943255   and po.DELIVER_DATE = '2017-12-28'   
Order By po.SEGMENT1 





select *
from xcust_PO_TBL
where GEN_OUTBOUD_FLAG = 'Y' and DELIVER_DATE = '2017-12-28'


query ที่ใช้ เพื่อ test PO006
update xcust_po_tbl
set GEN_OUTBOUD_FLAG = null
where GEN_OUTBOUD_FLAG = 'Y' and DELIVER_DATE = '2017-12-28'