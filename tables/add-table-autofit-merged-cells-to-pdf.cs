using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Use the first page (ensure it exists)
            Page page = doc.Pages[1];

            // Create a table and set column adjustment to automatically fit content
            Table table = new Table
            {
                ColumnAdjustment = ColumnAdjustment.AutoFitToContent,
                // Optional visual styling
                Border = new BorderInfo(BorderSide.All, 0.5f)
            };

            // ----- First row (e.g., headers) -----
            Row headerRow = table.Rows.Add();
            headerRow.Cells.Add("Header 1");
            headerRow.Cells.Add("Header 2");

            // ----- Second row (cells to be merged) -----
            Row dataRow = table.Rows.Add();
            // Add a cell that will span both columns
            Cell mergedCell = dataRow.Cells.Add("Merged Cell");
            mergedCell.ColSpan = 2; // Merge across two columns

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table added and saved to '{outputPath}'.");
    }
}