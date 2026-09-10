using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string xmlPath      = "input.xml";
        const string outputPdf    = "output.pdf";
        const string customFontPath = "custom-font.ttf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        if (!File.Exists(customFontPath))
        {
            Console.Error.WriteLine($"TrueType font file not found: {customFontPath}");
            return;
        }

        // Load the XML content into a PDF document
        XmlLoadOptions xmlLoadOptions = new XmlLoadOptions();
        using (Document pdfDoc = new Document(xmlPath, xmlLoadOptions))
        {
            // Ensure standard Type1 fonts are also embedded if used
            pdfDoc.EmbedStandardFonts = true;

            // Open the custom TrueType font and mark it for embedding
            Font customFont = FontRepository.OpenFont(customFontPath);
            customFont.IsEmbedded = true;

            // OPTIONAL: replace existing text with the custom font.
            // Here we simply add a new paragraph using the custom font
            // to guarantee that the font is embedded in the final PDF.
            TextFragment tf = new TextFragment("Sample text using custom TrueType font");
            tf.TextState.Font = customFont;
            tf.TextState.FontSize = 14;
            tf.TextState.ForegroundColor = Color.Black;

            // Add the fragment to the first page (or create a new page if none exist)
            if (pdfDoc.Pages.Count == 0)
                pdfDoc.Pages.Add();

            pdfDoc.Pages[1].Paragraphs.Add(tf);

            // Save the resulting PDF with the embedded font
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF generated with embedded TrueType font: {outputPdf}");
    }
}