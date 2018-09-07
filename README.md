# Platform
Multi Platforms

SQL_Select Column
=======


Table_Name : tên bảng cần hiển thị trên GridView

Colomn_Name : tên cột cần khai báo trên GridView(bắt buộc phải có ID vị trí đầu, status vị trí 2)

Table_Name_Count : tên bảng để count với quan hệ liên kết

ID_Table_Name_Count :ID bảng để count với quan hệ liên kết(thường trùng với ID Table_Name)

As_Name: Tên thay thế cột Table_Name_Count 

Group_by: có sử dụng group by trong sql hay ko

SQL ListBox Function Select Item
=======
#### SQL_ListBox(string Table_Name, string Column_Name, string ID_Table_Name, string Table_Name_From, string ID_Table_Name_From, int NOT_IN)

Table_Name : tên bảng cần khai báo trên ListBox

Column_Name : tên cột cần hiển thị trên ListBox

ID_Table_Name : ID bảng cần khai báo trên ListBox

Table_Name_From : tên bảng cần so sánh giá trị tồn tại

ID_Table_Name_From : ID bảng cần so sánh giá trị tồn tại

NOT_IN : không tồn tại(true/false)
