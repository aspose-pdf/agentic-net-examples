using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF files
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Page number (1‑based) and stamp index (1‑based) to be moved
        const int pageNumber = 1;
        const int stampIndex = 1;

        // New coordinates for the stamp (in points)
        const double newX = 100.0; // horizontal position
        const double newY = 200.0; // vertical position

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF, move the stamp, and save the result
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);                         // Load the PDF
            editor.MoveStamp(pageNumber, stampIndex, newX, newY); // Reposition the stamp
            editor.Save(outputPath);                           // Persist changes
        }

        Console.WriteLine($"Stamp repositioned and saved to '{outputPath}'.");
    }
}