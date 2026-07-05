using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Verify the PDF file exists before attempting to read metadata
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfFileInfo provides access to document metadata; it implements IDisposable
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Retrieve the Creator property and store it in a variable
            string creator = pdfInfo.Creator;

            // Example usage: display the value
            Console.WriteLine($"Creator: {creator}");
        }
    }
}