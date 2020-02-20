/****** Object:  Table [dbo].[AutoTrashCartAPILog]    Script Date: 2/20/2020 2:50:57 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AutoTrashCartAPILog](
	[LogID] [uniqueidentifier] NOT NULL,
	[Host] [nvarchar](100) NULL,
	[Headers] [nvarchar](1000) NULL,
	[StatusCode] [nvarchar](50) NULL,
	[TimeUtc] [datetime] NULL,
	[RequestBody] [nvarchar](max) NULL,
	[RequestedMethod] [nvarchar](max) NULL,
	[UserHostAddress] [nvarchar](100) NULL,
	[Useragent] [nvarchar](100) NULL,
	[AbsoluteUri] [nvarchar](100) NULL,
	[RequestType] [nvarchar](100) NULL,
 CONSTRAINT [PK_API_Log] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[AutoTrashCartAPILog] ADD  CONSTRAINT [DF_API_Log_LogID]  DEFAULT (newid()) FOR [LogID]
GO


