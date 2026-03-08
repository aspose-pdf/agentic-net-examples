using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class ReplaceTextExample
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Output PDF file path
        const string outputPath = "output.pdf";

        // Text to find and its replacement
        const string sourceText = "hello world";
        const string replacementText = "hi world";

        // Page number to apply replacement:
        // 0 means all pages, otherwise use 1‑based page index
        const int pageNumber = 0;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Create a PdfContentEditor facade and bind it to the loaded document
                PdfContentEditor editor = new PdfContentEditor();
                editor.BindPdf(doc);

                // Prepare a TextState to define appearance of the replacement text
                // Find a font (e.g., Arial) and embed it into the PDF
                Font font = FontRepository.FindFont("Arial");
                font.IsEmbedded = true;

                TextState textState = new TextState
                {
                    Font = font,
                    FontSize = 12,
                    FontStyle = FontStyles.Bold,
                    ForegroundColor = Color.Blue // Aspose.Pdf.Color, cross‑platform
                };

                // Perform the replacement.
                // Overload used: ReplaceText(string src, int page, string dest, TextState state)
                bool replaced = editor.ReplaceText(sourceText, pageNumber, replacementText, textState);

                Console.WriteLine(replaced
                    ? $"Text \"{sourceText}\" was replaced with \"{replacementText}\"."
                    : $"Text \"{sourceText}\" not found.");

                // Save the modified document. No SaveOptions needed for PDF output.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
