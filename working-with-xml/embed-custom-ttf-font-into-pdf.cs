using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string xmlPath        = "input.xml";
        const string customFontPath = "MyCustomFont.ttf";
        const string outputPdfPath  = "output.pdf";

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

        // Load XML into a PDF document
        Aspose.Pdf.XmlLoadOptions xmlLoadOptions = new Aspose.Pdf.XmlLoadOptions();

        using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(xmlPath, xmlLoadOptions))
        {
            // Open the custom TrueType font and mark it for embedding
            Aspose.Pdf.Text.Font customFont = Aspose.Pdf.Text.FontRepository.OpenFont(customFontPath);
            customFont.IsEmbedded = true;

            // Ensure standard Type1 fonts are also embedded if they appear
            pdfDoc.EmbedStandardFonts = true;

            // Replace the font of every text fragment with the custom font
            Aspose.Pdf.Text.TextFragmentAbsorber absorber = new Aspose.Pdf.Text.TextFragmentAbsorber();
            pdfDoc.Pages.Accept(absorber);

            foreach (Aspose.Pdf.Text.TextFragment fragment in absorber.TextFragments)
            {
                fragment.TextState.Font = customFont;
            }

            // Save the resulting PDF with the embedded font
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF generated with embedded font: {outputPdfPath}");
    }
}