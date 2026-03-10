using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "stamp.png";

        // Verify that the source PDF and image exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Add an image to the first page using PdfFileMend (Facade)
        // ------------------------------------------------------------
        using (PdfFileMend mend = new PdfFileMend())
        {
            // Bind the existing PDF document
            mend.BindPdf(inputPdf);

            // Add the image to page 1.
            // Coordinates are in points (1 inch = 72 points).
            // Here the image rectangle is defined by lower‑left (50,50) and upper‑right (200,200).
            mend.AddImage(imagePath, 1, 50f, 50f, 200f, 200f);

            // Save the modified PDF to a new file
            mend.Save(outputPdf);
        }

        // ------------------------------------------------------------
        // 2. Rotate the first page using PdfPageEditor (Facade)
        // ------------------------------------------------------------
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the PDF that was just saved
            editor.BindPdf(outputPdf);

            // Set rotation for the target page(s). 0, 90, 180, 270 are valid.
            editor.Rotation = 90; // rotate clockwise by 90 degrees

            // Apply the pending changes
            editor.ApplyChanges();

            // Overwrite the same file with the rotated page
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Image added and page rotated. Output saved to '{outputPdf}'.");
    }
}