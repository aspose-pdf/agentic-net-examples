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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF (it may already contain attachments – a PDF "portfolio")
        using (Document doc = new Document(inputPath))
        {
            // Ensure the PDF has a collection (portfolio) – if not, create one so we can add attachments later if needed
            if (doc.Collection == null)
                doc.Collection = new Collection();

            // -----------------------------------------------------------------
            // 1. Add a background rectangle (full‑page size) with a light gray fill
            // -----------------------------------------------------------------
            foreach (Page page in doc.Pages)
            {
                // Create a Graph container that spans the whole page
                Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

                // Rectangle constructor: left, bottom, width, height (float values)
                Aspose.Pdf.Drawing.Rectangle bgRect = new Aspose.Pdf.Drawing.Rectangle(
                    0f,
                    0f,
                    (float)page.PageInfo.Width,
                    (float)page.PageInfo.Height);

                // Styling via GraphInfo
                bgRect.GraphInfo.FillColor = Color.LightGray;
                bgRect.GraphInfo.Color = Color.Transparent; // no border

                graph.Shapes.Add(bgRect);
                page.Paragraphs.Add(graph);
            }

            // -----------------------------------------------------------------
            // 2. Add a title text fragment centered near the top of each page
            // -----------------------------------------------------------------
            foreach (Page page in doc.Pages)
            {
                TextFragment title = new TextFragment("My PDF Portfolio");
                title.Position = new Position(page.PageInfo.Width / 2, page.PageInfo.Height - 100);
                title.TextState.FontSize = 24;
                title.TextState.ForegroundColor = Color.DarkBlue;
                title.TextState.HorizontalAlignment = HorizontalAlignment.Center;
                page.Paragraphs.Add(title);
            }

            // -----------------------------------------------------------------
            // 3. Optionally add a background image (if a file exists) – stretched to page size
            // -----------------------------------------------------------------
            const string bgImagePath = "portfolio_bg.png";
            if (File.Exists(bgImagePath))
            {
                foreach (Page page in doc.Pages)
                {
                    // Use ImageStamp to place an image on the page
                    ImageStamp imgStamp = new ImageStamp(bgImagePath)
                    {
                        // Stretch to cover the whole page
                        Width = page.PageInfo.Width,
                        Height = page.PageInfo.Height,
                        // Position at lower‑left corner using alignment properties
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Bottom,
                        XIndent = 0,
                        YIndent = 0,
                        // Place the image behind other content
                        Background = true
                    };
                    page.AddStamp(imgStamp);
                }
            }

            // -----------------------------------------------------------------
            // 4. (Optional) Add a new file to the portfolio collection as an example
            // -----------------------------------------------------------------
            const string extraFile = "extra_document.pdf";
            if (File.Exists(extraFile))
            {
                var fileSpec = new FileSpecification(extraFile, "Extra document attached")
                {
                    Contents = new MemoryStream(File.ReadAllBytes(extraFile))
                };
                doc.Collection.Add(fileSpec);
            }

            // Save the modified PDF (still a regular PDF – the collection makes it a portfolio)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Custom visual template applied and saved to '{outputPath}'.");
    }
}
