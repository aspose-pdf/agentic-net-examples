using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string svgImagePath  = "image.svg";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(svgImagePath))
        {
            Console.Error.WriteLine($"SVG image not found: {svgImagePath}");
            return;
        }

        // Load the existing PDF (lifecycle rule: use using)
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure there is at least one page to place the table
            Page page = doc.Pages[1];

            // Create a table with a single column
            Table table = new Table
            {
                // Set column width (adjust as needed)
                ColumnWidths = "300"
            };

            // Add a row to the table
            Row row = table.Rows.Add();

            // Add a cell to the row
            Cell cell = row.Cells.Add();

            // Insert the SVG image into the cell using the Image class
            // (ImageFragment does not exist, so we use Image)
            Image svgImage = new Image
            {
                // Path to the SVG file
                File = svgImagePath
            };

            // Optionally set image dimensions (in points)
            // svgImage.FixWidth = 200;
            // svgImage.FixHeight = 150;

            // Add the image to the cell's paragraph collection
            cell.Paragraphs.Add(svgImage);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the modified PDF (lifecycle rule: use using)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with SVG image in table cell: {outputPdfPath}");
    }
}