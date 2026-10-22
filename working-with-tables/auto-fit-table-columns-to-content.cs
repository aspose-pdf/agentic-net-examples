using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

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

        try
        {
            // Load the source PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Ensure there is at least one page to place the table
                Page page = doc.Pages[1]; // 1‑based indexing

                // Create a table and define column widths (string format required)
                Table table = new Table
                {
                    ColumnWidths = "150 150 150"
                };

                // Add a header row
                Row header = table.Rows.Add();
                header.Cells.Add("Product");
                header.Cells.Add("Quantity");
                header.Cells.Add("Price");

                // Add a data row
                Row data = table.Rows.Add();
                data.Cells.Add("Widget A");
                data.Cells.Add("10");
                data.Cells.Add("$25.00");

                // Add another data row
                Row data2 = table.Rows.Add();
                data2.Cells.Add("Gadget B");
                data2.Cells.Add("5");
                data2.Cells.Add("$40.00");

                // Auto‑fit columns to the content
                table.ColumnAdjustment = ColumnAdjustment.AutoFitToContent;

                // Insert the table into the page's paragraph collection
                page.Paragraphs.Add(table);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Table with AutoFitBehavior saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
