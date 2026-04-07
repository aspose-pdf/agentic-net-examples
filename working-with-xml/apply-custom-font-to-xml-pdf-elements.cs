using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";          // Source XML file
        const string outputPdf = "output.pdf";       // Destination PDF file
        const string customFontPath = "MyCustomFont.ttf"; // Path to the custom TrueType font

        // Verify required files exist
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

        // Load XML using XmlLoadOptions (required for XML → PDF conversion)
        XmlLoadOptions loadOptions = new XmlLoadOptions();

        // Document lifecycle: create, load, use, save, dispose via using
        using (Document pdfDoc = new Document(xmlPath, loadOptions))
        {
            // Load the custom font from file
            Font customFont = FontRepository.OpenFont(customFontPath);

            // ------------------------------------------------------------
            // Apply the custom font to specific XML‑derived elements.
            // Example: all text fragments containing the word "Title"
            // ------------------------------------------------------------
            TextFragmentAbsorber titleAbsorber = new TextFragmentAbsorber("Title");
            pdfDoc.Pages.Accept(titleAbsorber);
            foreach (TextFragment tf in titleAbsorber.TextFragments)
            {
                tf.TextState.Font = customFont;                     // Set custom font
                tf.TextState.FontSize = 14;                         // Desired size
                tf.TextState.ForegroundColor = Aspose.Pdf.Color.Blue; // Optional color
            }

            // Example: all text fragments containing the word "Subtitle"
            TextFragmentAbsorber subtitleAbsorber = new TextFragmentAbsorber("Subtitle");
            pdfDoc.Pages.Accept(subtitleAbsorber);
            foreach (TextFragment tf in subtitleAbsorber.TextFragments)
            {
                tf.TextState.Font = customFont;
                tf.TextState.FontSize = 12;
                tf.TextState.ForegroundColor = Aspose.Pdf.Color.DarkGreen;
            }

            // Save the resulting PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF generated with custom font: {outputPdf}");
    }
}