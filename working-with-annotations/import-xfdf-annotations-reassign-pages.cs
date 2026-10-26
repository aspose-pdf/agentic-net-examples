using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string xfdfPath = "annotations.xfdf";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        // Predefined mapping: XFDF annotation name (or id) -> target page number (1‑based)
        var annotationPageMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            // Example entries – adjust as needed
            { "Annot1", 1 },
            { "Annot2", 2 },
            { "Annot3", 3 }
        };

        // Load XFDF as plain XML (Aspose.Pdf.Xfdf assembly is optional)
        XDocument xfdfDoc;
        try
        {
            xfdfDoc = XDocument.Load(xfdfPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load XFDF: {ex.Message}");
            return;
        }

        // Parse annotation elements
        var xfdfAnnotations = new List<XElement>();
        foreach (var annot in xfdfDoc.Descendants("annot"))
        {
            xfdfAnnotations.Add(annot);
        }

        using (Document pdfDoc = new Document(inputPdfPath))
        {
            foreach (var annotElem in xfdfAnnotations)
            {
                // Retrieve the annotation identifier (e.g., name or id attribute)
                string annotId = (string)annotElem.Attribute("name") ?? (string)annotElem.Attribute("id");
                if (string.IsNullOrEmpty(annotId))
                    continue; // cannot map without an identifier

                // Determine target page using the predefined dictionary
                if (!annotationPageMap.TryGetValue(annotId, out int targetPageNumber))
                {
                    // If not found in the map, optionally fall back to the page attribute in XFDF
                    if (int.TryParse((string)annotElem.Attribute("page"), out int xfdfPage))
                        targetPageNumber = xfdfPage;
                    else
                        continue; // skip if no mapping available
                }

                // Ensure the page number is within range
                if (targetPageNumber < 1 || targetPageNumber > pdfDoc.Pages.Count)
                    continue;

                Page page = pdfDoc.Pages[targetPageNumber];

                // Extract rectangle coordinates (llx lly urx ury) – space separated
                string rectAttr = (string)annotElem.Attribute("rect");
                if (string.IsNullOrWhiteSpace(rectAttr))
                    continue;

                var rectParts = rectAttr.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (rectParts.Length != 4 ||
                    !double.TryParse(rectParts[0], out double llx) ||
                    !double.TryParse(rectParts[1], out double lly) ||
                    !double.TryParse(rectParts[2], out double urx) ||
                    !double.TryParse(rectParts[3], out double ury))
                {
                    continue; // malformed rectangle
                }

                // Create a rectangle using the Aspose.Pdf.Rectangle type (fully qualified to avoid ambiguity)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                // Determine annotation type – default to TextAnnotation if unknown
                string type = ((string)annotElem.Attribute("type"))?.ToLowerInvariant() ?? "text";

                // Common properties
                string contents = (string)annotElem.Element("contents") ?? string.Empty;
                string title = (string)annotElem.Element("title") ?? string.Empty;

                // Create and configure the appropriate annotation
                Annotation annotation = null;

                switch (type)
                {
                    case "text":
                        var textAnnot = new TextAnnotation(page, rect);
                        textAnnot.Title = title;
                        textAnnot.Contents = contents;
                        textAnnot.Open = true;
                        textAnnot.Icon = TextIcon.Note;
                        annotation = textAnnot;
                        break;

                    case "highlight":
                        var highlight = new HighlightAnnotation(page, rect);
                        highlight.Contents = contents;
                        // Example: set a semi‑transparent yellow color
                        highlight.Color = Aspose.Pdf.Color.FromRgb(1.0, 1.0, 0.0);
                        highlight.Opacity = 0.5;
                        annotation = highlight;
                        break;

                    case "strikeout":
                        var strike = new StrikeOutAnnotation(page, rect);
                        strike.Contents = contents;
                        annotation = strike;
                        break;

                    case "underline":
                        var underline = new UnderlineAnnotation(page, rect);
                        underline.Contents = contents;
                        annotation = underline;
                        break;

                    // Add more cases as needed for other annotation types
                    default:
                        // Fallback to a simple text annotation
                        var fallback = new TextAnnotation(page, rect);
                        fallback.Title = title;
                        fallback.Contents = contents;
                        fallback.Open = true;
                        fallback.Icon = TextIcon.Note;
                        annotation = fallback;
                        break;
                }

                if (annotation != null)
                {
                    page.Annotations.Add(annotation);
                }
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Annotations imported and PDF saved to '{outputPdfPath}'.");
    }
}