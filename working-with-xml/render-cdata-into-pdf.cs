using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";   // XML source containing CDATA sections
        const string pdfPath = "output.pdf";  // Resulting PDF file

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML document and collect all CDATA sections
        XDocument xDoc = XDocument.Load(xmlPath);
        var cdataNodes = xDoc.DescendantNodes()
                             .OfType<XCData>();

        // Create a new PDF document
        using (Document pdfDoc = new Document())
        {
            // Add a single page (pages are 1‑based)
            Page page = pdfDoc.Pages.Add();

            // Position variables for placing each CDATA block
            double cursorY = page.PageInfo.Height - 50; // start near top margin
            const double marginX = 50;
            const double lineHeight = 20;

            foreach (XCData cdata in cdataNodes)
            {
                // Create a TextFragment with the CDATA content
                TextFragment fragment = new TextFragment(cdata.Value);

                // Configure the fragment's TextState (read‑only property, modify the instance instead of assigning)
                fragment.TextState.Font = FontRepository.FindFont("Helvetica");
                fragment.TextState.FontSize = 12;
                fragment.TextState.ForegroundColor = Color.Black;

                // Set the fragment position
                fragment.Position = new Position(marginX, cursorY);

                // Add the fragment to the page
                page.Paragraphs.Add(fragment);

                // Move cursor down for the next block
                cursorY -= lineHeight;
                if (cursorY < 50) // simple page overflow handling
                {
                    // Add a new page and reset cursor
                    page = pdfDoc.Pages.Add();
                    cursorY = page.PageInfo.Height - 50;
                }
            }

            // Save the PDF (no SaveOptions needed for PDF output)
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF with CDATA sections saved to '{pdfPath}'.");
    }
}
