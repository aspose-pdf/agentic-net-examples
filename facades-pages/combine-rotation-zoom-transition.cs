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

        // Load the source PDF
        using (Document document = new Document(inputPath))
        {
            // Initialize the page editor facade
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the loaded document to the editor
                editor.BindPdf(document);

                // Set rotation (90 degrees) and zoom (150%) for all pages
                editor.Rotation = 90;
                editor.Zoom = 1.5f;

                // Configure a transition effect for presentation mode
                editor.TransitionType = PdfPageEditor.DGLITTER; // Diagonal glitter transition
                editor.TransitionDuration = 2; // seconds

                // Apply the changes to the document
                editor.ApplyChanges();

                // Save the modified PDF
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Presentation PDF saved to '{outputPath}'.");
    }
}
