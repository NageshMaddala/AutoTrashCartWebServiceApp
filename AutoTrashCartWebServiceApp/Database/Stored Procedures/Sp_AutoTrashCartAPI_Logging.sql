/****** Object:  StoredProcedure [dbo].[AutoTrashCartAPI_Logging]    Script Date: 2/20/2020 2:53:14 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROC [dbo].[AutoTrashCartAPI_Logging] 
     @Host NVARCHAR(100)
	,@Headers NVARCHAR(500)
	,@StatusCode VARCHAR(50)
	,@RequestBody NVARCHAR(max)
	,@RequestedMethod NVARCHAR(max)
	,@UserHostAddress NVARCHAR(100)
	,@Useragent NVARCHAR(100)
	,@AbsoluteUri NVARCHAR(100)
	,@RequestType NVARCHAR(100)
AS
BEGIN
	INSERT INTO [dbo].[AutoTrashCartAPILog] (
		[Host]
		,[Headers]
		,[StatusCode]
		,[TimeUtc]
		,[RequestBody]
		,[RequestedMethod]
		,[UserHostAddress]
		,[Useragent]
		,[AbsoluteUri]
		,[RequestType]
		)
	VALUES (
		@Host
		,@Headers
		,@StatusCode
		,getdate()
		,@RequestBody
		,@RequestedMethod
		,@UserHostAddress
		,@Useragent
		,@AbsoluteUri
		,@RequestType
		)
END

GO


