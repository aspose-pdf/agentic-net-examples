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
        const string srcText    = "PDF";          // text to find
        const string destText   = "Aspose.Pdf";   // replacement text

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (disposal handled by using)
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfContentEditor and bind it to the loaded document
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Replace all occurrences of srcText on all pages (page index 0 = all pages)
            editor.ReplaceText(srcText, 0, destText);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"All occurrences of \"{srcText}\" replaced with \"{destText}\" and saved to '{outputPath}'.");
    }
}