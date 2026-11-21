using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    // Simple representation of an annotation extracted from an FDF file
    private class SimpleAnnotation
    {
        public int PageNumber { get; set; }               // 1‑based page index
        public string Type { get; set; }                  // e.g., "Text", "Highlight"
        public Aspose.Pdf.Rectangle Rect { get; set; }    // annotation rectangle
        public string Contents { get; set; }              // annotation text
    }

    static void Main()
    {
        const string inputPdfPath = "source.pdf";                 // PDF to receive annotations
        const string outputPdfPath = "consolidated.pdf";          // Resulting PDF
        string[] fdfFiles = { "ann1.fdf", "ann2.fdf", "ann3.fdf" }; // FDF sources

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }

        // Load the target PDF
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Collect all annotations from all FDF files
            List<SimpleAnnotation> allAnnotations = new List<SimpleAnnotation>();

            foreach (string fdfPath in fdfFiles)
            {
                if (!File.Exists(fdfPath))
                {
                    Console.Error.WriteLine($"FDF not found: {fdfPath}");
                    continue;
                }

                try
                {
                    allAnnotations.AddRange(ParseFdfFile(fdfPath));
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error parsing '{fdfPath}': {ex.Message}");
                }
            }

            // Add each annotation to the appropriate page
            foreach (SimpleAnnotation ann in allAnnotations)
            {
                // Validate page number
                if (ann.PageNumber < 1 || ann.PageNumber > pdfDoc.Pages.Count)
                {
                    Console.Error.WriteLine($"Invalid page {ann.PageNumber} for annotation; skipping.");
                    continue;
                }

                // Retrieve the target page
                Page targetPage = pdfDoc.Pages[ann.PageNumber];

                // Currently only TextAnnotation is demonstrated.
                // Additional types can be added with similar logic.
                if (string.Equals(ann.Type, "Text", StringComparison.OrdinalIgnoreCase))
                {
                    // Create a TextAnnotation using the core API
                    TextAnnotation txtAnn = new TextAnnotation(targetPage, ann.Rect)
                    {
                        Title    = "Note",
                        Contents = ann.Contents,
                        Open     = true,
                        Icon     = TextIcon.Note
                    };

                    targetPage.Annotations.Add(txtAnn);
                }
                else if (string.Equals(ann.Type, "Highlight", StringComparison.OrdinalIgnoreCase))
                {
                    // Example of a HighlightAnnotation
                    HighlightAnnotation hlAnn = new HighlightAnnotation(targetPage, ann.Rect)
                    {
                        Color = Aspose.Pdf.Color.Yellow,
                        Contents = ann.Contents
                    };

                    targetPage.Annotations.Add(hlAnn);
                }
                else
                {
                    Console.Error.WriteLine($"Unsupported annotation type '{ann.Type}'; skipping.");
                }
            }

            // Save the consolidated PDF
            pdfDoc.Save(outputPdfPath);
            Console.WriteLine($"Consolidated PDF saved to '{outputPdfPath}'.");
        }
    }

    // Parses a very simple custom FDF-like text file.
    // Expected line format (semicolon‑separated key/value pairs):
    // Page=1;Type=Text;Rect=100,200,300,400;Contents=Sample note
    private static IEnumerable<SimpleAnnotation> ParseFdfFile(string fdfPath)
    {
        List<SimpleAnnotation> result = new List<SimpleAnnotation>();

        foreach (string rawLine in File.ReadAllLines(fdfPath))
        {
            string line = rawLine.Trim();
            if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
                continue; // Skip empty lines and comments

            Dictionary<string, string> dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            string[] parts = line.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string part in parts)
            {
                string[] kv = part.Split(new[] { '=' }, 2);
                if (kv.Length == 2)
                    dict[kv[0].Trim()] = kv[1].Trim();
            }

            // Required fields
            if (!dict.TryGetValue("Page", out string pageStr) ||
                !dict.TryGetValue("Type", out string type) ||
                !dict.TryGetValue("Rect", out string rectStr) ||
                !dict.TryGetValue("Contents", out string contents))
            {
                Console.Error.WriteLine($"Incomplete annotation definition in '{fdfPath}': {line}");
                continue;
            }

            if (!int.TryParse(pageStr, out int pageNum))
            {
                Console.Error.WriteLine($"Invalid page number '{pageStr}' in '{fdfPath}'.");
                continue;
            }

            // Parse rectangle: llx,lly,urx,ury
            string[] rectVals = rectStr.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (rectVals.Length != 4 ||
                !double.TryParse(rectVals[0], out double llx) ||
                !double.TryParse(rectVals[1], out double lly) ||
                !double.TryParse(rectVals[2], out double urx) ||
                !double.TryParse(rectVals[3], out double ury))
            {
                Console.Error.WriteLine($"Invalid rectangle '{rectStr}' in '{fdfPath}'.");
                continue;
            }

            SimpleAnnotation ann = new SimpleAnnotation
            {
                PageNumber = pageNum,
                Type       = type,
                Rect       = new Aspose.Pdf.Rectangle(llx, lly, urx, ury),
                Contents   = contents
            };

            result.Add(ann);
        }

        return result;
    }
}