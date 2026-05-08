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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Initialize the content editor and bind the document
                PdfContentEditor editor = new PdfContentEditor();
                editor.BindPdf(doc);

                // Replace all occurrences of "Draft" with "Final" on all pages
                editor.ReplaceText("Draft", "Final");

                // Save the modified document using the editor (ensures all edits are persisted)
                editor.Save(outputPath);
            }

            Console.WriteLine($"All occurrences of 'Draft' replaced with 'Final'. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
