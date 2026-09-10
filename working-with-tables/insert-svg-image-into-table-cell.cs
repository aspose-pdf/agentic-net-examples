using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Input SVG file path and output PDF path
        const string svgPath   = "example.svg";
        const string outputPdf = "output.pdf";

        if (!File.Exists(svgPath))
        {
            Console.Error.WriteLine($"SVG file not found: {svgPath}");
            return;
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with a single column (adjust width as needed)
            Table table = new Table
            {
                ColumnWidths = "200" // width of the column in points
            };

            // Add a row to the table
            Row row = table.Rows.Add();

            // Add a cell (TableCell) to the row
            Cell cell = row.Cells.Add();

            // Create an Image object for the SVG file
            Image svgImage = new Image
            {
                // Set the file path of the SVG image
                File = svgPath,

                // Optional: preserve aspect ratio and fit within the cell
                // The Image will be scaled to fit the cell bounds while keeping its proportions
                // You can also set Width/Height if you need explicit sizing
                // Width = 180,
                // Height = 120
            };

            // Add the image to the cell's paragraph collection
            cell.Paragraphs.Add(svgImage);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with SVG image saved to '{outputPdf}'.");
    }
}