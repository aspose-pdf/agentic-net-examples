using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfPageEditor facade
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the document to the editor
                editor.BindPdf(doc);

                // Set the display duration (in seconds) for each slide/page
                editor.DisplayDuration = 5; // e.g., 5 seconds per page

                // Optional: set a transition effect and its duration
                editor.TransitionType = PdfPageEditor.BLINDV; // example transition style
                editor.TransitionDuration = 2;                // 2 seconds transition

                // Apply the changes to the document
                editor.ApplyChanges();

                // Save the modified PDF
                doc.Save(outputPath);
            }
        }

        Console.WriteLine($"Presentation PDF saved to '{outputPath}'.");
    }
}