using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // Use PdfPageEditor (facade) to edit page size and check rotation.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF file.
            editor.BindPdf(inputPath);

            // Retrieve rotation of the first page before any changes.
            int rotationBefore = editor.GetPageRotation(1);
            Console.WriteLine($"Rotation before resizing: {rotationBefore} degrees");

            // Change the page size (e.g., to A4). Width and height are in points (1 inch = 72 points).
            // A4 size: 595 x 842 points.
            editor.PageSize = new PageSize(595, 842);

            // Apply the size change.
            editor.ApplyChanges();

            // Retrieve rotation of the first page after resizing.
            int rotationAfter = editor.GetPageRotation(1);
            Console.WriteLine($"Rotation after resizing: {rotationAfter} degrees");

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}