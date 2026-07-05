using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";

        // Verify that the PDF file exists before trying to open it.
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Error: The file '{inputPath}' was not found. Please provide a valid PDF file.");
            return; // Exit gracefully – no PdfFileInfo is created.
        }

        PdfFileInfo pdfInfo = null;
        try
        {
            // Initialize the PdfFileInfo facade for the PDF file
            pdfInfo = new PdfFileInfo(inputPath);

            // List of metadata keys to read (custom or standard)
            string[] metaKeys = { "CustomKey1", "CustomKey2", "Author", "Title" };

            foreach (string key in metaKeys)
            {
                // GetMetaInfo returns an empty string if the key does not exist
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
        catch (Exception ex)
        {
            // Any unexpected exception is reported without crashing the program.
            Console.WriteLine($"An error occurred while processing the PDF: {ex.Message}");
        }
        finally
        {
            // Release resources held by the facade if it was created.
            pdfInfo?.Close();
        }
    }
}
