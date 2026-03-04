using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF files
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Page number (1‑based) and stamp index (1‑based) to be moved
        const int    pageNumber = 1;
        const int    stampIndex = 1;

        // New coordinates for the stamp (origin is bottom‑left)
        const double newX = 150.0;
        const double newY = 400.0;

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfContentEditor (a SaveableFacade) to edit the PDF
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPath);

            // Change the position of the specified stamp
            editor.MoveStamp(pageNumber, stampIndex, newX, newY);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Stamp position updated and saved to '{outputPath}'.");
    }
}