using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PCL file and the target PDF file
        const string inputPclPath  = "input.pcl";
        const string outputPdfPath = "output.pdf";

        // Ensure the source file exists
        if (!System.IO.File.Exists(inputPclPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPclPath}");
            return;
        }

        // PdfFileInfo works with the underlying file via the Facades API.
        // It can read a PCL file (which Aspose.Pdf treats as an input format)
        // and then write the updated metadata into a PDF file.
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPclPath))
        {
            // Set the desired document information
            pdfInfo.Title    = "Sample Document Title";
            pdfInfo.Author   = "John Doe";
            pdfInfo.Subject  = "Demonstration of PDF metadata via Facades";
            pdfInfo.Keywords = "Aspose.Pdf, Facades, Metadata, PCL";

            // Save the updated information into a new PDF file.
            // SaveNewInfo creates a PDF with the supplied metadata.
            pdfInfo.SaveNewInfo(outputPdfPath);
        }

        Console.WriteLine($"Metadata applied and PDF saved to '{outputPdfPath}'.");
    }
}