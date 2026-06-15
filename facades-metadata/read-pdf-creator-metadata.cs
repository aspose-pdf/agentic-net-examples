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

        // Open the PDF file info facade to read metadata
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Store the Creator metadata in a variable
            string creator = pdfInfo.Creator;

            // Example usage: output the value
            Console.WriteLine($"Creator: {creator}");
        }
    }
}