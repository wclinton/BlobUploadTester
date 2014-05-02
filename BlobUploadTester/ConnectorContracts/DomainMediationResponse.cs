using System;
using System.Runtime.Serialization;

namespace BlobUploadTester.ConnectorContracts
{
    /// <summary>
    /// DomainMediationResponse
    /// </summary>
    public class DomainMediationResponse
    {
        public DomainMediation DomainMediationEntry { get;  set; }


        public Guid RequestId { get;  set; }

        public Guid Id { get;  set; }

        public DateTime CreatedTimestampUtc { get;  set; }

        public String IndirectPayloadUploadId { get;  set; }

        // To support forward-compatible data contracts
        public ExtensionDataObject ExtensionData { get; set; }

     
    }
}