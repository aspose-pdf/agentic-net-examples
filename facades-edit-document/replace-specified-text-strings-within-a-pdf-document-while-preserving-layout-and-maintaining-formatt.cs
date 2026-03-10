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

        // Define the text pairs to replace: source text and its replacement.
        var replacements = new (string src, string dest)[]
        {
            ("Hello", "Hi"),
            ("World", "Earth")
        };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfContentEditor facade and bind it to the loaded document.
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Replace each source string with its destination across all pages (page number 0 = all pages).
            foreach (var pair in replacements)
            {
                editor.ReplaceText(pair.src, 0, pair.dest);
            }

            // Save the modified document. The layout and original formatting are preserved.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text replacement completed. Output saved to '{outputPath}'.");
    }
}