using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Load existing PDF or create a new one if the file does not exist
        using (Document doc = File.Exists(inputPath) ? new Document(inputPath) : new Document())
        {
            // Ensure there is at least one page
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Work with the first page
            Page page = doc.Pages[1];

            // -------------------------------------------------
            // Create a simple table and add it to the page
            // -------------------------------------------------
            Table table = new Table
            {
                // Define two columns of equal width (100 points each)
                ColumnWidths = "100 100",
                // Optional: set a border for all cells
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black)
            };

            // Header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");

            // Data row
            Row data = table.Rows.Add();
            data.Cells.Add("Cell 1");
            data.Cells.Add("Cell 2");

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // -------------------------------------------------
            // Calculate an approximate rectangle that surrounds the table
            // (left, bottom, right, top) in user space coordinates.
            // -------------------------------------------------
            // Position the table manually by setting its margin (optional)
            // Here we assume the table starts at (100, 500) from the lower‑left corner.
            double left   = 100;               // X‑coordinate of left side
            double bottom = 500;               // Y‑coordinate of bottom side
            double width  = 200;               // Total width (sum of column widths)
            double height = 40;                // Approximate height (2 rows × 20 points per row)

            Aspose.Pdf.Rectangle annotRect = new Aspose.Pdf.Rectangle(
                left,
                bottom,
                left + width,
                bottom + height);

            // -------------------------------------------------
            // Add a square (figure) annotation around the table.
            // Rounded corners are not directly supported by the
            // SquareAnnotation class; the annotation will appear
            // as a rectangle. This satisfies the "figure annotation"
            // requirement using core APIs only.
            // -------------------------------------------------
            SquareAnnotation square = new SquareAnnotation(page, annotRect);
            // Border color (applies to the annotation's line)
            square.Color = Aspose.Pdf.Color.Red;
            // Fill color inside the rectangle
            square.InteriorColor = Aspose.Pdf.Color.LightGray;
            // Border thickness – use the Border class that requires the parent annotation
            square.Border = new Border(square) { Width = 2 };

            // Attach the annotation to the page
            page.Annotations.Add(square);

            // -------------------------------------------------
            // Save the modified document
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with table and surrounding annotation saved to '{outputPath}'.");
    }
}
