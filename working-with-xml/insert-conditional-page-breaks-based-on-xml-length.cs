using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input XML file that will be converted to PDF
        const string xmlPath = "input.xml";
        // Output PDF file with conditional page breaks
        const string outputPdfPath = "output.pdf";
        // Threshold for content length (characters) that triggers a page break
        const int lengthThreshold = 1000;

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML into a PDF document using XmlLoadOptions
        XmlLoadOptions xmlLoadOptions = new XmlLoadOptions();
        using (Document pdfDoc = new Document(xmlPath, xmlLoadOptions))
        {
            // Parse the XML separately to evaluate content lengths
            XDocument xDoc = XDocument.Load(xmlPath);
            // Assume each top‑level element (e.g., <Section>) maps to a page in order
            var sections = xDoc.Root?.Elements() ?? new XElement[0];

            // Iterate through sections and insert a blank page after the corresponding PDF page
            // when the text length exceeds the defined threshold.
            // Page numbers in Aspose.Pdf are 1‑based.
            int pageIndex = 1; // start with first page
            foreach (var section in sections)
            {
                // Compute the length of the textual content of the current XML element
                int contentLength = section.Value?.Length ?? 0;

                if (contentLength > lengthThreshold)
                {
                    // Insert an empty page after the current page.
                    // The Insert method returns the newly created page, but we do not need it further.
                    pdfDoc.Pages.Insert(pageIndex + 1);
                    // Advance the index to skip the inserted blank page
                    pageIndex++;
                }

                // Move to the next original page
                pageIndex++;
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with conditional page breaks to '{outputPdfPath}'.");
    }
}