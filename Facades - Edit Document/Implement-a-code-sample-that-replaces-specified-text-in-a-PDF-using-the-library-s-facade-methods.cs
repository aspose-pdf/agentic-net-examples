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
        const string srcText    = "hello world";
        const string destText   = "hi world";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document – wrapped in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfContentEditor and bind the loaded document
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(doc);

                // Prepare TextState for the replacement text (font, size, style, color)
                Font font = FontRepository.FindFont("Courier New");
                font.IsEmbedded = true;

                TextState textState = new TextState
                {
                    Font = font,
                    FontSize = 17,
                    FontStyle = FontStyles.Bold | FontStyles.Italic,
                    ForegroundColor = Aspose.Pdf.Color.Red
                };

                // Replace the text on all pages (page number 0 means all pages)
                editor.ReplaceText(srcText, 0, destText, textState);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text replacement completed. Output saved to '{outputPath}'.");
    }
}
