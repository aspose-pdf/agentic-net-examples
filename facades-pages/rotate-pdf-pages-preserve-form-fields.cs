using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize the page editor facade
            PdfPageEditor editor = new PdfPageEditor();
            editor.BindPdf(doc);

            // Rotate all pages by 90 degrees clockwise
            editor.Rotation = 90; // allowed values: 0, 90, 180, 270
            editor.ApplyChanges();

            // Save the rotated document; form fields remain unchanged
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}