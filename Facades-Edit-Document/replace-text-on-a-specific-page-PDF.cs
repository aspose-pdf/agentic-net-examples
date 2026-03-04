using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string srcText    = "Hello World";   // text to find
        const string destText   = "Hi Universe";   // replacement text
        const int    pageNumber = 2;               // 1‑based page index

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the content editor and bind the document
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Define the appearance of the replacement text (optional)
            TextState textState = new TextState
            {
                Font = FontRepository.FindFont("Courier New"), // embed font
                FontSize = 14,
                FontStyle = FontStyles.Bold | FontStyles.Italic,
                ForegroundColor = Color.Red
            };

            // Replace the text on the specified page using the TextState
            bool replaced = editor.ReplaceText(srcText, pageNumber, destText, textState);

            Console.WriteLine(replaced
                ? $"Replaced \"{srcText}\" with \"{destText}\" on page {pageNumber}."
                : $"Text \"{srcText}\" not found on page {pageNumber}.");

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}