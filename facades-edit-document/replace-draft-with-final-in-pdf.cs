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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfContentEditor facade to edit the document content
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                // Bind the loaded document to the editor
                editor.BindPdf(doc);

                // Replace all occurrences of "Draft" with "Final" on all pages
                editor.ReplaceText("Draft", "Final");
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"All occurrences of \"Draft\" have been replaced with \"Final\" and saved to '{outputPath}'.");
    }
}