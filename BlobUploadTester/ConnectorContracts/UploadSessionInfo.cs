using System;
using System.Runtime.Serialization;

namespace BlobUploadTester.ConnectorContracts
{
    [DataContract(Name = "UploadSessionInfoContract")]
    public class UploadSessionInfo 
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of UploadSessionInfo with all values specified
        /// </summary>
        /// <param name="sessionKey"></param>
        /// <param name="destinationName"></param>
        /// <param name="containerUri"></param>
        /// <param name="directoryPath"></param>
        /// <param name="chunkSizeInBytes"></param>
        public UploadSessionInfo(
            string sessionKey,
            string destinationName,
            Uri containerUri,
            string directoryPath,
            Int32 chunkSizeInBytes)
        {
            SessionKey = sessionKey;
            DestinationName = destinationName;
            ContainerUri = containerUri;
            DirectoryPath = directoryPath;
            ChunkSizeInBytes = chunkSizeInBytes;
        }
     

        #endregion


        #region Properties

        /// <summary>
        /// The key used to identify an upload session
        /// </summary>
        [DataMember(Name = "SessionKey", IsRequired = true, Order = 0)]
        public string SessionKey { get; protected set; }

        /// <summary>
        /// The name of the destination for this upload
        /// This is how both the client and service identify the upload
        /// </summary>
        [DataMember(Name = "DestinationName", IsRequired = true, Order = 1)]
        public string DestinationName { get; protected set; }

        /// <summary>
        /// The Uri of the container for the tenant
        /// Which is where this new blob will be going
        /// </summary>
        [DataMember(Name = "ContainerUri", IsRequired = true, Order = 2)]
        public Uri ContainerUri { get; protected set; }

        /// <summary>
        /// The directory path, if any, for the resulting blob
        /// </summary>
        [DataMember(Name = "DirectoryPath", IsRequired = true, Order = 3)]
        public string DirectoryPath { get; protected set; }

        /// <summary>
        /// The size in bytes that uploads should be 'chunked'
        /// </summary>
        [DataMember(Name = "ChunkSizeInBytes", IsRequired = true, Order = 4)]
        public Int32 ChunkSizeInBytes { get; protected set; }


        #endregion


      
    }
}
