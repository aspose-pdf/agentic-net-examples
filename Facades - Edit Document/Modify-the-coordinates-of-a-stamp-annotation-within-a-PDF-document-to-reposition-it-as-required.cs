using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF containing the stamp annotation
        const string inputPath = "input.pdf";
        // Output PDF with the stamp repositioned
        const string outputPath = "output.pdf";

        // Page number (1‑based) where the stamp resides
        const int pageNumber = 1;
        // Index of the stamp on the page (1‑based)
        const int stampIndex = 1;

        // New coordinates for the stamp (lower‑left corner of the stamp)
        double newX = 150.0; // horizontal position
        double newY = 300.0; // vertical position

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create the content editor and bind the PDF
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Reposition the specified stamp
        editor.MoveStamp(pageNumber, stampIndex, newX, newY);

        // Save the modified document
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Stamp moved and saved to '{outputPath}'.");
    }
}