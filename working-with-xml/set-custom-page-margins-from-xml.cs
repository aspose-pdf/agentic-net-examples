using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";      // source PDF
        const string xmlPath = "margins.xml";    // XML with margin definitions
        const string outputPath = "output.pdf";  // result PDF

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlPath}");
            return;
        }

        // Load the XML that defines margins per section.
        XDocument xmlDoc = XDocument.Load(xmlPath);

        // Open the PDF inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Iterate over each <Section> element.
            foreach (XElement section in xmlDoc.Root.Elements("Section"))
            {
                // Parse page range attributes.
                int startPage = (int?)section.Attribute("StartPage") ?? 1;
                int endPage   = (int?)section.Attribute("EndPage")   ?? startPage;

                // Ensure the range is within the document.
                startPage = Math.Max(1, startPage);
                endPage   = Math.Min(pdfDoc.Pages.Count, endPage);
                if (startPage > endPage) continue;

                // Read margin values (assumed to be in points).
                XElement marginElem = section.Element("Margin");
                if (marginElem == null) continue;

                double top    = (double?)marginElem.Attribute("Top")    ?? 0;
                double bottom = (double?)marginElem.Attribute("Bottom") ?? 0;
                double left   = (double?)marginElem.Attribute("Left")   ?? 0;
                double right  = (double?)marginElem.Attribute("Right")  ?? 0;

                // Build a MarginInfo instance with the specified values.
                MarginInfo marginInfo = new MarginInfo
                {
                    Top    = top,
                    Bottom = bottom,
                    Left   = left,
                    Right  = right
                };

                // Apply the margin to each page in the range.
                for (int pageNum = startPage; pageNum <= endPage; pageNum++)
                {
                    pdfDoc.Pages[pageNum].PageInfo.Margin = marginInfo;
                }
            }

            // Save the modified PDF.
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom margins to '{outputPath}'.");
    }
}
