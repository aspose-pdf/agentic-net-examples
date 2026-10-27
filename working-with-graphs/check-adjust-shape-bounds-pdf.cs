using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // for Graph and shape types

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Page dimensions
                double pageWidth = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;

                // Shapes are stored inside Graph objects that are added to the page's Paragraphs collection.
                foreach (var paragraph in page.Paragraphs)
                {
                    if (paragraph is Graph graph)
                    {
                        // Iterate over each shape in the graph
                        foreach (var shape in graph.Shapes)
                        {
                            // We only know how to adjust rectangle‑type shapes because they expose
                            // Left, Bottom, Width and Height. Other shape types would need their own
                            // handling logic.
                            if (shape is Aspose.Pdf.Drawing.Rectangle rectShape)
                            {
                                // Current bounds of the rectangle shape
                                double left   = rectShape.Left;
                                double bottom = rectShape.Bottom;
                                double right  = left + rectShape.Width;
                                double top    = bottom + rectShape.Height;

                                // Horizontal correction
                                if (left < 0)
                                {
                                    double shift = -left;
                                    left  += shift;
                                    right += shift;
                                }
                                else if (right > pageWidth)
                                {
                                    double shift = right - pageWidth;
                                    left  -= shift;
                                    right -= shift;
                                }

                                // Vertical correction
                                if (bottom < 0)
                                {
                                    double shift = -bottom;
                                    bottom += shift;
                                    top    += shift;
                                }
                                else if (top > pageHeight)
                                {
                                    double shift = top - pageHeight;
                                    bottom -= shift;
                                    top    -= shift;
                                }

                                // Apply the corrected position back to the rectangle shape.
                                // Width and Height stay the same; only Left and Bottom are updated.
                                rectShape.Left   = (float)left;
                                rectShape.Bottom = (float)bottom;
                            }
                        }
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
