using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string outputPdf = "TableWithHighDpiImages.pdf";
        const string imagePath1 = "highres1.jpg";
        const string imagePath2 = "highres2.png";

        // Ensure the image files exist before proceeding
        if (!File.Exists(imagePath1) || !File.Exists(imagePath2))
        {
            Console.Error.WriteLine("One or more image files not found.");
            return;
        }

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Create a table with two columns
            Table table = new Table
            {
                // Define column widths (in points). Adjust as required.
                ColumnWidths = "200 200",
                // Optional visual styling
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // ---------- First Row ----------
            Row row1 = table.Rows.Add();

            // First cell – text
            Cell textCell = row1.Cells.Add();
            TextFragment tf = new TextFragment("Sample Text");
            tf.TextState.FontSize = 12;
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
            textCell.Paragraphs.Add(tf);

            // Second cell – high‑DPI image
            Cell imgCell1 = row1.Cells.Add();
            Image img1 = new Image
            {
                File = imagePath1,
                // Instruct Aspose.Pdf to respect the image's native resolution
                IsApplyResolution = true,
                // Scale factor >1 can be used to increase the effective DPI in the PDF
                ImageScale = 2.0
            };
            imgCell1.Paragraphs.Add(img1);

            // ---------- Second Row ----------
            Row row2 = table.Rows.Add();

            // First cell – another high‑DPI image
            Cell imgCell2 = row2.Cells.Add();
            Image img2 = new Image
            {
                File = imagePath2,
                IsApplyResolution = true,
                ImageScale = 2.0
            };
            imgCell2.Paragraphs.Add(img2);

            // Second cell – text
            Cell textCell2 = row2.Cells.Add();
            TextFragment tf2 = new TextFragment("More Text");
            tf2.TextState.FontSize = 12;
            tf2.TextState.ForegroundColor = Aspose.Pdf.Color.DarkBlue;
            textCell2.Paragraphs.Add(tf2);

            // Add the completed table to the page
            page.Paragraphs.Add(table);

            // Save the PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }
}