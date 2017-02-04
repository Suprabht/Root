//-----------------------------------------------------------------------
// <copyright file="CompressionType.cs" company="StoryTeller.">
//     Copyright (c) StoryTeller 2016. All rights reserved.
// </copyright>
// <author>Suprabhat Paul</author>
// <createddate>6/15/2016 7:22:55 PM</createddate>
// <changelog>
//  username        date        reason
//  --------        ----        ------
//                 
// </changelog>
//-----------------------------------------------------------------------

namespace SystemFrameWork.Compression
{
    public enum CompressionType
    {
        /// <summary>
        /// The zip
        /// </summary>
        Zip = 0,

        /// <summary>
        /// The g zip
        /// </summary>
        GZip = 1,


        /// <summary>
        /// The zip archive
        /// </summary>
        ZipArchive = 2
    }
}
