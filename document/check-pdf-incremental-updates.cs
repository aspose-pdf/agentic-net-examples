using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // The UpdateInfo property is not part of the public API in recent versions.
            // Use reflection to access it if it exists (it may be present in some builds).
            bool hasIncrementalUpdates = false;
            int updateCount = 0;

            PropertyInfo updateInfoProp = typeof(Document).GetProperty("UpdateInfo", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (updateInfoProp != null)
            {
                object updateInfo = updateInfoProp.GetValue(doc);
                if (updateInfo != null)
                {
                    PropertyInfo isIncProp = updateInfo.GetType().GetProperty("IsIncrementalUpdate", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    if (isIncProp != null && isIncProp.PropertyType == typeof(bool))
                    {
                        hasIncrementalUpdates = (bool)isIncProp.GetValue(updateInfo);
                    }

                    PropertyInfo countProp = updateInfo.GetType().GetProperty("UpdateCount", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    if (countProp != null && countProp.PropertyType == typeof(int))
                    {
                        updateCount = (int)countProp.GetValue(updateInfo);
                    }
                }
            }

            if (hasIncrementalUpdates || updateCount > 0)
            {
                Console.WriteLine("The PDF has been modified after its initial creation (incremental updates detected).");
                Console.WriteLine($"Number of incremental updates: {updateCount}");
            }
            else
            {
                Console.WriteLine("The PDF appears to have no incremental updates; it likely has not been modified after creation.");
            }
        }
    }
}
