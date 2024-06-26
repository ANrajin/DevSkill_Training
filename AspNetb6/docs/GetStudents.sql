USE [AspnetCoreB6]
GO
/****** Object:  StoredProcedure [dbo].[GetCourses]    Script Date: 1/1/2022 8:54:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER   PROCEDURE [dbo].[GetStudents]
@PageIndex int,
@PageSize int , 
@OrderBy nvarchar(50),
@Name nvarchar(250) = '%',
@Address nvarchar(250) = '%',
@Total int output,
@TotalDisplay int output

AS
BEGIN
	Declare @sql nvarchar(4000);
	Declare @countsql nvarchar(4000);
	Declare @paramList nvarchar(MAX); 
	Declare @countparamList nvarchar(MAX);
	
	SET NOCOUNT ON;

	Select @Total = count(*) from Students;
	SET @countsql = 'select @TotalDisplay = count(*) from Students where 1 = 1 ';
	
	IF @Name IS NOT NULL
	SET @countsql = @countsql + ' AND Name LIKE ''%'' + @xName + ''%''' 
	IF @Address IS NOT NULL
	SET @countsql = @countsql + ' AND Address LIKE ''%'' + @xAddress + ''%'''   

	SET @sql = 'select * from Students where 1 = 1 '; 

	IF @Name IS NOT NULL
	SET @sql = @sql + ' AND Name LIKE ''%'' + @xName + ''%''' 
	IF @Address IS NOT NULL
	SET @sql = @sql + ' AND Address LIKE ''%'' + @xAddress + ''%'''

	SET @sql = @sql + ' Order by '+@OrderBy+' OFFSET @PageSize * (@PageIndex - 1) 
	ROWS FETCH NEXT @PageSize ROWS ONLY';

	SELECT @countparamlist = '@xName nvarchar(250),
		@xAddress nvarchar(250),
		@TotalDisplay int output' ;

	exec sp_executesql @countsql , @countparamlist ,
		@Name,
		@Address,
		@TotalDisplay = @TotalDisplay output;

	SELECT @paramlist = '@xName nvarchar(250),
		@xAddress nvarchar(250),
		@PageIndex int,
		@PageSize int';

	exec sp_executesql @sql , @paramlist ,
		@Name,
		@Address,
		@PageIndex,
		@PageSize;
	
END