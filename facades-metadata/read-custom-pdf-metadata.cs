using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the PdfFileInfo facade for the PDF file
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Define the custom metadata keys you want to read
            string[] metaKeys = { "CustomAuthor", "Project", "Department" };

            foreach (string key in metaKeys)
            {
                // Retrieve the metadata value; GetMetaInfo returns an empty string if the key is absent
                string value = pdfInfo.GetMetaInfo(key);

                // Gracefully handle null or empty values
                if (string.IsNullOrEmpty(value))
                {
                    Console.WriteLine($"{key}: <not set>");
                }
                else
                {
                    Console.WriteLine($"{key}: {value}");
                }
            }
        }
    }
}