using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for HtmlFragment
using Aspose.Pdf.Drawing; // for page handling (optional)

class Program
{
    static void Main()
    {
        // Input XML file containing CDATA sections with HTML fragments
        const string xmlPath = "input.xml";
        // Output PDF file
        const string pdfPath = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        try
        {
            // Load the XML document (no special load options required)
            XDocument xDoc = XDocument.Load(xmlPath);

            // Create a new empty PDF document
            using (Document pdfDoc = new Document())
            {
                // Add a single page to host the HTML fragments
                Page page = pdfDoc.Pages.Add();

                // Iterate over all CDATA nodes in the XML
                foreach (XCData cdata in xDoc.DescendantNodes().OfType<XCData>())
                {
                    // The CDATA value is an HTML fragment
                    string htmlContent = cdata.Value;

                    // Create an HtmlFragment instance (renders HTML into PDF)
                    HtmlFragment htmlFragment = new HtmlFragment(htmlContent);

                    // Optionally, you can set HtmlLoadOptions on the fragment if needed
                    // htmlFragment.HtmlLoadOptions = new HtmlLoadOptions();

                    // Add the fragment to the page's paragraph collection
                    page.Paragraphs.Add(htmlFragment);
                }

                // Save the resulting PDF
                pdfDoc.Save(pdfPath);
            }

            Console.WriteLine($"PDF generated successfully: {pdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}