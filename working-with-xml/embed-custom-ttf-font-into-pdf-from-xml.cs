using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string xmlPath = "input.xml";
        const string outputPdf = "output.pdf";
        const string customFontPath = "custom-font.ttf"; // TrueType font to embed

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        if (!File.Exists(customFontPath))
        {
            Console.Error.WriteLine($"Font file not found: {customFontPath}");
            return;
        }

        // Load the XML content into a PDF document
        XmlLoadOptions xmlLoadOptions = new XmlLoadOptions();
        using (Document pdfDoc = new Document(xmlPath, xmlLoadOptions))
        {
            // Ensure standard Type1 fonts are also embedded if used
            pdfDoc.EmbedStandardFonts = true;

            // Open the custom TrueType font and mark it for embedding (use Stream overload)
            Font customFont;
            using (FileStream fontStream = new FileStream(customFontPath, FileMode.Open, FileAccess.Read))
            {
                customFont = FontRepository.OpenFont(fontStream, FontTypes.TTF);
            }
            customFont.IsEmbedded = true;

            // Replace the font of all existing text fragments with the custom font
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            pdfDoc.Pages.Accept(absorber);

            foreach (TextFragment fragment in absorber.TextFragments)
            {
                fragment.TextState.Font = customFont;
            }

            // Save the resulting PDF – fonts are now embedded
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF generated with embedded font: {outputPdf}");
    }
}
