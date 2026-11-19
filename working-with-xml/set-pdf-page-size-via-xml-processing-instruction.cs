using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string pdfPath = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Default values
        PageSize selectedSize = PageSize.A4;
        string selectedSizeName = "A4";
        bool isLandscape = false;

        // Load XML and read processing instructions
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlPath);
        foreach (XmlNode node in xmlDoc.ChildNodes)
        {
            if (node.NodeType == XmlNodeType.ProcessingInstruction)
            {
                XmlProcessingInstruction pi = (XmlProcessingInstruction)node;
                // Example: <?pdf pageSize="A4" orientation="Landscape"?>
                string[] parts = pi.Data.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string part in parts)
                {
                    string[] kv = part.Split('=');
                    if (kv.Length != 2) continue;
                    string key = kv[0].Trim();
                    string value = kv[1].Trim('"');

                    if (key.Equals("pageSize", StringComparison.OrdinalIgnoreCase))
                    {
                        selectedSize = GetPageSize(value);
                        selectedSizeName = value.ToUpperInvariant();
                    }
                    else if (key.Equals("orientation", StringComparison.OrdinalIgnoreCase))
                    {
                        isLandscape = value.Equals("Landscape", StringComparison.OrdinalIgnoreCase);
                    }
                }
            }
        }

        // Create PDF with the selected page size
        using (Document pdfDoc = new Document())
        {
            Page page = pdfDoc.Pages.Add();

            // Determine width and height (points) from the PageSize object
            double width = selectedSize.Width;
            double height = selectedSize.Height;
            if (isLandscape)
            {
                // Swap for landscape orientation
                double tmp = width;
                width = height;
                height = tmp;
            }

            // Apply size to the page
            page.SetPageSize(width, height);

            // Add a text fragment describing the chosen settings
            TextFragment tf = new TextFragment($"Page size: {selectedSizeName}, Orientation: {(isLandscape ? "Landscape" : "Portrait")}");
            // Position near the top‑left corner
            tf.Position = new Position(50, page.PageInfo.Height - 50);
            page.Paragraphs.Add(tf);

            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF generated at '{pdfPath}'.");
    }

    // Helper method to map string names to Aspose.Pdf.PageSize objects
    static PageSize GetPageSize(string name)
    {
        switch (name.ToUpperInvariant())
        {
            case "A0": return PageSize.A0;
            case "A1": return PageSize.A1;
            case "A2": return PageSize.A2;
            case "A3": return PageSize.A3;
            case "A4": return PageSize.A4;
            case "A5": return PageSize.A5;
            case "LETTER":
                // Letter size is 8.5 x 11 inches => 612 x 792 points
                return new PageSize(612, 792);
            case "LEGAL":
                // Legal size is 8.5 x 14 inches => 612 x 1008 points
                return new PageSize(612, 1008);
            default:
                // fallback to A4 if unknown
                return PageSize.A4;
        }
    }
}
