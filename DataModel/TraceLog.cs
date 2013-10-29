using System;

namespace DataModel
{
    public class TraceLog
    {
        public Int64 LogId { get; set; }
        public DateTime LogDate { get; set; }
        public string LogLevel { get; set; }
        public string LogMessage { get; set; }

    }

//    CREATE TABLE [dbo].[TestLog2](
//    [LogId] [bigint] IDENTITY(1,1) NOT NULL,
//    [LogDate] [datetime] NOT NULL,
//    [LogLevel] [nvarchar](20) NOT NULL,
//    [LogMessage] [nvarchar](max) NOT NULL,
// CONSTRAINT [PK_TestLog2] PRIMARY KEY CLUSTERED 
//(
//    [LogId] ASC
//)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
//) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
}
