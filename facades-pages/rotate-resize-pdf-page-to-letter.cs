using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to the PdfPageEditor facade
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);

            // Edit only page 3
            editor.ProcessPages = new int[] { 3 };

            // Rotate the selected page by 90 degrees (cast enum to int because the property expects an int)
            editor.Rotation = (int)Rotation.on90;

            // Change the page size to Letter (8.5" x 11" = 612 x 792 points)
            editor.PageSize = new PageSize(612, 792);

            // Apply the changes and save the result
            editor.ApplyChanges();
            editor.Save(outputPath);
            // No explicit Close needed – the using statement disposes the editor
        }

        Console.WriteLine($"Page 3 rotated and resized to Letter saved as '{outputPath}'.");
    }
}
