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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the facade that can edit PDF content
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Replace the target string on all pages (page number 0 means "all pages")
            // This overload preserves the original formatting and layout.
            bool replaced = editor.ReplaceText("OldString", 0, "NewString");

            Console.WriteLine(replaced ? "Text replacement succeeded." : "Target text not found.");

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}