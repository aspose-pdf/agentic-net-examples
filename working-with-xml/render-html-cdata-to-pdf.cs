using System;
using System.IO;
using System.Linq;                     // <-- Added for LINQ extension methods
using System.Xml.Linq;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Text;               // For text‑related classes (if needed)

class Program
{
    static void Main()
    {
        // Paths for the source XML and the resulting PDF.
        const string xmlPath = "source.xml";
        const string pdfPath = "result.pdf";

        // Verify that the XML file exists.
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"Error: XML file not found – {xmlPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Create an empty PDF document (lifecycle: create).
        // -----------------------------------------------------------------
        using (Document pdfDoc = new Document())
        {
            // Ensure there is at least one page to host the fragments.
            pdfDoc.Pages.Add();

            // -----------------------------------------------------------------
            // 2. Load the XML file using standard .NET XML APIs.
            //    (The XML may contain CDATA sections with HTML fragments.)
            // -----------------------------------------------------------------
            XDocument xDoc = XDocument.Load(xmlPath);

            // Counter to keep track of which page we are writing to.
            int currentPageNumber = 1;

            // Iterate over every CDATA node found in the XML.
            foreach (XCData cdata in xDoc.DescendantNodes().OfType<XCData>())
            {
                // The CDATA content is expected to be an HTML fragment.
                string htmlContent = cdata.Value.Trim();

                if (string.IsNullOrEmpty(htmlContent))
                    continue; // Skip empty fragments.

                // -----------------------------------------------------------------
                // 3. Create an HtmlFragment from the HTML string.
                //    (Lifecycle: create – the fragment is a formatted PDF element.)
                // -----------------------------------------------------------------
                HtmlFragment htmlFragment = new HtmlFragment(htmlContent);

                // Optional: customize loading options for this fragment.
                // For example, set a base path if the HTML references external resources.
                // htmlFragment.HtmlLoadOptions = new HtmlLoadOptions(Path.GetDirectoryName(xmlPath));

                // -----------------------------------------------------------------
                // 4. Add the fragment to the current page.
                // -----------------------------------------------------------------
                Page page = pdfDoc.Pages[currentPageNumber];
                page.Paragraphs.Add(htmlFragment);

                // If you want each fragment on a separate page, uncomment the block below:
                /*
                currentPageNumber++;
                pdfDoc.Pages.Add();
                */
            }

            // -----------------------------------------------------------------
            // 5. Save the populated PDF document (lifecycle: save).
            // -----------------------------------------------------------------
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF generated successfully: {pdfPath}");
    }
}
