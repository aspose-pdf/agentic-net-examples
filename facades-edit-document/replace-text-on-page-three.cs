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
        const string outputPath = "output_page3.pdf";
        const string srcText    = "TextToReplace";   // text to find on page 3
        const string destText   = "NewReplacement"; // replacement text

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: wrap in using)
            using (Document doc = new Document(inputPath))
            {
                // Create and bind the PdfContentEditor (Facades API)
                PdfContentEditor editor = new PdfContentEditor();
                editor.BindPdf(doc);

                // Replace text only on page 3 (pages are 1‑based)
                // Simple overload without TextState:
                editor.ReplaceText(srcText, 3, destText);

                // If you need to specify font, size, or color, use the overload with TextState:
                // TextState ts = new TextState
                // {
                //     Font = FontRepository.FindFont("Arial"),
                //     FontSize = 12,
                //     ForegroundColor = Aspose.Pdf.Color.Red
                // };
                // editor.ReplaceText(srcText, 3, destText, ts);

                // Save the modified document (lifecycle rule: use doc.Save)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Text replacement on page 3 completed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}