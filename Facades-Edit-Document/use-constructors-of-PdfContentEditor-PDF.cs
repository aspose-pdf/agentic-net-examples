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

        // Verify that the source PDF exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Initialise PdfContentEditor with the loaded Document.
            // This uses the PdfContentEditor(Document) constructor.
            using (PdfContentEditor editor = new PdfContentEditor(doc))
            {
                // Example operation: replace all occurrences of "Hello" with "Hi".
                editor.ReplaceText("Hello", "Hi");

                // Save the modified PDF. Save(string) writes a PDF regardless of extension.
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}