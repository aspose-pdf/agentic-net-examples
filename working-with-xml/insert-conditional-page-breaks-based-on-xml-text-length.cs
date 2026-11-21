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
        const string inputPdf = "template.pdf";   // existing PDF to use as a base
        const string xmlPath = "data.xml";        // source XML containing content
        const string outputPdf = "result.pdf";    // destination PDF
        const int charThreshold = 1000;           // max characters per page before a break

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Missing PDF template: {inputPdf}");
            return;
        }
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"Missing XML file: {xmlPath}");
            return;
        }

        // Load XML document
        XDocument xmlDoc = XDocument.Load(xmlPath);

        // Extract paragraphs (or any repeatable text node) from XML
        var paragraphs = xmlDoc.Descendants("Paragraph")
                               .Select(p => (string)p.Value)
                               .Where(t => !string.IsNullOrWhiteSpace(t));

        // Load the PDF inside a using block (deterministic disposal)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Start with the first page (Aspose.Pdf uses 1‑based indexing)
            int currentPageNumber = 1;
            Page currentPage = pdfDoc.Pages[currentPageNumber];
            int accumulatedLength = 0; // characters added to the current page

            foreach (var para in paragraphs)
            {
                int paraLength = para.Length;

                // If adding this paragraph would exceed the threshold, insert a new page
                if (accumulatedLength + paraLength > charThreshold)
                {
                    pdfDoc.Pages.Add(); // adds a new page at the end
                    currentPageNumber = pdfDoc.Pages.Count; // new page becomes the last one
                    currentPage = pdfDoc.Pages[currentPageNumber];
                    accumulatedLength = 0; // reset counter for the new page
                }

                // Add the paragraph text to the current page
                TextFragment tf = new TextFragment(para)
                {
                    Margin = new MarginInfo(20, 20, 20, 20),
                    TextState = { FontSize = 12 }
                };
                currentPage.Paragraphs.Add(tf);

                accumulatedLength += paraLength;
            }

            // Save the modified PDF (Document.Save without options writes PDF)
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with conditional page breaks saved to '{outputPdf}'.");
    }
}