using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath   = "input.xml";   // source XML with processing instructions
        const string pdfPath   = "output.pdf";  // destination PDF

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Default page size (A4) – will be overridden if a processing instruction is found
        double pageWidth  = 595; // points (8.27 inch)
        double pageHeight = 842; // points (11.69 inch)

        // -----------------------------------------------------------------
        // Scan the XML file for a processing instruction that specifies page size.
        // Expected format: <?pdf-page-size width=595 height=842?>
        // -----------------------------------------------------------------
        using (XmlReader reader = XmlReader.Create(xmlPath))
        {
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.ProcessingInstruction &&
                    string.Equals(reader.Name, "pdf-page-size", StringComparison.OrdinalIgnoreCase))
                {
                    // The data part looks like: "width=595 height=842"
                    string[] parts = reader.Value.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string part in parts)
                    {
                        string[] kv = part.Split('=');
                        if (kv.Length != 2) continue;

                        if (kv[0].Equals("width", StringComparison.OrdinalIgnoreCase) &&
                            double.TryParse(kv[1], out double w))
                        {
                            pageWidth = w;
                        }
                        else if (kv[0].Equals("height", StringComparison.OrdinalIgnoreCase) &&
                                 double.TryParse(kv[1], out double h))
                        {
                            pageHeight = h;
                        }
                    }
                }
            }
        }

        // -----------------------------------------------------------------
        // Load the XML into a PDF document using XmlLoadOptions.
        // -----------------------------------------------------------------
        XmlLoadOptions loadOptions = new XmlLoadOptions(); // no XSL required
        using (Document pdfDocument = new Document(xmlPath, loadOptions))
        {
            // Apply the page size to every page in the document.
            // Aspose.Pdf uses 1‑based indexing for pages.
            for (int i = 1; i <= pdfDocument.Pages.Count; i++)
            {
                pdfDocument.Pages[i].SetPageSize(pageWidth, pageHeight);
            }

            // Save the resulting PDF.
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"PDF generated at '{pdfPath}' with page size {pageWidth}×{pageHeight} points.");
    }
}