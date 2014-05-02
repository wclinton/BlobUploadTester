using System.Runtime.Serialization;

namespace BlobUploadTester.ConnectorContracts
{
    /// <summary>
    ///  Used to categorize the messages
    /// </summary>
    [DataContract(Name = "MessageType")]
    public enum MessageType
    {
        [EnumMember]
        Undefined = 0,
        [EnumMember]
        Request = 1,
        [EnumMember]
        Response = 2,
        [EnumMember]
        UploadSession = 3,
        [EnumMember]
        ConfigurationRequest = 4,
        [EnumMember]
        ConfigurationResponse = 5
    }
}