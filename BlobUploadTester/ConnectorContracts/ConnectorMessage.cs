using System;
using System.Runtime.Serialization;

namespace BlobUploadTester.ConnectorContracts
{
    /// <summary>
    /// Data contract for messages exchanged between the cloud, the connector and other platform components that are making requests intended for
    /// the connector
    /// </summary>
    [DataContract]
    public class ConnectorMessage
    {


        public ConnectorMessage()
        {
            Id = Guid.NewGuid();
            TimeStamp = DateTime.UtcNow;
            Version = 1;
            CorrelationId = Guid.Empty;
        }
        
        /// <summary>
        /// Unique identifier for this message
        /// </summary>
        [DataMember(Name = "id")]
        public Guid Id { get; set; }
        /// <summary>
        /// Message type identifier
        /// </summary>
        /// <see cref="MessageType"/>
        [DataMember(Name = "type")]
        public MessageType Type { get; set; }
        /// <summary>
        /// Date and time of request
        /// </summary>
        [DataMember(Name = "timestamp")]
        public DateTime TimeStamp { get; set; }
        /// <summary>
        /// Version of the message
        /// </summary>
        [DataMember(Name = "version")]
        public int Version { get; set; }
        /// <summary>
        /// A string descriptor of the type of the enclosed body
        /// </summary>
        [DataMember(Name = "bodytype")]
        public String BodyType { get; set; }
        /// <summary>
        /// Serialized value of the cloud/back office entities
        /// </summary>
        [DataMember(Name = "body")]
        public String Body { get; set; }
        /// <summary>
        /// Computed hash of the body content
        /// </summary>
        [DataMember(Name = "bodyhash")]
        public String BodyHash { get; set; }

        /// <summary>
        /// Encapsulates information indicating the location of blob storage used for large data uploads
        /// </summary>
        [DataMember(Name = "uploadsessioninfo")]
        public UploadSessionInfo UploadSessionInfo { get; set; }
        /// <summary>
        /// Identifier of the original message / request
        /// </summary>
        /// <remarks>
        /// Used to track the original request
        /// </remarks>
        [DataMember(Name = "correlationid")]
        public Guid CorrelationId { get; set; }
    }
}