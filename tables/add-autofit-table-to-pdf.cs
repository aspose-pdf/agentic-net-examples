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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page to add the table to
            Page page = doc.Pages.Count > 0 ? doc.Pages[1] : doc.Pages.Add();

            // Create a table and set column adjustment to AutoFitToContent
            Table table = new Table
            {
                ColumnAdjustment = ColumnAdjustment.AutoFitToContent,
                Border = new BorderInfo(BorderSide.All, 0.5f) // optional visual border
            };

            // Header row
            Row header = table.Rows.Add();
            header.Cells.Add("Product");
            header.Cells.Add("Quantity");
            header.Cells.Add("Price");

            // Sample data rows
            Row row1 = table.Rows.Add();
            row1.Cells.Add("Apple");
            row1.Cells.Add("10");
            row1.Cells.Add("$1.00");

            Row row2 = table.Rows.Add();
            row2.Cells.Add("Banana");
            row2.Cells.Add("5");
            row2.Cells.Add("$0.50");

            // Add the table to the page's content
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table added and saved to '{outputPath}'.");
    }
}