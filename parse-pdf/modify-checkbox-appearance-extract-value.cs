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

            if (checkbox != null)
            {
                // Retrieve the rectangle of the checkbox (Aspose.Pdf.Rectangle)
                Aspose.Pdf.Rectangle cbRect = checkbox.Rect;

                // Create a Graph that matches the size of the checkbox
                Graph graph = new Graph(cbRect.Width, cbRect.Height);

                // Define a drawing rectangle shape with custom fill and border colors
                var rectShape = new Aspose.Pdf.Drawing.Rectangle(
                    0f,
                    0f,
                    (float)cbRect.Width,
                    (float)cbRect.Height);
                rectShape.GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.LightGreen,
                    Color = Aspose.Pdf.Color.DarkBlue,
                    LineWidth = 2f
                };
                graph.Shapes.Add(rectShape);

                // Position the graph exactly over the checkbox
                graph.Margin = new MarginInfo(cbRect.LLX, cbRect.LLY, 0, 0);

                // Add the graph to the page that contains the checkbox
                doc.Pages[checkbox.PageIndex].Paragraphs.Add(graph);

                // Extract the checkbox value after appearance modification
                string value = checkbox.Value;
                Console.WriteLine($"Checkbox value: {value}");
            }
            else
            {
                Console.WriteLine("No checkbox field found in the document.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
