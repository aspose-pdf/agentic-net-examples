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
        const string srcText = "Hello World";
        const string destText = "Hi Universe";
        const int pageNumber = 1; // 1‑based page index

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the content editor and bind the document
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Prepare a TextState to keep formatting (font, size, style, color)
            Font font = FontRepository.FindFont("Arial");
            font.IsEmbedded = true; // ensure the font is embedded in the output PDF

            TextState textState = new TextState
            {
                Font = font,
                FontSize = 12,
                FontStyle = FontStyles.Bold,
                ForegroundColor = Aspose.Pdf.Color.Blue
            };

            // Replace the specified text on the given page
            bool replaced = editor.ReplaceText(srcText, pageNumber, destText, textState);
            Console.WriteLine(replaced ? "Text replaced successfully." : "Source text not found on the page.");

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}