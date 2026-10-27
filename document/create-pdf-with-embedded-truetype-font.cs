using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string outputPdfPath = "EmbeddedFontOutput.pdf";
        const string trueTypeFontPath = "fonts/Arial.ttf"; // Path to a TrueType (.ttf) font file

        // Verify the font file exists
        if (!File.Exists(trueTypeFontPath))
        {
            Console.Error.WriteLine($"Font file not found: {trueTypeFontPath}");
            return;
        }

        // Register the TrueType font with Aspose.Pdf's FontRepository using a substitution
        // This replaces the removed AddFont method in newer versions
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("Arial", trueTypeFontPath));

        // Retrieve the font by its family name (e.g., "Arial")
        Font font = FontRepository.FindFont("Arial");
        if (font == null)
        {
            Console.Error.WriteLine("Failed to locate the registered font.");
            return;
        }

        // Ensure the font will be embedded in the PDF
        font.IsEmbedded = true;

        // Create the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document())
        {
            // Add a page to the document
            Page page = pdfDoc.Pages.Add();

            // Create a text fragment that uses the embedded TrueType font
            TextFragment tf = new TextFragment("This text is rendered with an embedded TrueType font.");
            tf.TextState.Font = font;                     // Apply the embedded font
            tf.TextState.FontSize = 14;                   // Set desired font size
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black; // Use cross‑platform color

            // Add the text fragment to the page's paragraphs collection
            page.Paragraphs.Add(tf);

            // Save the PDF – the font will be embedded automatically because IsEmbedded = true
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF created with embedded TrueType font: {outputPdfPath}");
    }
}