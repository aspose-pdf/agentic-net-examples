using System;
using System.IO;
using Aspose.Pdf;               // Document, PageSize
using Aspose.Pdf.Facades;      // PdfPageEditor

class Program
{
    static void Main()
    {
        const string inputPath  = "sample.pdf";
        const string outputPath = "sample_resized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF with PdfPageEditor (facade API)
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);

            // Retrieve rotation of the first page before any changes
            int rotationBefore = editor.GetPageRotation(1);
            Console.WriteLine($"Rotation before resize: {rotationBefore} degrees");

            // Change the page size (e.g., to A5). Width and height are in points (1 inch = 72 points)
            // A5 size: 420 x 595 points (approx 5.83 x 8.27 inches)
            editor.PageSize = new PageSize(420, 595);

            // Apply the size change to the document
            editor.ApplyChanges();

            // Retrieve rotation of the same page after resizing
            int rotationAfter = editor.GetPageRotation(1);
            Console.WriteLine($"Rotation after resize: {rotationAfter} degrees");

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}
