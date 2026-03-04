using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the content editor facade
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Replace all occurrences of the source string with the destination string
            bool replaced = editor.ReplaceText("hello world", "hi world");

            Console.WriteLine(replaced ? "Text replaced." : "Text not found.");

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved modified PDF to '{outputPath}'.");
    }
}