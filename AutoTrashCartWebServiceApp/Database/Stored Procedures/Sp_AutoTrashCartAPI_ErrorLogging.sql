/****** Object:  StoredProcedure [dbo].[AutoTrashCartAPI_ErrorLogging]    Script Date: 2/20/2020 2:52:05 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER Proc [dbo].[AutoTrashCartAPI_ErrorLogging]
 @Message nvarchar(max)
,@RequestMethod varchar(50)
,@RequestUri varchar(50)
,@TimeUtc datetime
as
begin

INSERT INTO [dbo].[AutoTrashCartAPIError]
           (
            [Message]
           ,[RequestMethod]
           ,[RequestUri]
           ,[TimeUtc])
     VALUES
           (
            @Message 
           ,@RequestMethod 
           ,@RequestUri 
           ,@TimeUtc)
end


GO


