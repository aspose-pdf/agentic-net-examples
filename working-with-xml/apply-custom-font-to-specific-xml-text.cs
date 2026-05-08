using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths to the source XML and the output PDF.
        const string xmlPath = "input.xml";
        const string pdfPath = "output.pdf";

        // Verify that the XML file exists.
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML file into a PDF document using XmlLoadOptions.
        // No XSL is supplied, so the default conversion is used.
        XmlLoadOptions loadOptions = new XmlLoadOptions();
        using (Document pdfDoc = new Document(xmlPath, loadOptions))
        {
            // ------------------------------------------------------------
            // Apply a custom font to specific text elements.
            // In this example we target all occurrences of the word
            // "Important" that were generated from the XML.
            // ------------------------------------------------------------

            // Find the custom font (ensure it is installed on the machine).
            // Times New Roman is used here as an example.
            Font customFont = FontRepository.FindFont("Times New Roman");
            // Embed the font into the PDF so the document is self‑contained.
            customFont.IsEmbedded = true;

            // Create a TextFragmentAbsorber that looks for the target text.
            TextFragmentAbsorber absorber = new TextFragmentAbsorber("Important");

            // Apply the absorber to every page in the document.
            foreach (Page page in pdfDoc.Pages)
            {
                page.Accept(absorber);
            }

            // Update the font (and optionally size) for each found fragment.
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                fragment.TextState.Font = customFont;
                fragment.TextState.FontSize = 14; // Adjust size as needed.
            }

            // Save the modified PDF.
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF generated successfully: {pdfPath}");
    }
}