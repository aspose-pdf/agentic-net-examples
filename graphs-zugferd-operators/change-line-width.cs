using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Operators;

class Program
{
    static void Main()
    {
        string inputPath = "input.pdf";
        string outputPath = "output.pdf";

        // Ensure the input file exists – if not, create a simple PDF for demonstration.
        if (!File.Exists(inputPath))
        {
            CreateSamplePdf(inputPath);
        }

        using (Document document = new Document(inputPath))
        {
            // Iterate through all pages in the document
            for (int pageIndex = 1; pageIndex <= document.Pages.Count; pageIndex++)
            {
                Page page = document.Pages[pageIndex];

                // The OperatorCollection is zero‑based. Use foreach for safe enumeration.
                foreach (Operator op in page.Contents)
                {
                    // Look for SetLineWidth operators that set the width to 1 point
                    if (op is SetLineWidth setLineWidth && Math.Abs(setLineWidth.Width - 1.0) < 0.0001)
                    {
                        // Change the line width to 3 points
                        setLineWidth.Width = 3.0;
                    }
                }
            }

            document.Save(outputPath);
        }

        Console.WriteLine($"Line widths updated and saved to {outputPath}");
    }

    /// <summary>
    /// Creates a minimal PDF containing a single line drawn with a 1‑point width.
    /// This helper is used only when the expected input file is missing.
    /// </summary>
    private static void CreateSamplePdf(string path)
    {
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            // Add a simple line using low‑level operators: set line width, move to, line to, stroke.
            OperatorCollection ops = page.Contents;
            ops.Add(new SetLineWidth(1.0));          // line width = 1 point
            ops.Add(new MoveTo(100, 500));           // move to start point
            ops.Add(new LineTo(400, 500));           // draw line to end point
            ops.Add(new Stroke());                  // stroke the path
            doc.Save(path);
        }
    }
}
