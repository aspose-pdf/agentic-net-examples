using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

// Alias to avoid the ambiguous System.IO.Path / Aspose.Pdf.Drawing.Path name clash
using IOPath = System.IO.Path;

class Program
{
    static void Main()
    {
        var inputFiles = new List<string>
        {
            "doc1.pdf",
            "doc2.pdf",
            "doc3.pdf"
        };

        Parallel.ForEach(inputFiles, inputPath =>
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                return;
            }

            // Build the output file name using the alias to System.IO.Path
            string outputPath = IOPath.Combine(
                IOPath.GetDirectoryName(inputPath) ?? string.Empty,
                IOPath.GetFileNameWithoutExtension(inputPath) + "_out.pdf");

            // Load the PDF, modify it, and save it – deterministic disposal via using
            using (var doc = new Document(inputPath))
            {
                // Ensure at least one page exists
                if (doc.Pages.Count == 0)
                {
                    doc.Pages.Add();
                }

                Page page = doc.Pages[1];

                // Create a Graph that spans the whole page. The Graph ctor takes width & height only.
                var graph = new Graph(page.PageInfo.Width, page.PageInfo.Height)
                {
                    Left = 0,
                    Top = 0,
                    // No visible border – we only want the shapes inside the graph
                    Border = new BorderInfo(BorderSide.All, 0f, Color.Transparent)
                };

                // ---------- First ellipse (blue axial gradient) ----------
                var ellipse1 = new Ellipse(100f, 100f, 200f, 100f);
                ellipse1.GraphInfo.FillColor = new Color
                {
                    PatternColorSpace = new GradientAxialShading(Color.LightBlue, Color.DarkBlue)
                    {
                        Start = new Point(100f, 100f),
                        End   = new Point(300f, 200f)
                    }
                };
                // No stroke for the ellipse
                ellipse1.GraphInfo.Color = Color.Transparent;
                graph.Shapes.Add(ellipse1);

                // ---------- Second ellipse (red axial gradient) ----------
                var ellipse2 = new Ellipse(200f, 250f, 200f, 100f);
                ellipse2.GraphInfo.FillColor = new Color
                {
                    PatternColorSpace = new GradientAxialShading(Color.LightCoral, Color.DarkRed)
                    {
                        Start = new Point(200f, 250f),
                        End   = new Point(400f, 350f)
                    }
                };
                ellipse2.GraphInfo.Color = Color.Transparent;
                graph.Shapes.Add(ellipse2);

                // Add the graph to the page. Graph is a BaseParagraph, so it belongs in Paragraphs.
                page.Paragraphs.Add(graph);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Processed and saved: {outputPath}");
        });
    }
}
