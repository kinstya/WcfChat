USE [ChatDb]
GO
/****** Object:  Table [dbo].[Conversations]    Script Date: 6/16/2015 3:41:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Conversations](
	[ConversationId] [int] IDENTITY(1,1) NOT NULL,
	[Started] [datetime] NOT NULL,
	[Ended] [datetime] NULL,
 CONSTRAINT [PK_dbo.Conversations] PRIMARY KEY CLUSTERED 
(
	[ConversationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Messages]    Script Date: 6/16/2015 3:41:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages](
	[MessageId] [int] IDENTITY(1,1) NOT NULL,
	[ParticipantId] [int] NOT NULL,
	[ConversationId] [int] NOT NULL,
	[Text] [nvarchar](max) NULL,
	[Time] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Messages] PRIMARY KEY CLUSTERED 
(
	[MessageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Participants]    Script Date: 6/16/2015 3:41:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Participants](
	[ParticipantId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Joined] [datetime] NOT NULL,
	[Left] [datetime] NULL,
 CONSTRAINT [PK_dbo.Participants] PRIMARY KEY CLUSTERED 
(
	[ParticipantId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Messages_dbo.Conversations_ConversationId] FOREIGN KEY([ConversationId])
REFERENCES [dbo].[Conversations] ([ConversationId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_dbo.Messages_dbo.Conversations_ConversationId]
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Messages_dbo.Participants_ParticipantId] FOREIGN KEY([ParticipantId])
REFERENCES [dbo].[Participants] ([ParticipantId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_dbo.Messages_dbo.Participants_ParticipantId]
GO
