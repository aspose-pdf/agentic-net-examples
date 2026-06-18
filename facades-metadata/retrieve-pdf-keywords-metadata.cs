using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Verify the PDF file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the PdfFileInfo facade with the PDF file
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Retrieve the Keywords metadata
            string keywords = pdfInfo.Keywords;

            // Display the retrieved value
            if (!string.IsNullOrEmpty(keywords))
                Console.WriteLine($"Keywords: {keywords}");
            else
                Console.WriteLine("Keywords metadata is not set.");
        }
    }
}