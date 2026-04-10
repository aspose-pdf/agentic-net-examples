using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_landscape.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfPageEditor facade bound to the loaded document
            PdfPageEditor editor = new PdfPageEditor(doc);

            // Define custom A4 landscape size (width > height) in points.
            // A4 size in points: 595 (width) x 842 (height) for portrait.
            // For landscape, swap the dimensions.
            Aspose.Pdf.PageSize landscapeA4 = new Aspose.Pdf.PageSize(842, 595);
            // Alternatively, you could use PageSize.A4 and set IsLandscape = true:
            // Aspose.Pdf.PageSize landscapeA4 = Aspose.Pdf.PageSize.A4;
            // landscapeA4.IsLandscape = true;

            // Apply the custom page size to all pages
            editor.PageSize = landscapeA4;

            // Commit the changes to the document
            editor.ApplyChanges();

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with A4 landscape page size: {outputPath}");
    }
}