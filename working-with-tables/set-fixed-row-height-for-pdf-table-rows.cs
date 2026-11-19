using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_fixed_row_height.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page to host the table
            Page page = doc.Pages.Add();

            // Instantiate a table
            Table table = new Table();

            // Optional: define column widths (three equal columns)
            table.ColumnWidths = "100 100 100";

            // Populate the table with rows and cells
            for (int i = 0; i < 5; i++) // 5 rows
            {
                Row row = table.Rows.Add(); // Add a new row

                for (int j = 0; j < 3; j++) // 3 columns per row
                {
                    // Create a text fragment for the cell content
                    TextFragment tf = new TextFragment($"R{i + 1}C{j + 1}");
                    // Add the cell containing the text fragment
                    row.Cells.Add(tf);
                }
            }

            // Apply a fixed height to every row (float value)
            foreach (Row r in table.Rows)
            {
                r.FixedRowHeight = 20F; // 20 points high for each row
            }

            // Add the completed table to the page's content
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
