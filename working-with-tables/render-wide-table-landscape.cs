using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_landscape.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page
            Page page = doc.Pages[1];

            // Switch the page orientation to landscape
            page.PageInfo.IsLandscape = true;

            // Swap width and height so the page size matches the new orientation
            double originalWidth  = page.PageInfo.Width;
            double originalHeight = page.PageInfo.Height;
            page.SetPageSize(originalHeight, originalWidth); // width, height

            // Create a wide table that will fit the landscape page
            Table table = new Table();

            // Define column widths (example: five equal columns)
            table.ColumnWidths = "100 100 100 100 100";

            // Optional styling for cells
            table.DefaultCellBorder  = new BorderInfo(BorderSide.All, 0.5f);
            table.DefaultCellPadding = new MarginInfo(5, 5, 5, 5);

            // Add a header row with five cells
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");
            header.Cells.Add("Header 4");
            header.Cells.Add("Header 5");

            // Add a data row as an example
            Row data = table.Rows.Add();
            data.Cells.Add("Cell A1");
            data.Cells.Add("Cell A2");
            data.Cells.Add("Cell A3");
            data.Cells.Add("Cell A4");
            data.Cells.Add("Cell A5");

            // Insert the table into the page
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Landscape PDF with table saved to '{outputPath}'.");
    }
}