using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // PDF generated from TeX
        const string outputPath = "output.pdf";  // Result with page transitions

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize the page editor facade
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the loaded document to the editor
                editor.BindPdf(doc);

                // Apply a transition effect to all pages
                // Available transition constants are defined in PdfPageEditor (e.g., BLINDH, DISSOLVE, etc.)
                editor.TransitionType = PdfPageEditor.BLINDH;   // example transition
                editor.TransitionDuration = 2;                 // duration in seconds

                // Optionally, edit other page properties (rotation, zoom, etc.) here
                // editor.Rotation = 0;
                // editor.Zoom = 1.0f;

                // Apply the changes to the document
                editor.ApplyChanges();

                // Save the modified PDF
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF with transitions saved to '{outputPath}'.");
    }
}