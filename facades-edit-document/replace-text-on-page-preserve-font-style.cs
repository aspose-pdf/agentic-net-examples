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
        const string srcString  = "TextToReplace";   // text to find
        const string destString = "NewText";         // replacement text

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using the standard lifecycle rule with using)
        using (Document doc = new Document(inputPath))
        {
            // Initialize the Facade editor and bind the loaded document
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Replace the text on page 2.
            // Using the overload without a TextState preserves the original font style,
            // color, size, etc., of the replaced text.
            editor.ReplaceText(srcString, 2, destString);

            // Save the modified document (PDF format, no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Replacement complete. Saved to '{outputPath}'.");
    }
}