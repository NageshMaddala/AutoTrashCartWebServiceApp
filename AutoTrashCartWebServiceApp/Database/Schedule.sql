USE [AutoTrashCartDB]
GO

/****** Object:  Table [dbo].[Schedule]    Script Date: 11/25/2019 4:53:19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER TABLE [dbo].[Schedule](
	[ScheduleId] [int] IDENTITY(1,1) NOT NULL,
	[Token] [nvarchar](50) NOT NULL,
	[Day] [int] NOT NULL,
	[Pickup] [time](0) NOT NULL,
	[Holidays] [bit] NOT NULL,
 CONSTRAINT [PK_Schedule] PRIMARY KEY CLUSTERED 
(
	[ScheduleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


