using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";      // existing PDF (can be empty)
        const string outputPdfPath = "output.pdf";     // result PDF
        const string svgImagePath  = "image.svg";      // SVG file to embed

        if (!File.Exists(svgImagePath))
        {
            Console.Error.WriteLine($"SVG image not found: {svgImagePath}");
            return;
        }

        // Ensure the input PDF exists; if not, create a blank document.
        if (!File.Exists(inputPdfPath))
        {
            using (var blank = new Document())
            {
                blank.Pages.Add(); // add a single empty page
                blank.Save(inputPdfPath);
            }
        }

        // Open the PDF, add a table with one cell containing the SVG image.
        using (Document doc = new Document(inputPdfPath))
        {
            // Add a new page if the document has no pages.
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Use the first page for simplicity.
            Page page = doc.Pages[1];

            // Create a table (core API) and set its position.
            Table table = new Table
            {
                // Position the table at (50, 700) with a width of 500 points.
                // The Height will be auto‑calculated based on content.
                ColumnWidths = "500",
                Border = new BorderInfo(BorderSide.All, 0.5f),
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f)
            };
            table.ColumnWidths = "500"; // single column

            // Add a single row.
            Row row = table.Rows.Add();

            // Add a single cell.
            Cell cell = row.Cells.Add();

            // Create an Image object that reads the SVG file.
            // Aspose.Pdf.Image can handle SVG streams.
            Image svgImage = new Image
            {
                ImageStream = new FileStream(svgImagePath, FileMode.Open, FileAccess.Read)
            };

            // Add the image to the cell's paragraph collection.
            // The cell can contain any TextFragment‑derived objects; Image is acceptable.
            cell.Paragraphs.Add(svgImage);

            // Add the table to the page's paragraphs collection.
            page.Paragraphs.Add(table);

            // Save the modified PDF.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"SVG image inserted into table cell and saved to '{outputPdfPath}'.");
    }
}