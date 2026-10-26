using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";
        const string xfdfPath  = "annotations.xfdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF not found: {xfdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Load and parse the XFDF file (simple XML format)
            XDocument xfdfDoc = XDocument.Load(xfdfPath);
            XNamespace ns = "http://ns.adobe.com/xfdf/";

            // Iterate over each annotation element in the XFDF
            foreach (XElement annotElem in xfdfDoc.Descendants(ns + "annots").Elements())
            {
                // Extract required fields (page number, type, rectangle, contents)
                // XFDF stores page numbers as 0‑based; Aspose.Pdf uses 1‑based indexing
                int pageNumber = (int)annotElem.Element(ns + "page") + 1;

                // Ensure the page exists
                if (pageNumber < 1 || pageNumber > pdfDoc.Pages.Count)
                    continue;

                string type = (string)annotElem.Element(ns + "type");
                string rectStr = (string)annotElem.Element(ns + "rect");
                string contents = (string)annotElem.Element(ns + "contents") ?? string.Empty;

                // Parse rectangle coordinates: left bottom right top (as strings separated by spaces)
                // Example: "100 200 300 400"
                double llx = 0, lly = 0, urx = 0, ury = 0;
                if (!string.IsNullOrWhiteSpace(rectStr))
                {
                    string[] parts = rectStr.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 4)
                    {
                        double.TryParse(parts[0], out llx);
                        double.TryParse(parts[1], out lly);
                        double.TryParse(parts[2], out urx);
                        double.TryParse(parts[3], out ury);
                    }
                }

                // Create the appropriate annotation based on the type.
                // For this example we handle only Text and Highlight annotations.
                Page page = pdfDoc.Pages[pageNumber];
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                if (string.Equals(type, "Text", StringComparison.OrdinalIgnoreCase))
                {
                    // Text annotation (sticky note)
                    TextAnnotation txtAnnot = new TextAnnotation(page, rect)
                    {
                        Title    = "Note",
                        Contents = contents,
                        Open     = false,
                        Icon     = TextIcon.Note
                    };
                    page.Annotations.Add(txtAnnot);
                }
                else if (string.Equals(type, "Highlight", StringComparison.OrdinalIgnoreCase))
                {
                    // Highlight annotation
                    HighlightAnnotation hlAnnot = new HighlightAnnotation(page, rect)
                    {
                        Color = Aspose.Pdf.Color.Yellow,
                        Contents = contents
                    };
                    page.Annotations.Add(hlAnnot);
                }
                // Additional annotation types can be added here following the same pattern.
            }

            // Save the modified PDF preserving original layout and content
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Annotations imported and saved to '{outputPdf}'.");
    }
}