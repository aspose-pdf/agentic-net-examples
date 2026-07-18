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
        const string customFontPath = "MyCustomFont.ttf";

        // Verify files exist
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

        // Load the XML into a PDF document using XmlLoadOptions
        XmlLoadOptions xmlLoadOptions = new XmlLoadOptions();
        using (Document pdfDoc = new Document(xmlPath, xmlLoadOptions))
        {
            // Open the custom TrueType font and mark it for embedding
            Font customFont = FontRepository.OpenFont(customFontPath);
            customFont.IsEmbedded = true;               // ensure embedding
            // (Optional) embed standard Type1 fonts if they are used
            pdfDoc.EmbedStandardFonts = true;

            // Replace all existing text fonts with the custom font
            // TextFragmentAbsorber without a search pattern captures all text fragments
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            pdfDoc.Pages.Accept(absorber);

            foreach (TextFragment fragment in absorber.TextFragments)
            {
                fragment.TextState.Font = customFont;
            }

            // Save the resulting PDF – the custom TrueType font will be embedded
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF generated with embedded font: {outputPdf}");
    }
}