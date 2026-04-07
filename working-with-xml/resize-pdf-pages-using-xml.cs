using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath   = "pagesize.xml";   // XML defining page width/height
        const string inputPdf  = "input.pdf";      // Source PDF
        const string outputPdf = "output.pdf";     // Destination PDF

        // Verify files exist
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"PDF file not found: {inputPdf}");
            return;
        }

        // -----------------------------------------------------------------
        // Parse XML – expect a node like:
        // <PageSize Width="595" Height="842" />
        // -----------------------------------------------------------------
        double width = 0, height = 0;
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlPath);
        XmlNode sizeNode = xmlDoc.SelectSingleNode("//PageSize");
        if (sizeNode != null)
        {
            double.TryParse(sizeNode.Attributes["Width"]?.Value, out width);
            double.TryParse(sizeNode.Attributes["Height"]?.Value, out height);
        }

        if (width <= 0 || height <= 0)
        {
            Console.Error.WriteLine("Invalid or missing page size values in XML.");
            return;
        }

        // -----------------------------------------------------------------
        // Load PDF, adjust each page size, and save.
        // -----------------------------------------------------------------
        using (Document pdfDoc = new Document(inputPdf)) // document-disposal-with-using rule
        {
            // Pages collection is 1‑based (page-indexing-one-based rule)
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                // Set the size directly; SetPageSize expects width & height as double.
                pdfDoc.Pages[i].SetPageSize(width, height);
                // Alternative using Resize:
                // pdfDoc.Pages[i].Resize(new PageSize((float)width, (float)height));
            }

            // Save the modified PDF (document.Save without SaveOptions writes PDF)
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"All pages resized to {width}×{height} and saved as '{outputPdf}'.");
    }
}