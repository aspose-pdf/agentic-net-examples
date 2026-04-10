using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the PDF file whose custom metadata will be read.
        const string inputPdf = "sample.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // PdfFileInfo implements IDisposable, so wrap it in a using block.
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPdf))
        {
            // List of custom metadata keys to retrieve.
            string[] metaKeys = { "CustomKey1", "CustomKey2", "Author" };

            foreach (string key in metaKeys)
            {
                // GetMetaInfo returns an empty string if the key does not exist.
                string value = pdfInfo.GetMetaInfo(key);

                // Gracefully handle null or empty values.
                if (string.IsNullOrEmpty(value))
                {
                    Console.WriteLine($"Metadata '{key}' is missing or empty.");
                }
                else
                {
                    Console.WriteLine($"Metadata '{key}': {value}");
                }
            }
        }
    }
}