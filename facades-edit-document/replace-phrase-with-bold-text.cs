using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

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
        Document doc = new Document(inputPath);

        // Bind the document to the content editor
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(doc);

        // Define a TextState that only changes the font style to Bold.
        // Leaving other properties (e.g., FontSize) unset preserves the original appearance.
        TextState textState = new TextState
        {
            FontStyle = FontStyles.Bold
        };

        // Replace the phrase on every page. Aspose.Pdf page numbers are 1‑based.
        for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
        {
            editor.ReplaceText(srcPhrase, pageNumber, destPhrase, textState);
        }

        // Save the modified PDF
        doc.Save(outputPath);
        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
