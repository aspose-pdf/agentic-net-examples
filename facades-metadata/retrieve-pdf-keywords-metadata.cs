using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "sample.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Initialize the PdfFileInfo facade with the PDF file.
        using (PdfFileInfo fileInfo = new PdfFileInfo(pdfPath))
        {
            // Retrieve the Keywords metadata.
            string keywords = fileInfo.Keywords;

            // Display the retrieved value.
            Console.WriteLine($"Keywords: {keywords}");
        }
    }
}