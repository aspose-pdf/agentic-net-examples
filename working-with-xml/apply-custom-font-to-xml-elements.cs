using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths to the source XML, output PDF and the custom TrueType font file.
        const string xmlPath       = "input.xml";
        const string outputPdfPath = "output.pdf";
        const string customFontPath = "MyCustomFont.ttf";

        // Ensure the required files exist.
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

        // Load the XML file into a PDF document using XmlLoadOptions.
        using (Document pdfDoc = new Document(xmlPath, new XmlLoadOptions()))
        {
            // Create a TextFragmentAbsorber to collect all text fragments.
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();

            // Accept the absorber on all pages.
            pdfDoc.Pages.Accept(absorber);

            // Load the custom font once; reuse it for all matching fragments.
            Font customFont = FontRepository.OpenFont(customFontPath);

            // Iterate over each text fragment and apply the custom font
            // to fragments that belong to a specific XML element.
            // For illustration, we treat any fragment whose text starts with
            // the marker "[Special]" as belonging to that element.
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                if (fragment.Text.StartsWith("[Special]"))
                {
                    fragment.TextState.Font = customFont;   // Apply custom font
                    fragment.TextState.FontSize = 12;       // Desired size
                    // Optionally set other visual properties.
                    fragment.TextState.ForegroundColor = Color.Black;
                }
            }

            // Save the modified PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF generated with custom font applied: {outputPdfPath}");
    }
}