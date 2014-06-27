create procedure sp_getUserInfo
@BADGENUMBER nvarchar(24)
as
begin
	select *
	from USERINFO ui
	where ui.BADGENUMBER = @BADGENUMBER
end