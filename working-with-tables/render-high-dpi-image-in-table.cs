using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPdf = "table_highdpi.pdf";
        const string imageFile = "highres_image.jpg";

        if (!File.Exists(imageFile))
        {
            Console.Error.WriteLine($"Image file not found: {imageFile}");
            return;
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with two columns
            Aspose.Pdf.Table table = new Aspose.Pdf.Table
            {
                // Define column widths (in points)
                ColumnWidths = "200 200",

                // Optional styling for cells
                DefaultCellBorder = new BorderInfo(BorderSide.All, 1f, Color.Black),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // ----- Header row -----
            Row headerRow = table.Rows.Add();
            headerRow.Cells.Add("Product");
            headerRow.Cells.Add("Image");

            // ----- Data row -----
            Row dataRow = table.Rows.Add();

            // Text cell
            dataRow.Cells.Add("Sample Item");

            // Image cell
            Aspose.Pdf.Image img = new Aspose.Pdf.Image
            {
                // Load the image file
                File = imageFile,

                // Enable high‑DPI handling
                IsApplyResolution = true,

                // Scale the image (e.g., 2×) to increase effective DPI
                ImageScale = 2.0
            };

            // Add the image to the second cell's paragraph collection
            dataRow.Cells[2].Paragraphs.Add(img);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with high‑DPI table saved to '{outputPdf}'.");
    }
}