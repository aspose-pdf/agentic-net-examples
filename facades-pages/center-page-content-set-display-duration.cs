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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least five pages
            if (doc.Pages.Count < 5)
            {
                Console.Error.WriteLine("The document does not contain five pages.");
                return;
            }

            // Use PdfPageEditor to set alignment and display duration for page 5
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(doc);
                // Specify the page(s) to edit – 1‑based indexing
                editor.ProcessPages = new int[] { 5 };

                // Center the page content horizontally and vertically
                editor.HorizontalAlignment = HorizontalAlignment.Center;
                // Use the non‑obsolete property for vertical alignment
                editor.VerticalAlignmentType = VerticalAlignment.Center;

                // Set the display duration (in seconds) for the selected page
                editor.DisplayDuration = 4; // integer seconds as required

                // Apply the changes and save the result
                editor.ApplyChanges();
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
