using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for Color if needed

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that the document has at least ten pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 10)
            {
                Console.Error.WriteLine("The document contains fewer than 10 pages.");
                return;
            }

            // Create a new Table object
            Table table = new Table();

            // Set column widths (example: three columns with specific widths)
            table.ColumnWidths = "100 150 200";

            // Define a default border for all cells
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Color.Black);

            // Add header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");

            // Add first data row
            Row row1 = table.Rows.Add();
            row1.Cells.Add("Data A1");
            row1.Cells.Add("Data A2");
            row1.Cells.Add("Data A3");

            // Add second data row
            Row row2 = table.Rows.Add();
            row2.Cells.Add("Data B1");
            row2.Cells.Add("Data B2");
            row2.Cells.Add("Data B3");

            // Replace the content of page 10 with the new table
            Page pageTen = doc.Pages[10];
            pageTen.Paragraphs.Clear();          // Remove any existing paragraphs/elements
            pageTen.Paragraphs.Add(table);       // Insert the newly created table

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}