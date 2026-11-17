using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "portfolio_input.pdf";
        const string outputPath = "portfolio_custom_template.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF Portfolio (or any PDF that will become a portfolio)
        using (Document doc = new Document(inputPath))
        {
            // Insert a new cover page as the first page of the document
            Page coverPage = doc.Pages.Insert(1);

            // Set page size (A4) – dimensions are in points (1 point = 1/72 inch)
            coverPage.PageInfo.Width = 595;   // A4 width
            coverPage.PageInfo.Height = 842;  // A4 height

            // ------------------------------------------------------------
            // 1. Draw a background rectangle that fills the whole page.
            //    Shapes must be placed inside a Graph object, which is a
            //    paragraph that can be added to the page.
            // ------------------------------------------------------------
            Graph graph = new Graph(coverPage.PageInfo.Width, coverPage.PageInfo.Height);

            // Rectangle constructor expects float values (left, bottom, width, height)
            var bgRect = new Aspose.Pdf.Drawing.Rectangle(
                0f,
                0f,
                (float)coverPage.PageInfo.Width,
                (float)coverPage.PageInfo.Height);

            // Styling is done through the GraphInfo property
            bgRect.GraphInfo.FillColor = Aspose.Pdf.Color.LightGray;
            bgRect.GraphInfo.Color = Aspose.Pdf.Color.Transparent; // no border

            graph.Shapes.Add(bgRect);
            coverPage.Paragraphs.Add(graph);

            // ------------------------------------------------------------
            // 2. Add title text fragment
            // ------------------------------------------------------------
            TextFragment title = new TextFragment("My Portfolio");
            title.Position = new Position(50, coverPage.PageInfo.Height - 100);
            title.TextState.FontSize = 36;
            title.TextState.FontStyle = FontStyles.Bold;
            title.TextState.ForegroundColor = Aspose.Pdf.Color.DarkBlue;
            coverPage.Paragraphs.Add(title);

            // ------------------------------------------------------------
            // 3. Add subtitle text fragment
            // ------------------------------------------------------------
            TextFragment subtitle = new TextFragment("Generated with Aspose.PDF");
            subtitle.Position = new Position(50, coverPage.PageInfo.Height - 150);
            subtitle.TextState.FontSize = 18;
            subtitle.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
            coverPage.Paragraphs.Add(subtitle);

            // ------------------------------------------------------------
            // 4. (Optional) If you want the document to be recognized as a
            //    portfolio, ensure a Collection exists and add at least one
            //    embedded file. The cover page itself does not need a special
            //    Portfolio.CoverPage property – that member does not exist.
            // ------------------------------------------------------------
            if (doc.Collection == null)
                doc.Collection = new Collection();

            // Example: embed the original PDF as an attachment inside the portfolio
            var fileSpec = new FileSpecification(inputPath, System.IO.Path.GetFileName(inputPath))
            {
                Contents = new MemoryStream(File.ReadAllBytes(inputPath))
            };
            doc.Collection.Add(fileSpec);

            // Save the modified document (now with a custom cover page)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Portfolio saved with custom template to '{outputPath}'.");
    }
}
