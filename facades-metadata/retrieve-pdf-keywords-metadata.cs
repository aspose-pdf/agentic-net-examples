using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "sample.pdf";

        // Verify that the PDF file exists before creating PdfFileInfo
        if (!File.Exists(inputPdf))
        {
            Console.WriteLine($"Error: File '{inputPdf}' not found.");
            return;
        }

        // Initialize PdfFileInfo with a valid file path
        using (PdfFileInfo fileInfo = new PdfFileInfo(inputPdf))
        {
            // Retrieve the Keywords metadata (empty string if not set)
            string keywords = fileInfo.Keywords ?? string.Empty;
            Console.WriteLine($"Keywords: {keywords}");
        }
    }
}