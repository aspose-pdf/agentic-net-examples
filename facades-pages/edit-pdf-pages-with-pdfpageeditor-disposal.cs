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

        // Load the PDF document (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Create and bind PdfPageEditor (facade)
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(doc);

                // Example modifications
                editor.Rotation = 90;          // rotate pages 90 degrees
                editor.Zoom = 0.8f;            // set zoom to 80%

                // Apply the changes to the bound document
                editor.ApplyChanges();

                // Save the edited PDF (save rule)
                editor.Save(outputPath);
            } // editor.Dispose() automatically calls Close() to release handles
        } // doc.Dispose() releases the document resources

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}