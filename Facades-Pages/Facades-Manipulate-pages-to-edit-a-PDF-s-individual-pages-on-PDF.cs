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

        // PdfPageEditor is a facade for page-level editing (rotate, zoom, move, resize)
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the source PDF file to the facade
            editor.BindPdf(inputPath);

            // Rotate page 2 by 90 degrees
            editor.Rotation = 90;
            editor.ProcessPages = new int[] { 2 }; // edit only page 2

            // Zoom the page content to 150%
            editor.Zoom = 1.5f;

            // Shift the page content 50 points right and 30 points up
            editor.MovePosition(50, 30);

            // Change the output page size to A4
            editor.PageSize = PageSize.A4;

            // Apply all configured changes
            editor.ApplyChanges();

            // Save the edited PDF to a new file
            editor.Save(outputPath);
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}