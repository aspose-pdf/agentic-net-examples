using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // Graph, Line, Shape, etc.

class Program
{
    // Simple DTO for JSON serialization
    private class LineInfo
    {
        public int PageNumber { get; set; }
        public double X1 { get; set; }
        public double Y1 { get; set; }
        public double X2 { get; set; }
        public double Y2 { get; set; }
    }

    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "lines.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Wrap Document in a using block (lifecycle rule)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            var lines = new List<LineInfo>();

            // Pages are 1‑based (page-indexing-one-based rule)
            for (int pageIdx = 1; pageIdx <= pdfDoc.Pages.Count; pageIdx++)
            {
                Page page = pdfDoc.Pages[pageIdx];

                // Iterate over page paragraphs; Graph objects contain drawing shapes
                foreach (var paragraph in page.Paragraphs)
                {
                    if (paragraph is Graph graph)
                    {
                        // Graph.Shapes holds Shape objects (Line, Rectangle, Curve, etc.)
                        foreach (var shape in graph.Shapes)
                        {
                            if (shape is Line lineShape)
                            {
                                // Line stores its coordinates in PositionArray: {x1, y1, x2, y2, ...}
                                float[] pts = lineShape.PositionArray;
                                if (pts != null && pts.Length >= 4)
                                {
                                    lines.Add(new LineInfo
                                    {
                                        PageNumber = pageIdx,
                                        X1 = pts[0],
                                        Y1 = pts[1],
                                        X2 = pts[2],
                                        Y2 = pts[3]
                                    });
                                }
                            }
                        }
                    }
                }
            }

            // Serialize the collected line information to JSON
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(lines, jsonOptions);
            File.WriteAllText(outputJsonPath, json);
            Console.WriteLine($"Extracted {lines.Count} line(s) to '{outputJsonPath}'.");
        }
    }
}
