using System;
using System.IO;
using System.Xml;

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

        // Load the XML layout definition
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlPath);

        // Create a new PDF document
        using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document())
        {
            // Expect <Page orientation="landscape|portrait" /> elements
            XmlNodeList pageNodes = xmlDoc.SelectNodes("//Page");
            if (pageNodes == null || pageNodes.Count == 0)
            {
                Console.Error.WriteLine("No <Page> elements found in XML.");
                return;
            }

            foreach (XmlNode pageNode in pageNodes)
            {
                // Add a new page to the document
                Aspose.Pdf.Page page = pdfDoc.Pages.Add();

                // Default A4 portrait dimensions
                double width = Aspose.Pdf.PageSize.A4.Width;
                double height = Aspose.Pdf.PageSize.A4.Height;

                // Determine orientation from the XML attribute
                string orientation = pageNode.Attributes["orientation"]?.Value?.ToLowerInvariant();
                if (orientation == "landscape")
                {
                    // Swap width and height for landscape layout
                    double temp = width;
                    width = height;
                    height = temp;
                }

                // Apply the calculated page size
                page.PageInfo.Width = width;
                page.PageInfo.Height = height;

                // Add a simple label showing the page number and orientation
                string label = $"Page {pdfDoc.Pages.Count} - {(orientation ?? "portrait")}";
                Aspose.Pdf.Text.TextFragment tf = new Aspose.Pdf.Text.TextFragment(label);
                tf.Position = new Aspose.Pdf.Text.Position(50, height - 50);
                page.Paragraphs.Add(tf);
            }

            // Save the resulting PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }
}