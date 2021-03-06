﻿using System;
using System.IO;
using System.Web;

namespace Meerkat.Mailer
{   
    public static class FileExtensions
    {
        /// <summary>
        /// Handles web relative file names
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string MapPath(this string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                if (fileName.StartsWith("~"))
                {
                    // TODO: Convert this to use a static class property and configure at startup.
#if net452
                    if (string.IsNullOrEmpty(HttpRuntime.AppDomainAppPath))
                    {
                        throw new NotSupportedException("Cannot map web paths without a HttpRuntime.AppDomainAppPath");
                    }

                    // Strip the ~ before combining
                    var path = Path.Combine(HttpRuntime.AppDomainAppPath, fileName.Substring(1));
                    return path;
#else
                    throw new NotSupportedException("Cannot map web paths in .NET Core");
#endif
                }

                if (fileName.StartsWith("."))
                {
                    // Convert to a full path
                    return Path.GetFullPath(fileName);
                }
            }

            // Return the one we are given
            return fileName;
        }
    }
}