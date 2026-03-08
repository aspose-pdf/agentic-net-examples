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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Prepare a TextState to keep formatting (font, size, color) for the replacement text
            Font replaceFont = FontRepository.FindFont("Arial");
            replaceFont.IsEmbedded = true;

            TextState textState = new TextState
            {
                Font = replaceFont,
                FontSize = 12,
                FontStyle = FontStyles.Regular, // Fixed: use FontStyles enum
                ForegroundColor = Color.Black
            };

            // Initialize the PdfContentEditor facade and bind it to the loaded document
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Replace text on all pages (thePage = 0) preserving layout via TextState
            editor.ReplaceText("Hello World", 0, "Hi Universe", textState);

            // Replace text on a specific page (page numbers are 1‑based)
            editor.ReplaceText("Sample", 2, "Example", textState);

            // Save the modified PDF; Save() without options writes PDF regardless of extension
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text replacement completed. Output saved to '{outputPath}'.");
    }
}
