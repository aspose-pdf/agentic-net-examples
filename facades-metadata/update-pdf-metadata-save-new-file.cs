using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF file information
        PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath);

        // Update metadata fields
        pdfInfo.Title    = "Updated Document Title";
        pdfInfo.Author   = "Jane Smith";
        pdfInfo.Subject  = "Updated Subject";
        pdfInfo.Keywords = "Aspose, PDF, Metadata";

        // Persist the changes back to the PDF file (provide the output path)
        pdfInfo.SaveNewInfo(pdfPath);

        Console.WriteLine($"Metadata updated and saved to '{pdfPath}'.");
    }
}
