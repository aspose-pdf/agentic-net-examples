using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for TextFragment if needed

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf rule)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Iterate all Rectangle shapes on the page.
                // Use fully qualified type to avoid ambiguity with System.Drawing.Rectangle.
                foreach (Aspose.Pdf.Rectangle shape in page.Paragraphs.OfType<Aspose.Pdf.Rectangle>())
                {
                    // Retrieve the page size (media box) for boundary checks
                    double pageWidth  = page.PageInfo.Width;
                    double pageHeight = page.PageInfo.Height;

                    // Current shape bounds
                    double llx = shape.LLX; // lower‑left X
                    double lly = shape.LLY; // lower‑left Y
                    double urx = shape.URX; // upper‑right X
                    double ury = shape.URY; // upper‑right Y

                    bool moved = false;

                    // If shape extends beyond the right edge, shift it left
                    if (urx > pageWidth)
                    {
                        double shift = urx - pageWidth;
                        shape.LLX -= shift;
                        shape.URX -= shift;
                        moved = true;
                    }

                    // If shape extends beyond the left edge, shift it right
                    if (shape.LLX < 0)
                    {
                        double shift = -shape.LLX;
                        shape.LLX += shift;
                        shape.URX += shift;
                        moved = true;
                    }

                    // If shape extends beyond the top edge, shift it down
                    if (ury > pageHeight)
                    {
                        double shift = ury - pageHeight;
                        shape.LLY -= shift;
                        shape.URY -= shift;
                        moved = true;
                    }

                    // If shape extends beyond the bottom edge, shift it up
                    if (shape.LLY < 0)
                    {
                        double shift = -shape.LLY;
                        shape.LLY += shift;
                        shape.URY += shift;
                        moved = true;
                    }

                    if (moved)
                    {
                        Console.WriteLine($"Adjusted shape on page {pageIndex}: LLX={shape.LLX}, LLY={shape.LLY}, URX={shape.URX}, URY={shape.URY}");
                    }
                }
            }

            // Save the modified document (save inside the using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}