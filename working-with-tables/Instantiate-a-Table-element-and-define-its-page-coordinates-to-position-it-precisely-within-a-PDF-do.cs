using System;
using System.IO;
using Aspose.Pdf;   // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to host the table
            Page page = doc.Pages.Add();

            // Instantiate a Table (inherits BaseParagraph)
            Table table = new Table();

            // Define exact page coordinates (units are points; 1 inch = 72 points)
            table.Left = 100;   // X‑coordinate from the left edge
            table.Top  = 500;   // Y‑coordinate from the bottom edge

            // Optional: define column widths (three columns, each 100 points wide)
            table.ColumnWidths = "100 100 100";

            // Add a header row
            var headerRow = table.Rows.Add();
            headerRow.Cells.Add("Header 1");
            headerRow.Cells.Add("Header 2");
            headerRow.Cells.Add("Header 3");

            // Add a data row
            var dataRow = table.Rows.Add();
            dataRow.Cells.Add("Cell 1");
            dataRow.Cells.Add("Cell 2");
            dataRow.Cells.Add("Cell 3");

            // Place the table onto the page
            page.Paragraphs.Add(table);

            // Save the PDF (Document.Save without SaveOptions writes PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table added and saved to '{outputPath}'.");
    }
}