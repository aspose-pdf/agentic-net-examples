using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths to the source XML, the custom TrueType font, and the output PDF.
        const string xmlPath   = "input.xml";
        const string fontPath  = "custom.ttf";
        const string pdfPath   = "output.pdf";

        // Verify that the required files exist.
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }
        if (!File.Exists(fontPath))
        {
            Console.Error.WriteLine($"TrueType font file not found: {fontPath}");
            return;
        }

        // Load the XML content into a PDF document using XmlLoadOptions.
        Aspose.Pdf.XmlLoadOptions xmlLoadOptions = new Aspose.Pdf.XmlLoadOptions();

        using (Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(xmlPath, xmlLoadOptions))
        {
            // Locate the custom TrueType font and mark it for embedding.
            Aspose.Pdf.Text.Font customFont = Aspose.Pdf.Text.FontRepository.FindFont(fontPath);
            customFont.IsEmbedded = true;               // Ensure the font is embedded.
            pdfDocument.EmbedStandardFonts = true;       // Required for embedding standard Type1 fonts as well.

            // Optionally, set the custom font as the default for any missing fonts.
            // This helps when the XML does not explicitly reference the font.
            Aspose.Pdf.PdfSaveOptions saveOptions = new Aspose.Pdf.PdfSaveOptions
            {
                DefaultFontName = customFont.FontName   // Use the font's internal name.
            };

            // Save the resulting PDF with the embedded font.
            pdfDocument.Save(pdfPath, saveOptions);
        }

        Console.WriteLine($"PDF generated with embedded TrueType font: {pdfPath}");
    }
}