using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class ToggleBoundsCheckModeExample
{
    static void Main()
    {
        // User setting: true = strict (throw if shape does not fit), false = ignore bounds
        bool useStrictBoundsCheck = GetUserSetting();

        // Create a new PDF document
        using (Document pdfDoc = new Document())
        {
            // Add a page to the document
            Page page = pdfDoc.Pages.Add();

            // Create a Graph container with a defined size (width=400, height=200)
            // Use the double‑parameter constructor (the float overload is obsolete)
            Graph graph = new Graph(400.0, 200.0);

            // Toggle the bounds‑check mode of the shape collection inside the Graph
            if (useStrictBoundsCheck)
                graph.Shapes.UpdateBoundsCheckMode(BoundsCheckMode.ThrowExceptionIfDoesNotFit);
            else
                graph.Shapes.UpdateBoundsCheckMode(BoundsCheckMode.Default); // ignore bounds

            // Example shape: a rectangle that intentionally exceeds the Graph size
            // Use the drawing Rectangle (Aspose.Pdf.Drawing.Rectangle) and float literals
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 500f, 300f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 1f
            };

            // Add the rectangle to the Graph's shape collection
            graph.Shapes.Add(rect);

            // Add the Graph to the page's content
            page.Paragraphs.Add(graph);

            // Save the PDF document
            pdfDoc.Save("ToggleBoundsCheckMode_Output.pdf");
        }

        Console.WriteLine("PDF created with bounds‑check mode set to " +
                          (useStrictBoundsCheck ? "ThrowExceptionIfDoesNotFit" : "Default (ignore)"));
    }

    // Placeholder for obtaining the user setting (could be from UI, config, etc.)
    static bool GetUserSetting()
    {
        // For demonstration, toggle based on an environment variable; default to false
        string env = Environment.GetEnvironmentVariable("STRICT_BOUNDS_CHECK");
        return string.Equals(env, "true", StringComparison.OrdinalIgnoreCase);
    }
}