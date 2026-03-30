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
        const string sourcePhrase = "old phrase";
        const string replacementPhrase = "new phrase";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            TextState textState = new TextState();
            // Use Aspose.Pdf.Text.FontStyles enum (not System.Drawing.FontStyle)
            textState.FontStyle = FontStyles.Bold;

            // Replace on all pages (page number 0 means all pages)
            editor.ReplaceText(sourcePhrase, 0, replacementPhrase, textState);

            doc.Save(outputPath);
        }

        Console.WriteLine($"Phrase replaced and saved to '{outputPath}'.");
    }
}
