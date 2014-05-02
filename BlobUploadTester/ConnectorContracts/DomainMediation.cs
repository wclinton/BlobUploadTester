using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace BlobUploadTester.ConnectorContracts
{
    /// <summary>
    /// Domain mediation entry 
    /// </summary>
    public class DomainMediation
    {
        // ReSharper disable once InconsistentNaming
        private ICollection<String> payloadBlobIds { get; set; }


        public DomainMediation()
        {
            payloadBlobIds = new Collection<string>();
        }

        /// <summary>
        /// 
        /// </summary>
        public Guid UniqueIdentifier { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DomainFeatureRequest { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Payload { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PayloadType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<String> PayloadBlobIds
        {
            get { return payloadBlobIds ?? (payloadBlobIds = new Collection<string>()); }
        }



        #region IExtensibleDataObject implementation

        /// <summary>
        /// To support forward-compatible data contracts
        /// </summary>
        public ExtensionDataObject ExtensionData { get; set; }

        #endregion
    }
}
