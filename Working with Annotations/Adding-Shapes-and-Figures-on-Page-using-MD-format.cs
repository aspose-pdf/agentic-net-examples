using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_shapes.pdf";
        const string imagePath  = "sample_image.jpg";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // -------------------------------------------------
            // 1. Add vector shapes (rectangle, ellipse, line)
            // -------------------------------------------------
            Page page = doc.Pages[1]; // 1‑based indexing

            // Create a Graph that covers the whole page
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Rectangle shape
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(100F, 500F, 200F, 100F);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 2
            };
            graph.Shapes.Add(rect);

            // Ellipse shape
            Aspose.Pdf.Drawing.Ellipse ellipse = new Aspose.Pdf.Drawing.Ellipse(300F, 600F, 150F, 100F);
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.Yellow,
                Color = Aspose.Pdf.Color.Red,
                LineWidth = 1.5F
            };
            graph.Shapes.Add(ellipse);

            // Line shape
            float[] linePoints = { 100F, 400F, 300F, 400F };
            Aspose.Pdf.Drawing.Line line = new Aspose.Pdf.Drawing.Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Blue,
                LineWidth = 2
            };
            graph.Shapes.Add(line);

            // Add the Graph to the page
            page.Paragraphs.Add(graph);

            // -------------------------------------------------
            // 2. Add an image using PdfFileMend (Facades API)
            // -------------------------------------------------
            using (PdfFileMend mend = new PdfFileMend())
            {
                // Bind the facade to the current document
                mend.BindPdf(doc);

                // Add the image to page 1 at the specified rectangle
                // lower‑left (50, 700), upper‑right (150, 800)
                mend.AddImage(imagePath, 1, 50F, 700F, 150F, 800F);
            }

            // -------------------------------------------------
            // 3. Create a Figure element in the tagged structure
            // -------------------------------------------------
            ITaggedContent taggedContent = doc.TaggedContent;
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle("PDF with Shapes and Figure");

            // Root element of the tagged content tree
            StructureElement root = taggedContent.RootElement;

            // Create a Figure element and set its alternative text
            FigureElement figure = taggedContent.CreateFigureElement();
            figure.AlternativeText = "Illustrative figure with an embedded image";

            // Append the figure to the root of the structure tree
            root.AppendChild(figure);

            // -------------------------------------------------
            // Save the modified document
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}