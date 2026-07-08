using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string svgPath = "image.svg";

        // Verify that the source PDF and SVG files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(svgPath))
        {
            Console.Error.WriteLine($"SVG file not found: {svgPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Create a table with a single column (width can be adjusted as needed)
            Table table = new Table
            {
                ColumnWidths = "200"
            };

            // Add one row to the table
            Row row = table.Rows.Add();

            // Add one cell to the row
            Cell cell = row.Cells.Add();

            // Create an Image object that points to the SVG file
            Image svgImage = new Image
            {
                File = svgPath
                // Optional: set explicit dimensions if required
                // Width = 150;
                // Height = 150;
            };

            // Insert the SVG image into the cell's paragraph collection
            cell.Paragraphs.Add(svgImage);

            // Add the table to the first page of the document
            doc.Pages[1].Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"SVG image inserted into table cell and saved to '{outputPdf}'.");
    }
}