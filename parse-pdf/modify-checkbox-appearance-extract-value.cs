using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Annotations; // for Border

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

        // Load the PDF document (using the recommended using block)
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

            // ------------------------------------------------------------
            // Modify the visual appearance of the checkbox using Drawing API
            // ------------------------------------------------------------

            // Change the check box style (e.g., to a check mark)
            checkbox.Style = BoxStyle.Check;

            // Change the color of the check box
            checkbox.Color = Aspose.Pdf.Color.Green;

            // Set a thicker border (Border requires the parent annotation in the ctor)
            checkbox.Border = new Border(checkbox) { Width = 2 };

            // Additionally, draw a red rectangle around the checkbox using Aspose.Pdf.Drawing
            // Get the page that contains the checkbox
            Page page = doc.Pages[checkbox.PageIndex];

            // Create a Graph container (size is arbitrary; it will be positioned by the shape)
            Graph graph = new Graph(200.0, 200.0); // double literals as required

            // Create a drawing rectangle that matches the checkbox bounds
            // Aspose.Pdf.Rectangle is the PDF coordinate rectangle; Aspose.Pdf.Drawing.Rectangle is the shape
            Aspose.Pdf.Rectangle cbRect = checkbox.Rect;
            float width = (float)(cbRect.URX - cbRect.LLX);
            float height = (float)(cbRect.URY - cbRect.LLY);
            var shapeRect = new Aspose.Pdf.Drawing.Rectangle(
                (float)cbRect.LLX,
                (float)cbRect.LLY,
                width,
                height);
            shapeRect.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Red,   // stroke color
                LineWidth = 1f
            };
            graph.Shapes.Add(shapeRect);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // ------------------------------------------------------------
            // Save the modified PDF (optional, but demonstrates the change)
            // ------------------------------------------------------------
            doc.Save(outputPath);

            // ------------------------------------------------------------
            // Extract the checkbox value after appearance changes
            // ------------------------------------------------------------
            string value = checkbox.Value;
            Console.WriteLine($"Checkbox value: {value}");
        }
    }
}
