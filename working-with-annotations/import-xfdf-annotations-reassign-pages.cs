using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xfdfPath = "annotations.xfdf";
        const string outputPath = "output.pdf";

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

        // Mapping from source page numbers (as stored in the XFDF file) to target page numbers in the PDF.
        var pageMapping = new Dictionary<int, int>
        {
            { 1, 3 },
            { 2, 5 }
            // Add additional mappings as required.
        };

        // Load the PDF document.
        using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(pdfPath))
        {
            // Load and parse the XFDF file manually (avoids the Aspose.Pdf.Xfdf assembly).
            XDocument xfdfDoc = XDocument.Load(xfdfPath);

            // The XFDF structure is typically: <xfdf><annots><.../></annots></xfdf>
            // Iterate over each annotation element.
            foreach (XElement annElement in xfdfDoc.Descendants("annots").Elements())
            {
                // Extract the source page number. XFDF stores it in the "page" attribute.
                int sourcePage = (int?)annElement.Attribute("page") ?? 0;
                if (!pageMapping.TryGetValue(sourcePage, out int targetPage))
                    continue; // No mapping for this page – skip.

                // Determine the annotation type – e.g., "Text", "Highlight", etc.
                string type = (string)annElement.Attribute("type") ?? "Text";

                // Parse rectangle coordinates ("rect" attribute: "llx,lly,urx,ury").
                Rectangle rect = ParseRectangle((string)annElement.Attribute("rect"));

                // Common properties.
                string contents = (string)annElement.Element("contents") ?? string.Empty;
                string title = (string)annElement.Element("title") ?? string.Empty;

                // Create the appropriate Aspose.Pdf annotation based on the type.
                Annotation annotation = CreateAnnotation(type, pdfDoc.Pages[targetPage], rect, contents, title);
                if (annotation != null)
                {
                    pdfDoc.Pages[targetPage].Annotations.Add(annotation);
                }
            }

            // Save the updated PDF with the imported annotations.
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Annotations imported and saved to '{outputPath}'.");
    }

    // Parses a rectangle string "llx,lly,urx,ury" into an Aspose.Pdf.Rectangle.
    private static Rectangle ParseRectangle(string rectString)
    {
        if (string.IsNullOrWhiteSpace(rectString))
            return new Rectangle(0, 0, 0, 0);

        string[] parts = rectString.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 4)
            return new Rectangle(0, 0, 0, 0);

        double llx = double.TryParse(parts[0], out double v0) ? v0 : 0;
        double lly = double.TryParse(parts[1], out double v1) ? v1 : 0;
        double urx = double.TryParse(parts[2], out double v2) ? v2 : 0;
        double ury = double.TryParse(parts[3], out double v3) ? v3 : 0;
        return new Rectangle(llx, lly, urx, ury);
    }

    // Factory method that creates a concrete Annotation based on the XFDF type.
    private static Annotation CreateAnnotation(string type, Page page, Rectangle rect, string contents, string title)
    {
        switch (type.ToLowerInvariant())
        {
            case "text":
                var textAnn = new TextAnnotation(page, rect)
                {
                    Contents = contents,
                    Color = Color.Yellow // default visual cue
                };
                // Title belongs to MarkupAnnotation – TextAnnotation derives from it.
                if (!string.IsNullOrEmpty(title))
                    ((MarkupAnnotation)textAnn).Title = title;
                return textAnn;

            case "highlight":
                var highlight = new HighlightAnnotation(page, rect)
                {
                    Contents = contents,
                    Color = Color.Yellow
                };
                if (!string.IsNullOrEmpty(title))
                    ((MarkupAnnotation)highlight).Title = title;
                return highlight;

            case "underline":
                var underline = new UnderlineAnnotation(page, rect)
                {
                    Contents = contents,
                    Color = Color.Yellow
                };
                if (!string.IsNullOrEmpty(title))
                    ((MarkupAnnotation)underline).Title = title;
                return underline;

            case "strikeout":
                var strike = new StrikeOutAnnotation(page, rect)
                {
                    Contents = contents,
                    Color = Color.Yellow
                };
                if (!string.IsNullOrEmpty(title))
                    ((MarkupAnnotation)strike).Title = title;
                return strike;

            // Add more cases for other annotation types as needed.
            default:
                // Unsupported type – ignore.
                return null;
        }
    }
}
