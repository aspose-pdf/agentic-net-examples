using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the input file exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor to modify page 3: rotate 90° and change size to Letter
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the source PDF
            editor.BindPdf(inputPath);

            // Edit only page 3
            editor.ProcessPages = new int[] { 3 };

            // Set rotation (cast enum to int because the property expects an int)
            editor.Rotation = (int)Rotation.on90;

            // Change page size to Letter (8.5" x 11" = 612 x 792 points)
            editor.PageSize = new PageSize(612, 792);

            // Apply the changes and save the result
            editor.ApplyChanges();
            editor.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
