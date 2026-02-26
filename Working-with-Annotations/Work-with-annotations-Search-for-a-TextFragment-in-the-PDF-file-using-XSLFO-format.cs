using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths for the source XML, XSL‑FO stylesheet and the intermediate PDF.
        const string xmlPath   = "source.xml";
        const string xslFoPath = "template.xsl";
        const string pdfPath   = "generated.pdf";

        // Phrase to search for in the resulting PDF.
        const string searchPhrase = "Sample Text";

        // -----------------------------------------------------------------
        // 1. Generate a PDF from XSL‑FO (XML + XSL‑FO stylesheet).
        // -----------------------------------------------------------------
        if (!File.Exists(xmlPath) || !File.Exists(xslFoPath))
        {
            Console.Error.WriteLine("XML or XSL‑FO file not found.");
            return;
        }

        using (Document pdfDoc = new Document())
        {
            // Bind the XML data with the XSL‑FO stylesheet to produce PDF content.
            pdfDoc.BindXml(xmlPath, xslFoPath);

            // Save the generated PDF.
            pdfDoc.Save(pdfPath);
        }

        // -----------------------------------------------------------------
        // 2. Search for the specified text fragment inside the PDF.
        // -----------------------------------------------------------------
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Generated PDF not found: {pdfPath}");
            return;
        }

        using (Document doc = new Document(pdfPath))
        {
            // Create a TextFragmentAbsorber for the phrase we want to locate.
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchPhrase);

            // Include text that resides inside annotations (optional, but often useful).
            absorber.TextSearchOptions.SearchInAnnotations = true;

            // Perform the search on all pages.
            doc.Pages.Accept(absorber);

            // Output the results.
            if (absorber.TextFragments.Count == 0)
            {
                Console.WriteLine($"Phrase \"{searchPhrase}\" not found.");
            }
            else
            {
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    // The Page property gives the page containing the fragment.
                    Console.WriteLine($"Found on page {fragment.Page.Number}: \"{fragment.Text}\"");
                }
            }
        }
    }
}