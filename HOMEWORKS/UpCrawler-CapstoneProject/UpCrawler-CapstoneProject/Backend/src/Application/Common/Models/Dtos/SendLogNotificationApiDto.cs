namespace Application.Common.Models.Dtos
{
    public class SendLogNotificationApiDto
    {
        public SeleniumLogDto Log { get; set; }

        public SeleniumLogDto ProductLog { get; set; }

        public string ConnectionId { get; set; }

        public SendLogNotificationApiDto(SeleniumLogDto log, string connectionId, SeleniumLogDto productLog)
        {
            Log = log;

            ConnectionId = connectionId;

            ProductLog = productLog;
        }
    }
}
