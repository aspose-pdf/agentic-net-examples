using System;
using System.IO;
using Aspose.Pdf;

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

        // Load the existing PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Create a table with a single column (width can be adjusted as needed)
            Table table = new Table
            {
                ColumnWidths = "200" // width in points
            };

            // Add one row to the table
            Row row = table.Rows.Add();

            // Access the first cell of the row
            Cell cell = row.Cells[1];

            // Create an Image object that references the SVG file
            Image svgImage = new Image
            {
                File = svgImagePath
            };

            // Insert the SVG image into the cell
            cell.Paragraphs.Add(svgImage);

            // Add the table to the first page of the PDF
            doc.Pages[1].Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"SVG image inserted and PDF saved to '{outputPdfPath}'.");
    }
}