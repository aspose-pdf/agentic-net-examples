using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output PDF files
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Phrase to replace and its replacement
        const string sourcePhrase = "old phrase";
        const string newPhrase    = "new phrase";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfContentEditor and bind the document
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Prepare TextState: set bold style, keep other attributes unchanged
            TextState textState = new TextState
            {
                // Apply bold style; other properties (font, size, color) remain as default
                FontStyle = FontStyles.Bold
            };

            // Replace the phrase on all pages (thePage = 0) using the TextState
            editor.ReplaceText(sourcePhrase, 0, newPhrase, textState);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Phrase replaced and saved to '{outputPath}'.");
    }
}