using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for input TeX file and output PDF
        const string texInputPath  = "input.tex";
        const string pdfOutputPath = "output.pdf";

        // Verify input file exists
        if (!File.Exists(texInputPath))
        {
            Console.Error.WriteLine($"TeX source not found: {texInputPath}");
            return;
        }

        // Load the TeX file into a PDF Document using TeXLoadOptions
        using (Document pdfDoc = new Document(texInputPath, new TeXLoadOptions()))
        {
            // Initialize PdfPageEditor facade with the loaded document
            using (PdfPageEditor pageEditor = new PdfPageEditor(pdfDoc))
            {
                // Apply a transition effect to all pages
                // Available transition constants are defined in PdfPageEditor (e.g., BLINDH, DISSOLVE, etc.)
                pageEditor.TransitionType = PdfPageEditor.BLINDH; // vertical blinds transition
                pageEditor.TransitionDuration = 2;                // duration in seconds

                // Optionally, edit other page properties (rotation, zoom, etc.)
                // pageEditor.Rotation = 0;
                // pageEditor.Zoom = 1.0f;

                // Apply the changes to the document
                pageEditor.ApplyChanges();
            }

            // Save the modified document as PDF
            pdfDoc.Save(pdfOutputPath);
        }

        Console.WriteLine($"PDF with transition effects saved to '{pdfOutputPath}'.");
    }
}