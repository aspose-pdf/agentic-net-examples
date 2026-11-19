using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF (document‑disposal‑with‑using rule)
            using (Document doc = new Document(inputPath))
            {
                // Access the first page (page‑indexing‑one‑based rule)
                Page page = doc.Pages[1];

                // Create a table and add it to the page
                Table table = new Table
                {
                    ColumnWidths = "200" // single column width
                };
                page.Paragraphs.Add(table);

                // Add a row to the table
                Row row = table.Rows.Add();

                // Add a cell with some text
                Cell cell = row.Cells.Add("Sample text");

                // Set vertical alignment of the cell content to Middle (correct enum member is Center)
                cell.VerticalAlignment = VerticalAlignment.Center;

                // Save the modified PDF (save‑to‑non‑pdf‑always‑use‑save‑options not needed for PDF)
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
