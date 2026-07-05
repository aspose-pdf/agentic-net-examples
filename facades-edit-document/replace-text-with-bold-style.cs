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
        const string phraseToReplace = "old phrase";
        const string newPhrase = "new phrase";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfContentEditor and bind the document
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Define TextState with Bold style; other properties (font, size, color) remain unchanged
            TextState textState = new TextState
            {
                // In newer Aspose.PDF versions the enum is FontStyles (plural)
                FontStyle = FontStyles.Bold
            };

            // Replace the phrase on all pages (thePage = 0) with the new text using the TextState
            editor.ReplaceText(phraseToReplace, 0, newPhrase, textState);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Phrase replaced and saved to '{outputPath}'.");
    }
}
