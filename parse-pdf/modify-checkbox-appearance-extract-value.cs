using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

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
            CheckboxField checkbox = null;
            foreach (Field field in doc.Form)
            {
                if (field is CheckboxField cb)
                {
                    checkbox = cb;
                    break;
                }
            }

            if (checkbox == null)
            {
                Console.WriteLine("No checkbox field found.");
                doc.Save(outputPath); // save unchanged document
                return;
            }

            // ---- Modify appearance using Aspose.Pdf.Drawing ----
            // Change the visual style of the checkbox (e.g., use a check mark)
            checkbox.Style = BoxStyle.Check;               // set built‑in style
            checkbox.Color = Aspose.Pdf.Color.Green;       // change border/color

            // Additionally, draw a custom check mark on the same page using Drawing API
            Page page = doc.Pages[checkbox.PageIndex];     // 1‑based page index
            // Create a graph container (size large enough for the checkbox)
            Graph graph = new Graph(200.0, 200.0);

            // Use the checkbox rectangle to compute absolute coordinates for the check mark
            Aspose.Pdf.Rectangle cbRect = checkbox.Rect;
            // Define a line that looks like a check mark, positioned inside the checkbox rectangle
            float[] linePoints = {
                (float)(cbRect.LLX + 20), // start X
                (float)(cbRect.LLY + 80), // start Y
                (float)(cbRect.LLX + 80), // end X
                (float)(cbRect.LLY + 20)  // end Y
            };
            Line checkLine = new Line(linePoints);
            checkLine.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Green,
                LineWidth = 2f
            };
            graph.Shapes.Add(checkLine);

            // Add the graph to the page (no need for a transformation matrix)
            page.Paragraphs.Add(graph);
            // -----------------------------------------------------

            // Extract the checkbox value after appearance changes
            string value = checkbox.Value;
            Console.WriteLine($"Checkbox value: {value}");

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
