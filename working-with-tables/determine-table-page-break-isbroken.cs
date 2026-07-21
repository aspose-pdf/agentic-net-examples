using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a table with three columns
            Table table = new Table
            {
                ColumnWidths = "100 100 100"
            };

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");

            // Add multiple data rows to increase the chance of a page break
            for (int i = 0; i < 20; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add($"Item {i + 1}A");
                row.Cells.Add($"Item {i + 1}B");
                row.Cells.Add($"Item {i + 1}C");
            }

            // Add the table to the first page (pages are 1‑based)
            Page page = doc.Pages[1];
            page.Paragraphs.Add(table);

            // After layout, IsBroken indicates whether the table will be split across pages
            bool willBreak = table.IsBroken;
            Console.WriteLine($"Table.IsBroken = {willBreak}");

            // Save the modified document (optional)
            doc.Save("output.pdf");
        }
    }
}