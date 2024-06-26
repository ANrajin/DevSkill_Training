USE [AspnetCoreB6]
GO
/****** Object:  StoredProcedure [dbo].[GetCourses]    Script Date: 1/1/2022 8:54:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER   PROCEDURE [dbo].[GetCourses]
@PageIndex int,
@PageSize int , 
@OrderBy nvarchar(50),
@Title nvarchar(250) = '%',
@RegistrationEndFrom datetime = null,
@RegistrationEndTo datetime = null,
@IsActive bit = null,
@Total int output,
@TotalDisplay int output

AS
BEGIN
	Declare @sql nvarchar(4000);
	Declare @countsql nvarchar(4000);
	Declare @paramList nvarchar(MAX); 
	Declare @countparamList nvarchar(MAX);
	
	SET NOCOUNT ON;

	Select @Total = count(*) from Courses;
	SET @countsql = 'select @TotalDisplay = count(*) from Courses where 1 = 1 ';
	
	IF @Title IS NOT NULL
	SET @countsql = @countsql + ' AND Title LIKE ''%'' + @xTitle + ''%''' 
	IF @IsActive IS NOT NULL
	SET @countsql = @countsql + ' AND IsActive = @xIsActive' 
	IF @RegistrationEndFrom IS NOT NULL
	SET @countsql = @countsql + ' AND RegistrationEnd >= @xRegistrationEndFrom'
	IF @RegistrationEndTo IS NOT NULL
	SET @countsql = @countsql + ' AND RegistrationEnd <= @xRegistrationEndTo' 


	SET @sql = 'select * from Courses where 1 = 1 '; 

	IF @Title IS NOT NULL
	SET @sql = @sql + ' AND Title LIKE ''%'' + @xTitle + ''%''' 
	IF @IsActive IS NOT NULL
	SET @sql = @sql + ' AND IsActive = @xIsActive' 
	IF @RegistrationEndFrom IS NOT NULL
	SET @sql = @sql + ' AND RegistrationEnd >= @xRegistrationEndFrom'
	IF @RegistrationEndTo IS NOT NULL
	SET @sql = @sql + ' AND RegistrationEnd <= @xRegistrationEndTo' 

	SET @sql = @sql + ' Order by '+@OrderBy+' OFFSET @PageSize * (@PageIndex - 1) 
	ROWS FETCH NEXT @PageSize ROWS ONLY';

	SELECT @countparamlist = '@xTitle nvarchar(250),
		@xRegistrationEndFrom datetime,
		@xRegistrationEndTo datetime,
		@xIsActive bit,
		@TotalDisplay int output' ;

	exec sp_executesql @countsql , @countparamlist ,
		@Title,
		@RegistrationEndFrom,
		@RegistrationEndTo,
		@IsActive,
		@TotalDisplay = @TotalDisplay output;

	SELECT @paramlist = '@xTitle nvarchar(250),
		@xRegistrationEndFrom datetime,
		@xRegistrationEndTo datetime,
		@xIsActive bit,
		@PageIndex int,
		@PageSize int';

	exec sp_executesql @sql , @paramlist ,
		@Title,
		@RegistrationEndFrom,
		@RegistrationEndTo,
		@IsActive, 
		@PageIndex,
		@PageSize;

	print @sql;
	
END