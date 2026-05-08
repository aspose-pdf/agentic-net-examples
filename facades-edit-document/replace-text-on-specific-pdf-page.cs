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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Prepare a font and text state for the replacement (optional)
            Aspose.Pdf.Text.Font font = FontRepository.FindFont("Courier New");
            font.IsEmbedded = true;

            TextState textState = new TextState
            {
                Font = font,
                FontSize = 14,
                FontStyle = FontStyles.Bold,
                ForegroundColor = Aspose.Pdf.Color.Blue
            };

            // Bind the document to PdfContentEditor
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Replace text only on page 3 (Aspose.Pdf uses 1‑based page indexing)
            editor.ReplaceText(srcText, 3, destText, textState);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text replaced on page 3 and saved to '{outputPath}'.");
    }
}