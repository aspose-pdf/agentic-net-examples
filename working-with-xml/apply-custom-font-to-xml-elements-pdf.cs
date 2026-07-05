using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths to the source XML and the resulting PDF.
        const string xmlPath   = "input.xml";
        const string pdfPath   = "output.pdf";

        // Verify the XML file exists.
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML file into a PDF document using XmlLoadOptions.
        XmlLoadOptions loadOptions = new XmlLoadOptions();
        using (Document pdfDoc = new Document(xmlPath, loadOptions))
        {
            // ------------------------------------------------------------
            // Apply a custom font to specific text fragments.
            // Example: change the font of all occurrences of the word "Header"
            // to a custom TrueType font (e.g., "Arial").
            // ------------------------------------------------------------

            // Find all text fragments containing the target word.
            TextFragmentAbsorber absorber = new TextFragmentAbsorber("Header");
            pdfDoc.Pages.Accept(absorber);

            // Load the custom font.
            Font customFont = FontRepository.FindFont("Arial"); // replace with desired font name

            // Apply the custom font to each found fragment.
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                fragment.TextState.Font = customFont;
                // Optionally adjust size and color for better readability.
                fragment.TextState.FontSize = 14;
                fragment.TextState.ForegroundColor = Color.Black;
            }

            // Save the modified PDF.
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF generated with custom font: {pdfPath}");
    }
}