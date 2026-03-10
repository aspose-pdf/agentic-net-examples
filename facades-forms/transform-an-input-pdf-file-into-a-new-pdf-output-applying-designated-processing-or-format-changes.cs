using System;
using System.IO;
using Aspose.Pdf.Facades; // Facade classes for PDF processing

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Use PdfPageEditor facade to rotate all pages by 90 degrees
        // PdfPageEditor implements SaveableFacade, which provides BindPdf, ApplyChanges, and Save methods
        using (PdfPageEditor pageEditor = new PdfPageEditor())
        {
            // Bind the source PDF document to the facade
            pageEditor.BindPdf(inputPdfPath);

            // Set the rotation angle (must be 0, 90, 180, or 270)
            pageEditor.Rotation = 90;

            // Apply the rotation to all pages (ProcessPages defaults to all pages)
            pageEditor.ApplyChanges();

            // Save the modified document to the output path
            pageEditor.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF has been transformed and saved to '{outputPdfPath}'.");
    }
}