using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF, text to find, replacement text, and output PDF paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string searchText = "PLACEHOLDER";
        const string replaceText = "ACTUAL VALUE";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfContentEditor and bind it to the loaded document
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Replace text on all pages (thePage = 0 means all pages)
            // This overload preserves the original layout.
            editor.ReplaceText(searchText, 0, replaceText);

            // Save the modified document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text replacement completed. Output saved to '{outputPath}'.");
    }
}