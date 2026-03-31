using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (PdfPageEditor pageEditor = new PdfPageEditor())
        {
            // Load the PDF file into the editor
            pageEditor.BindPdf(inputPath);

            // Example modifications: rotate pages 90 degrees and set zoom to 80%
            pageEditor.Rotation = 90;
            pageEditor.Zoom = 0.8f;

            // Apply the changes to the document
            pageEditor.ApplyChanges();

            // Save the edited PDF
            pageEditor.Save(outputPath);
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}
