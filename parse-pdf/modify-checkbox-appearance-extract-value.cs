using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "modified.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Locate the first checkbox field in the form
            CheckboxField checkbox = null;
            foreach (var field in doc.Form.Fields)
            {
                if (field is CheckboxField cb)
                {
                    checkbox = cb;
                    break;
                }
            }

            if (checkbox == null)
            {
                Console.WriteLine("No checkbox field found in the document.");
                return;
            }

            // Determine the page containing the checkbox (PageIndex is zero‑based)
            int pageNumber = checkbox.PageIndex + 1; // Aspose.Pdf uses 1‑based page indexing
            Page page = doc.Pages[pageNumber];

            // Get the rectangle of the checkbox (Aspose.Pdf.Rectangle)
            Aspose.Pdf.Rectangle cbRect = checkbox.Rect;

            // Create a Graph that covers the whole page (required for absolute positioning)
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Define a rectangle shape that matches the checkbox bounds
            var shapeRect = new Aspose.Pdf.Drawing.Rectangle(
                (float)cbRect.LLX,
                (float)cbRect.LLY,
                (float)cbRect.Width,
                (float)cbRect.Height);
            shapeRect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray, // new fill color
                Color = Aspose.Pdf.Color.DarkBlue,     // border color
                LineWidth = 1
            };
            graph.Shapes.Add(shapeRect);

            // Add the graph to the page's content
            page.Paragraphs.Add(graph);

            // Extract the checkbox value after modifying its appearance
            string value = checkbox.Value;
            Console.WriteLine($"Checkbox value: {value}");

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
