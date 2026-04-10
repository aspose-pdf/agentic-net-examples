using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "MagazineLayout.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // ------------------------------------------------------------
            // Page setup – define page size (A4) and margins using MarginInfo
            // ------------------------------------------------------------
            doc.PageInfo.Width = 595;   // A4 width in points
            doc.PageInfo.Height = 842;  // A4 height in points
            // left, right, top, bottom margins (in points)
            doc.PageInfo.Margin = new MarginInfo(40, 40, 40, 40);

            // ------------------------------------------------------------
            // Create a FloatingBox that will hold the multi‑column content
            // ------------------------------------------------------------
            FloatingBox box = new FloatingBox();

            // Configure column layout: 2 columns, 5‑point spacing,
            // column widths expressed as space‑separated values
            box.ColumnInfo.ColumnCount = 2;
            box.ColumnInfo.ColumnSpacing = "5";
            box.ColumnInfo.ColumnWidths = "260 260";

            // ------------------------------------------------------------
            // Add a headline (large bold text) to the box
            // ------------------------------------------------------------
            TextFragment headline = new TextFragment("The Future of Technology");
            headline.TextState.Font = FontRepository.FindFont("Helvetica");
            headline.TextState.FontSize = 24;
            headline.TextState.FontStyle = FontStyles.Bold;
            headline.TextState.ForegroundColor = Aspose.Pdf.Color.DarkBlue;
            // Add a blank line after the headline
            headline.TextState.Underline = false;
            box.Paragraphs.Add(headline);
            box.Paragraphs.Add(new TextFragment(Environment.NewLine));

            // ------------------------------------------------------------
            // Add article body using an HtmlFragment (allows simple HTML markup)
            // ------------------------------------------------------------
            string html = @"
                <p style='text-align:justify;'>
                    <strong>Innovation</strong> drives the modern world. From artificial intelligence
                    to quantum computing, the pace of change is unprecedented. Companies are
                    investing heavily in research and development to stay ahead of the curve.
                </p>
                <p style='text-align:justify;'>
                    The rise of <em>edge computing</em> brings processing power closer to the
                    data source, reducing latency and improving performance for critical
                    applications such as autonomous vehicles and IoT devices.
                </p>";
            HtmlFragment article = new HtmlFragment(html);
            box.Paragraphs.Add(article);

            // ------------------------------------------------------------
            // Insert a horizontal line separator using Graph + Line
            // ------------------------------------------------------------
            Graph separator = new Graph(500, 2); // width, height
            float[] lineCoords = { 0, 0, 500, 0 };
            Line line = new Line(lineCoords);
            line.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Gray,
                LineWidth = 1
            };
            separator.Shapes.Add(line);
            // Add some spacing before the line
            box.Paragraphs.Add(new TextFragment(Environment.NewLine));
            box.Paragraphs.Add(separator);
            box.Paragraphs.Add(new TextFragment(Environment.NewLine));

            // ------------------------------------------------------------
            // Add an advertisement image (e.g., banner.jpg) below the article
            // ------------------------------------------------------------
            const string adImagePath = "banner.jpg"; // ensure this file exists
            if (File.Exists(adImagePath))
            {
                Image adImage = new Image();
                adImage.File = adImagePath;
                // Scale the image to fit the column width (approx. 260 points)
                adImage.FixWidth = 260;
                box.Paragraphs.Add(adImage);
            }
            else
            {
                // Fallback: placeholder text if image is missing
                TextFragment placeholder = new TextFragment("[Advertisement Image Missing]");
                placeholder.TextState.Font = FontRepository.FindFont("Helvetica");
                placeholder.TextState.FontSize = 12;
                placeholder.TextState.ForegroundColor = Aspose.Pdf.Color.Red;
                box.Paragraphs.Add(placeholder);
            }

            // ------------------------------------------------------------
            // Add the configured FloatingBox to the first page
            // ------------------------------------------------------------
            Page page = doc.Pages.Add();
            page.Paragraphs.Add(box);

            // ------------------------------------------------------------
            // Save the PDF document
            // ------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"Magazine‑style PDF created at '{outputPath}'.");
    }
}