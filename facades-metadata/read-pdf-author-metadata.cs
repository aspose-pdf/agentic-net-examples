using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!System.IO.File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Initialize the PdfFileInfo facade for the specified PDF file
        using (PdfFileInfo fileInfo = new PdfFileInfo(pdfPath))
        {
            // Read the Author metadata
            string author = fileInfo.Author;

            // Output the Author value (or indicate if it's missing)
            Console.WriteLine($"Author: {author ?? "(none)"}");
        }
    }
}