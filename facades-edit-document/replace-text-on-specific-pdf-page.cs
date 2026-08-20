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
        const string srcText    = "TextToReplace";   // text to find
        const string destText   = "NewText";         // replacement text

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfContentEditor facade and bind the document
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Replace text only on page 3 (Aspose.Pdf uses 1‑based page indexing)
            editor.ReplaceText(srcText, 3, destText);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Replacement completed. Output saved to '{outputPath}'.");
    }
}