using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create a TextState with the desired font properties
            TextState textState = new TextState
            {
                // Find the font by name (must be installed on the system or embedded)
                Font = FontRepository.FindFont("Arial"),
                FontSize = 14,
                FontStyle = FontStyles.Bold | FontStyles.Italic,
                // Use Aspose.Pdf.Color for cross‑platform compatibility
                ForegroundColor = Aspose.Pdf.Color.FromRgb(0.0, 0.0, 1.0) // blue
            };

            // Initialize the PdfContentEditor facade and bind the document
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Example: replace a specific string with the same text but new font properties.
            // Use page number 0 to apply to all pages.
            string sourceText = "Sample Text";   // text to be replaced
            string destText   = "Sample Text";   // same text, only font changes

            // Perform the replacement with the defined TextState
            editor.ReplaceText(sourceText, 0, destText, textState);

            // Save the modified document
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Document saved with updated font properties to '{outputPdf}'.");
    }
}