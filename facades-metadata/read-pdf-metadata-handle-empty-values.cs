using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the PDF file
        const string pdfPath = "sample.pdf";

        // List of metadata keys to retrieve (custom or standard)
        string[] metaKeys = { "CustomKey1", "CustomKey2", "Author", "Title" };

        // Verify that the file exists before proceeding
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Bind the PDF using PdfFileInfo (constructor handles loading)
        using (PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath))
        {
            foreach (string key in metaKeys)
            {
                // GetMetaInfo returns an empty string if the key does not exist
                string value = pdfInfo.GetMetaInfo(key);

                // Gracefully handle null or empty values
                if (string.IsNullOrEmpty(value))
                {
                    Console.WriteLine($"{key}: <empty>");
                }
                else
                {
                    Console.WriteLine($"{key}: {value}");
                }
            }
        }
    }
}