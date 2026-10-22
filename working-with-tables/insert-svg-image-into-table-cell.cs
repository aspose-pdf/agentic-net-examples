using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for TextFragment

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_table.pdf";
        const string svgPath   = "image.svg";

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(svgPath))
        {
            Console.Error.WriteLine($"SVG image not found: {svgPath}");
            return;
        }

        // Open the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Add a new page to host the table (could also use an existing page)
            Page page = doc.Pages.Add();

            // Create a table and configure basic appearance
            Table table = new Table
            {
                Border = new BorderInfo(BorderSide.All, 0.5f), // thin border on all sides
                ColumnWidths = "200 200" // two equal columns, width in points
            };

            // Add a row to the table
            Row row = table.Rows.Add();

            // First cell: simple text
            Cell textCell = row.Cells.Add();
            textCell.Paragraphs.Add(new TextFragment("Sample text"));

            // Second cell: SVG image
            Cell imageCell = row.Cells.Add();

            // Load the SVG file into an Image object
            Image img = new Image();
            using (FileStream fs = new FileStream(svgPath, FileMode.Open, FileAccess.Read))
            {
                img.ImageStream = fs; // Image will read the SVG from the stream
            }
            // Optional: set explicit dimensions for the SVG rendering
            img.FixWidth  = 150; // width in points
            img.FixHeight = 150; // height in points

            // Insert the image into the cell
            imageCell.Paragraphs.Add(img);

            // Add the completed table to the page
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with SVG image in table cell saved to '{outputPdf}'.");
    }
}