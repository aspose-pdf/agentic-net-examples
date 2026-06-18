using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_fade.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize PdfPageEditor with the loaded document
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Set fade (dissolve) transition for all pages
                editor.TransitionType = PdfPageEditor.DISSOLVE; // fade effect
                editor.TransitionDuration = 2; // duration in seconds

                // Apply the transition settings to the document
                editor.ApplyChanges();
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with fade transition: '{outputPath}'.");
    }
}