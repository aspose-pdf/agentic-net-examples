using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Step 1: Create a sample PDF containing a red rectangle (vector graphic)
        string inputPath = "input.pdf";
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            // Create a Graph container (canvas) for drawing shapes
            Aspose.Pdf.Drawing.Graph graph = new Aspose.Pdf.Drawing.Graph(600.0, 800.0);
            // Define a rectangle shape: lower‑left (100,500), width 200, height 200
            Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(100f, 500f, 200f, 200f);
            rectShape.GraphInfo = new GraphInfo { Color = Aspose.Pdf.Color.Red };
            // Add the rectangle to the graph and the graph to the page
            graph.Shapes.Add(rectShape);
            page.Paragraphs.Add(graph);
            doc.Save(inputPath);
        }

        // Step 2: Load the PDF and convert its vector graphics to SVG
        using (Document pdfDoc = new Document(inputPath))
        {
            string svgPath = "output.svg";
            Aspose.Pdf.SvgSaveOptions svgOptions = new Aspose.Pdf.SvgSaveOptions();
            pdfDoc.Save(svgPath, svgOptions);

            // Step 3: Read the generated SVG, replace the original red color with the brand blue color, and write back
            string svgContent = File.ReadAllText(svgPath);
            string transformedSvg = svgContent.Replace("#FF0000", "#0033A0");
            transformedSvg = transformedSvg.Replace("#ff0000", "#0033A0");
            File.WriteAllText(svgPath, transformedSvg);
        }

        Console.WriteLine("Vector graphics extracted and brand colors applied to SVG.");
    }
}