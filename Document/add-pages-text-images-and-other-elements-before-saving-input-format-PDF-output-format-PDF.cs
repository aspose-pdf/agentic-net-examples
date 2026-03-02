using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string imagePath  = "image.png";

        // Load existing PDF if it exists; otherwise start with a new empty document.
        Document doc = File.Exists(inputPath) ? new Document(inputPath) : new Document();

        // Ensure the document is disposed properly.
        using (doc)
        {
            // Add a new blank page at the end of the document.
            Page newPage = doc.Pages.Add();

            // ----- Add a text fragment -----
            TextFragment text = new TextFragment("Hello Aspose.Pdf!");
            text.Position = new Position(100, 700);                     // Position on the page
            text.TextState.FontSize = 20;
            text.TextState.Font = FontRepository.FindFont("Helvetica");
            text.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
            newPage.Paragraphs.Add(text);

            // ----- Add an image (if the file is present) -----
            if (File.Exists(imagePath))
            {
                Image img = new Image
                {
                    File = imagePath,
                    FixWidth = 200,
                    FixHeight = 150
                };
                newPage.Paragraphs.Add(img);
            }

            // ----- Draw a rectangle shape using Graph -----
            // Graph is a container for vector shapes.
            Graph graph = new Graph(500, 300); // width and height of the drawing canvas

            // Create a rectangle shape (left, bottom, width, height).
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(50, 200, 250, 100);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 2
            };
            graph.Shapes.Add(rect);

            // Add the graph (which contains the rectangle) to the page.
            newPage.Paragraphs.Add(graph);

            // Save the modified document as PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}