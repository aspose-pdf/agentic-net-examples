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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfContentEditor facade and bind it to the document
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Replace all occurrences of "Draft" with "Final" on all pages (0 = all pages)
            editor.ReplaceText("Draft", 0, "Final");

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"All occurrences of \"Draft\" replaced with \"Final\" and saved to '{outputPath}'.");
    }
}