using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "TitlePage.pdf";

        // Create a new empty PDF document
        using (Document doc = new Document())
        {
            // Set the PDF document title (metadata)
            doc.Info.Title = "Sample PDF with Custom Title Page";

            // Add a new page (this will be the title page)
            Page titlePage = doc.Pages.Add();

            // -------------------------------------------------
            // Add a colored background rectangle using Graph
            // -------------------------------------------------
            // Create a Graph container (size matches the page)
            Graph graph = new Graph(titlePage.PageInfo.Width, titlePage.PageInfo.Height);

            // Define a rectangle shape that covers the whole page (float parameters required)
            var backgroundRect = new Aspose.Pdf.Drawing.Rectangle(
                0f,
                0f,
                (float)titlePage.PageInfo.Width,
                (float)titlePage.PageInfo.Height)
            {
                GraphInfo = new GraphInfo
                {
                    FillColor = Color.LightBlue,   // Background fill color
                    Color = Color.LightBlue        // Border color (same as fill to hide border)
                }
            };

            // Add the rectangle shape to the graph
            graph.Shapes.Add(backgroundRect);

            // Add the Graph to the page
            titlePage.Paragraphs.Add(graph);

            // -------------------------------------------------
            // Add the title text with custom font, size, and color
            // -------------------------------------------------
            // Create a TextFragment for the title
            TextFragment titleFragment = new TextFragment("My Custom Title")
            {
                // Position the text (centered horizontally, near the top)
                Position = new Position(titlePage.PageInfo.Width / 2, titlePage.PageInfo.Height - 100)
            };

            // Set appearance via TextState
            titleFragment.TextState.Font = FontRepository.FindFont("Helvetica");
            titleFragment.TextState.FontSize = 36;
            titleFragment.TextState.ForegroundColor = Color.DarkBlue;
            titleFragment.TextState.HorizontalAlignment = HorizontalAlignment.Center;

            // Add the TextFragment to the page
            titlePage.Paragraphs.Add(titleFragment);

            // Save the document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with title page saved to '{outputPath}'.");
    }
}