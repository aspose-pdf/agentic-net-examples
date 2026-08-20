using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string outputPdf = "output.pdf";
        const string customFontPath = "MyCustomFont.ttf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        if (!File.Exists(customFontPath))
        {
            Console.Error.WriteLine($"Custom font file not found: {customFontPath}");
            return;
        }

        // Load the XML file into a PDF document using XmlLoadOptions
        XmlLoadOptions loadOptions = new XmlLoadOptions();
        using (Document doc = new Document(xmlPath, loadOptions))
        {
            // Open the custom TrueType font
            Font customFont = FontRepository.OpenFont(customFontPath);

            // Extract all text fragments from the document
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            doc.Pages.Accept(absorber);

            // Apply the custom font to fragments that belong to specific XML elements.
            // Here we use a simple marker "[Special]" to identify those elements.
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                if (fragment.Text.Contains("[Special]"))
                {
                    fragment.TextState.Font = customFont;
                }
            }

            // Save the resulting PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF generated successfully: {outputPdf}");
    }
}