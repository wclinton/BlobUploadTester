using System;

namespace BlobUploadTester.ConnectorContracts
{
    /// <summary>
    /// DomainMediationRequest
    /// </summary>
    public class DomainMediationRequest
    {
        public Guid Id { get; set; }
        public DateTime CreatedTimestampUtc { get; set; }
        public int RetryCount { get; set; }
        public string RequestingUser { get; set; }
        public string ProjectName { get; set; }
        public string RequestSummary { get; set; }
        public string IndirectPayloadDownloadId { get; set; }
        public int Priority { get; set; }
        public DomainMediation DomainMediationEntry { get; set; }
    }
}
