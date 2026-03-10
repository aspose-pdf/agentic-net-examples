using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF files
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define the text replacements (source => destination)
        var replacements = new Dictionary<string, string>(StringComparer.Ordinal)
        {
            { "Hello World", "Hi Universe" },
            { "Sample Text", "Replaced Text" }
            // Add more pairs as needed
        };

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Initialize the PdfContentEditor facade and bind it to the document
                PdfContentEditor editor = new PdfContentEditor();
                editor.BindPdf(doc);

                // Perform each replacement on all pages (page index 0 means "all pages")
                foreach (var pair in replacements)
                {
                    // ReplaceText(srcString, thePage, destString)
                    // thePage = 0 → replace on every page of the document
                    editor.ReplaceText(pair.Key, 0, pair.Value);
                }

                // Save the modified PDF (no SaveOptions needed for PDF output)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Text replacement completed. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}