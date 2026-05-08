using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string xmlPath      = "input.xml";          // XML source
        const string fontPath     = "MyCustomFont.ttf";   // TrueType font file
        const string outputPdf    = "output.pdf";

        // Verify input files exist
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }
        if (!File.Exists(fontPath))
        {
            Console.Error.WriteLine($"Font file not found: {fontPath}");
            return;
        }

        // Load the XML into a PDF document using XmlLoadOptions.
        // Disable font‑license verification so the custom font can be embedded
        // even if its license would normally forbid embedding.
        XmlLoadOptions xmlLoadOptions = new XmlLoadOptions
        {
            DisableFontLicenseVerifications = true
        };

        // Open the XML file as a stream (the overload Document(Stream, LoadOptions) is
        // guaranteed to exist and avoids any ambiguity with string overloads).
        using (FileStream xmlStream = File.OpenRead(xmlPath))
        // Create the PDF document from the XML.
        using (Document pdfDoc = new Document(xmlStream, xmlLoadOptions))
        {
            // Load the custom TrueType font from file.
            // FontRepository.OpenFont returns a Font object that can be marked as embedded.
            using (FileStream fontStream = File.OpenRead(fontPath))
            {
                Font customFont = FontRepository.OpenFont(fontStream, FontTypes.TTF);
                customFont.IsEmbedded = true;               // Ensure the font is embedded.
                customFont.IsSubset   = true;               // Subset the font to reduce size (optional).

                // Add a new page to the document (if the XML did not already create pages).
                Page page = pdfDoc.Pages.Add();

                // Create a text fragment that uses the custom font.
                TextFragment fragment = new TextFragment("Sample text using a custom TrueType font");
                fragment.TextState.Font      = customFont;
                fragment.TextState.FontSize  = 14;
                fragment.TextState.ForegroundColor = Color.Black;

                // Add the fragment to the page.
                page.Paragraphs.Add(fragment);
            }

            // Optional: embed standard Type1 fonts as well (may improve compatibility).
            pdfDoc.EmbedStandardFonts = true;

            // Save the resulting PDF. The custom font will be embedded in the output file.
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF generated with embedded TrueType font: {outputPdf}");
    }
}