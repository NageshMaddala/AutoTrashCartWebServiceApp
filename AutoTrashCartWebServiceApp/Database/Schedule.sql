/****** Object:  Table [dbo].[Schedule]    Script Date: 2/20/2020 2:48:03 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Schedule](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ScheduleId] [nvarchar](50) NOT NULL,
	[Day] [int] NOT NULL,
	[Pickup] [time](0) NOT NULL,
	[Holidays] [bit] NOT NULL,
 CONSTRAINT [PK_Schedule] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


