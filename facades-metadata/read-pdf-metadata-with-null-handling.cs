using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // PdfFileInfo provides access to PDF metadata via GetMetaInfo.
            PdfFileInfo info = new PdfFileInfo(pdfPath);

            // List of common metadata keys we want to read.
            string[] metaKeys = { "Title", "Author", "Subject", "Keywords", "Creator", "Producer" };

            foreach (string key in metaKeys)
            {
                // Retrieve the metadata value; GetMetaInfo may return null.
                string value = info.GetMetaInfo(key);

                // Gracefully handle null or empty values.
                if (string.IsNullOrEmpty(value))
                {
                    value = "(not set)";
                }

                Console.WriteLine($"{key}: {value}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading metadata: {ex.Message}");
        }
    }
}