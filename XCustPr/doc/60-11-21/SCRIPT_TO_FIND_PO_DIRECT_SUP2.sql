  declare @vpr_id as nvarchar;
  declare @vpr_line_id as nvarchar;
  declare @vpr_pr_number nvarchar;
  declare @vtest as nvarchar;
  
  declare @cur_data as CURSOR ;
  set @cur_data = cursor for
  select pr.REQUISITION_HEADER_ID,pr.REQUISITION_LINE_ID,pr.REQUISITION_NUMBER
  from xcust_po_tbl po
  ,xcust_pr_tbl pr
  where po.REQUISITION_HEADER_ID != 0
  and po.po_header_id in (300000001040981,300000001045456,300000001045847,300000001046243,300000001046255)
  and po.REQUISITION_HEADER_ID = pr.REQUISITION_HEADER_ID
  and po.REQUISITION_LINE_ID = pr.REQUISITION_LINE_ID
  order by po_header_id,po_line_id;
    
open  @cur_data;
fetch next from @cur_data into @vpr_id,@vpr_line_id,@vpr_pr_number;

WHILE @@FETCH_STATUS = 0   
BEGIN   
      begin
      if @vpr_pr_number = '117000003'
        update xcust_pr_tbl
        set ATTRIBUTE_CATEGORY = 'LINFOX_PR'
        ,attribute1 = 'LINFOX'
        ,attribute2 = 'PO001'
        ,ATTRIBUTE_CATEGORY_l = 'LINFOX_PR'
        ,attribute1_l = 'PO001'
        ,attribute2_l = '1.000'
        where REQUISITION_HEADER_ID = @vpr_id
        and REQUISITION_LINE_ID = @vpr_line_id
       
      else if @vpr_pr_number = '117000001'
         update xcust_pr_tbl
        set ATTRIBUTE_CATEGORY = 'LINFOX_PR'
        ,attribute1 = 'LINFOX'
        ,attribute2 = 'PO002'
        ,ATTRIBUTE_CATEGORY_l = 'LINFOX_PR'
        ,attribute1_l = 'PO002'
        ,attribute2_l = '1.000'
        where REQUISITION_HEADER_ID = @vpr_id
        and REQUISITION_LINE_ID = @vpr_line_id
      else if @vpr_pr_number = '117000007' 
        update xcust_pr_tbl
        set ATTRIBUTE_CATEGORY = 'LINFOX_PR'
        ,attribute1 = 'LINFOX'
        ,ATTRIBUTE_CATEGORY_l = 'LINFOX_PR'
        ,attribute2 = 'PO003'
        ,attribute1_l = 'PO003'
        ,attribute2_l = '1.000'
        where REQUISITION_HEADER_ID = @vpr_id
        and REQUISITION_LINE_ID = @vpr_line_id
      else if @vpr_pr_number = '117000014' and @vpr_line_id = '300000001048167' 
        update xcust_pr_tbl
        set ATTRIBUTE_CATEGORY = 'LINFOX_PR'
        ,attribute1 = 'LINFOX'
        ,attribute2 = 'PO004'
        ,ATTRIBUTE_CATEGORY_l = 'LINFOX_PR'
        ,attribute1_l = 'PO004'
        ,attribute2_l = '1.000'
        where REQUISITION_HEADER_ID = @vpr_id
        and REQUISITION_LINE_ID = @vpr_line_id
        
      else if @vpr_pr_number = '117000014' and @vpr_line_id = '300000001048169' 
        update xcust_pr_tbl
        set ATTRIBUTE_CATEGORY = 'LINFOX_PR'
        ,attribute1 = 'LINFOX'
        ,attribute2 = 'PO004'
        ,ATTRIBUTE_CATEGORY_l = 'LINFOX_PR'
        ,attribute1_l = 'PO004'
        ,attribute2_l = '2.000'
        where REQUISITION_HEADER_ID = @vpr_id
        and REQUISITION_LINE_ID = @vpr_line_id      
      else
        update xcust_pr_tbl
        set ATTRIBUTE_CATEGORY = 'LINFOX_PR'
        ,attribute1 = 'LINFOX'
        ,attribute2 = 'PO005'
        ,ATTRIBUTE_CATEGORY_l = 'LINFOX_PR'
        ,attribute1_l = 'PO005'
        ,attribute2_l = '1.000'
        where REQUISITION_HEADER_ID = @vpr_id
        and REQUISITION_LINE_ID = @vpr_line_id
      end

FETCH NEXT FROM @cur_data; 
END 
commit

close @cur_data;
deallocate @cur_data;


