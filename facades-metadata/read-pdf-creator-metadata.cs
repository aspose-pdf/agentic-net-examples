using System;
using System.IO;
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

        // Initialize PdfFileInfo facade for the PDF file
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Retrieve the Creator metadata
            string creator = pdfInfo.Creator;

            // Store or use the creator value as needed
            Console.WriteLine($"Creator: {creator}");
        }
    }
}