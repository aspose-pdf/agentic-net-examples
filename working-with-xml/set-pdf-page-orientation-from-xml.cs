using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string xmlPath = "layout.xml";
        const string outputPdf = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML that defines page orientation
        XDocument xdoc = XDocument.Load(xmlPath);

        // Create a new PDF document
        using (Document pdf = new Document())
        {
            // Process each <Page> element in the XML
            foreach (XElement pageElem in xdoc.Root.Elements("Page"))
            {
                // Optional page number attribute; if missing, append sequentially
                int pageNumber = (int?)pageElem.Attribute("number") ?? pdf.Pages.Count + 1;

                // Ensure the document has enough pages
                while (pdf.Pages.Count < pageNumber)
                {
                    pdf.Pages.Add();
                }

                // Read the orientation attribute (landscape or portrait)
                string orientation = (string)pageElem.Attribute("orientation") ?? "portrait";
                bool isLandscape = string.Equals(orientation, "landscape", StringComparison.OrdinalIgnoreCase);

                // Set the page orientation
                pdf.Pages[pageNumber].PageInfo.IsLandscape = isLandscape;

                // Add a simple text fragment to identify the page and its orientation
                TextFragment tf = new TextFragment($"Page {pageNumber} - {(isLandscape ? "Landscape" : "Portrait")}");
                tf.Position = new Position(50, 800);
                pdf.Pages[pageNumber].Paragraphs.Add(tf);
            }

            // Save the PDF with the applied orientations
            pdf.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }
}