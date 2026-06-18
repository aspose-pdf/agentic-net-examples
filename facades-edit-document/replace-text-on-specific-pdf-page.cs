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
        const string srcText    = "old text";   // text to find
        const string destText   = "new text";   // replacement text

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
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

                // Replace text only on page 3 (Aspose.Pdf uses 1‑based page indexing)
                editor.ReplaceText(srcText, 3, destText);

                // Save the modified document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Text on page 3 replaced and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}