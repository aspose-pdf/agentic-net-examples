using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // TextState, FontStyles

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string srcPhrase = "old phrase";
        const string destPhrase = "new phrase";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Bind the document to PdfContentEditor
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Define TextState with Bold style; other properties (font, size) stay unchanged
            TextState textState = new TextState
            {
                FontStyle = FontStyles.Bold
            };

            // Replace the phrase on all pages (page number 0 means all pages)
            editor.ReplaceText(srcPhrase, 0, destPhrase, textState);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}