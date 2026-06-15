using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "modified.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Find the first checkbox field in the form
            CheckboxField checkbox = doc.Form.Fields
                                          .OfType<CheckboxField>()
                                          .FirstOrDefault();

            if (checkbox == null)
            {
                Console.WriteLine("No checkbox field found in the document.");
                return;
            }

            // ---- Modify appearance using Aspose.Pdf.Drawing ----
            // Get the page that contains the checkbox (pages are 1‑based)
            Page page = doc.Pages[checkbox.PageIndex];

            // Create a Graph container using double dimensions (required constructor)
            var graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Convert the checkbox rectangle (Aspose.Pdf.Rectangle) to a drawing rectangle
            float llx = (float)checkbox.Rect.LLX;
            float lly = (float)checkbox.Rect.LLY;
            float rectWidth  = (float)(checkbox.Rect.URX - checkbox.Rect.LLX);
            float rectHeight = (float)(checkbox.Rect.URY - checkbox.Rect.LLY);

            var drawRect = new Aspose.Pdf.Drawing.Rectangle(llx, lly, rectWidth, rectHeight);
            drawRect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.DarkGray,
                LineWidth = 1f
            };

            // Add the rectangle shape to the graph
            graph.Shapes.Add(drawRect);

            // Insert the graph at the beginning of the page's content so it appears behind form fields
            page.Paragraphs.Insert(0, graph);

            // Optionally change the checkbox style and color directly
            checkbox.Style = BoxStyle.Check;          // use a check‑mark style
            checkbox.Color = Aspose.Pdf.Color.Blue;   // change the box border color
            checkbox.Checked = true;                  // ensure it is checked (for demonstration)

            // ---- Extract the checkbox value ----
            string value = checkbox.Value; // e.g., "Off" or the export value of the checked state
            Console.WriteLine($"Checkbox value: {value}");

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
