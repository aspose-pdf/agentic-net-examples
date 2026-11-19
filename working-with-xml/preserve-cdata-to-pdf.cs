using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

// Alias to disambiguate the LoadOptions enum that exists in both System.Xml.Linq and Aspose.Pdf
using XmlLoadOptions = System.Xml.Linq.LoadOptions;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string pdfPath = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML document preserving CDATA sections and whitespace
        XDocument xDoc = XDocument.Load(xmlPath, XmlLoadOptions.PreserveWhitespace | XmlLoadOptions.SetLineInfo);

        // Create a new PDF document
        using (Document pdfDoc = new Document())
        {
            // Add a single page to host the formatted text
            Page page = pdfDoc.Pages.Add();

            // Define a monospaced font for CDATA content
            Font cdataFont = FontRepository.FindFont("Courier New");

            // Iterate over all CDATA sections in the XML
            foreach (XCData cdata in xDoc.DescendantNodes().OfType<XCData>())
            {
                // Create a text fragment with the CDATA content
                TextFragment tf = new TextFragment(cdata.Value);

                // Apply formatting: monospaced font, light gray background for visibility
                tf.TextState.Font = cdataFont;
                tf.TextState.FontSize = 12;
                tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
                tf.TextState.BackgroundColor = Aspose.Pdf.Color.LightGray;

                // NOTE: TextFragment automatically respects line‑break characters (\n, \r\n) in the text.
                // Therefore no explicit "IsWrapped" property is required; removing the non‑existent
                // property eliminates the CS1061 compile error while preserving the original line breaks.

                // Add the fragment to the page
                page.Paragraphs.Add(tf);

                // Add an empty line after each CDATA block for readability
                page.Paragraphs.Add(new TextFragment(Environment.NewLine));
            }

            // Save the PDF document
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF created at '{pdfPath}'.");
    }
}
