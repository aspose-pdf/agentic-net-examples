using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // Load the PDF document to access its DocumentInfo dictionary
        using (Document doc = new Document(inputPath))
        {
            DocumentInfo info = doc.Info;
            List<string> customKeys = new List<string>();

            // Collect keys that are not predefined (i.e., custom metadata)
            foreach (string key in info.Keys)
            {
                if (!DocumentInfo.IsPredefinedKey(key))
                {
                    customKeys.Add(key);
                }
            }

            // Sort keys alphabetically
            customKeys.Sort();

            // Use PdfFileInfo to retrieve values via GetMetaInfo
            PdfFileInfo fileInfo = new PdfFileInfo(inputPath);

            Console.WriteLine("Custom PDF Metadata (alphabetical order):");
            foreach (string key in customKeys)
            {
                string value = fileInfo.GetMetaInfo(key);
                Console.WriteLine($"{key}: {value}");
            }
        }
    }
}