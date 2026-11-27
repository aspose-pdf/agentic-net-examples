using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "filtered.svg";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load source PDF
        using (Document srcDoc = new Document(inputPath))
        {
            const int targetPageNumber = 16;
            if (srcDoc.Pages.Count < targetPageNumber)
            {
                Console.Error.WriteLine($"Document has only {srcDoc.Pages.Count} pages; page {targetPageNumber} is unavailable.");
                return;
            }

            Page srcPage = srcDoc.Pages[targetPageNumber];
            // PageInfo.Height returns double – cast to float for later calculations
            float pageHeight = (float)srcPage.PageInfo.Height;

            // ------------------------------------------------------------
            // 1. Create a GraphicsAbsorber via reflection (avoids direct
            //    reference to Aspose.Pdf.Facades namespace which is forbidden).
            // ------------------------------------------------------------
            Type absorberType = Type.GetType("Aspose.Pdf.Facades.GraphicsAbsorber, Aspose.Pdf");
            if (absorberType == null)
            {
                Console.Error.WriteLine("GraphicsAbsorber type not found. Ensure Aspose.Pdf library version supports it.");
                return;
            }
            object absorber = Activator.CreateInstance(absorberType);

            // Accept the absorber on the target page.
            MethodInfo acceptMethod = typeof(Page).GetMethod("Accept", new[] { absorberType });
            acceptMethod.Invoke(srcPage, new[] { absorber });

            // ------------------------------------------------------------
            // 2. Prepare the output PDF (will be saved as SVG).
            // ------------------------------------------------------------
            using (Document outDoc = new Document())
            {
                Page outPage = outDoc.Pages.Add();

                // Retrieve the collection of absorbed graphics objects via reflection.
                PropertyInfo graphicsProp = absorberType.GetProperty("GraphicsObjects");
                var graphicsCollection = graphicsProp.GetValue(absorber) as System.Collections.IEnumerable;

                foreach (var graphic in graphicsCollection)
                {
                    // We are interested only in rectangle shapes.
                    if (graphic is Aspose.Pdf.Drawing.Rectangle rect && rect.Width > 100)
                    {
                        // ------------------------------------------------
                        // 3. Transfer the rectangle to the output page.
                        //    A rectangle cannot be drawn directly; it must be
                        //    placed inside a Graph container.
                        // ------------------------------------------------
                        // Create a Graph whose size matches the rectangle (cast to float).
                        Graph graph = new Graph((float)rect.Width, (float)rect.Height);

                        // Position the graph on the page so that it matches the
                        // original rectangle location.
                        // Graph.Top is measured from the top of the page.
                        graph.Left = (float)rect.Left;
                        // pageHeight is from bottom, so we convert to top‑based coordinate.
                        graph.Top = pageHeight - (float)rect.Bottom - (float)rect.Height;

                        // Create a rectangle shape inside the graph (origin 0,0).
                        var shape = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, (float)rect.Width, (float)rect.Height);
                        shape.GraphInfo.Color = Color.Black;          // stroke color
                        shape.GraphInfo.LineWidth = 1;                // stroke width
                        shape.GraphInfo.FillColor = Color.Transparent; // no fill

                        graph.Shapes.Add(shape);
                        outPage.Paragraphs.Add(graph);
                    }
                }

                // Save the result as SVG.
                SvgSaveOptions svgOptions = new SvgSaveOptions();
                outDoc.Save(outputPath, svgOptions);
            }
        }

        Console.WriteLine($"Filtered SVG saved to '{outputPath}'.");
    }
}
