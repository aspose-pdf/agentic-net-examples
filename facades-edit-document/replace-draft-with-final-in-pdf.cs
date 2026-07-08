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
            // Initialize the PdfContentEditor facade and bind it to the document
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Replace all occurrences of "Draft" with "Final" throughout the document
            editor.ReplaceText("Draft", "Final");

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text replacement completed. Saved to '{outputPath}'.");
    }
}