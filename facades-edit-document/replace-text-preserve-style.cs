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
        const string srcText    = "Hello World";
        const string destText   = "Hi Universe";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfContentEditor and bind the loaded document
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Replace text on all pages (page number 0) while preserving original style
            editor.ReplaceText(srcText, 0, destText);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text replacement completed. Saved to '{outputPath}'.");
    }
}