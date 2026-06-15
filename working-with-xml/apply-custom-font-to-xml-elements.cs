using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string xmlPath   = "input.xml";
        const string outputPdf = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"File not found: {xmlPath}");
            return;
        }

        // Load the XML file with default options
        XmlLoadOptions loadOptions = new XmlLoadOptions();

        // Lifecycle: create, load, save
        using (Document doc = new Document(xmlPath, loadOptions))
        {
            // Custom font to be applied (e.g., Arial)
            Font customFont = FontRepository.FindFont("Arial");

            // Example: apply the custom font to all text fragments that match a specific pattern.
            // Adjust the search string or use a regular expression as needed for your XML elements.
            TextFragmentAbsorber absorber = new TextFragmentAbsorber("YourElementText");
            doc.Pages.Accept(absorber);

            foreach (TextFragment tf in absorber.TextFragments)
            {
                tf.TextState.Font = customFont;               // Set custom font
                tf.TextState.FontSize = 12;                  // Optional: set size
                tf.TextState.ForegroundColor = Color.Black;  // Optional: set color
            }

            // Save the resulting PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF generated with custom font: {outputPdf}");
    }
}