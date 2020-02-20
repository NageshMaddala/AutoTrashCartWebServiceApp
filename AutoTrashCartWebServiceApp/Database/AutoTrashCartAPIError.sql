/****** Object:  Table [dbo].[AutoTrashCartAPIError]    Script Date: 2/20/2020 2:49:05 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AutoTrashCartAPIError](
	[APIErrorId] [uniqueidentifier] NOT NULL,
	[RequestMethod] [varchar](max) NULL,
	[RequestUri] [varchar](max) NULL,
	[TimeUtc] [datetime] NULL,
	[Message] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_API_Error] PRIMARY KEY CLUSTERED 
(
	[APIErrorId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[AutoTrashCartAPIError] ADD  CONSTRAINT [DF_API_Error_APIErrorId]  DEFAULT (newid()) FOR [APIErrorId]
GO


