using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page to work with
            Page page = doc.Pages[1];

            // Create a table and configure required properties
            Table table = new Table
            {
                // Place the table on a new page
                IsInNewPage = true,
                // Auto‑fit column widths based on cell content
                ColumnAdjustment = ColumnAdjustment.AutoFitToContent
            };

            // Add a header row
            Row headerRow = table.Rows.Add();
            Cell h1 = headerRow.Cells.Add("Header 1");
            Cell h2 = headerRow.Cells.Add("Header 2");
            Cell h3 = headerRow.Cells.Add("Header 3");

            // Optional visual styling for header cells
            h1.BackgroundColor = Color.LightGray;
            h2.BackgroundColor = Color.LightGray;
            h3.BackgroundColor = Color.LightGray;

            // Add a data row
            Row dataRow = table.Rows.Add();
            dataRow.Cells.Add("Row 1, Col 1");
            dataRow.Cells.Add("Row 1, Col 2");
            dataRow.Cells.Add("Row 1, Col 3");

            // Insert the table into the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with table saved to '{outputPath}'.");
    }
}