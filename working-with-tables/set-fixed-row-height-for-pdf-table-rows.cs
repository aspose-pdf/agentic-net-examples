using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF
            using (Document doc = new Document(inputPath))
            {
                // Ensure there is at least one page to host the table
                Page page = doc.Pages.Count > 0 ? doc.Pages[1] : doc.Pages.Add();

                // Create a visual table (not a tagged structure table)
                Table table = new Table
                {
                    // Define two equal columns (widths in points)
                    ColumnWidths = "200 200",
                    // Optional: set a default border for all cells
                    DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5F, Color.Black)
                };

                // ----- Header row -----
                Row header = table.Rows.Add();
                header.Cells.Add("Column A");
                header.Cells.Add("Column B");
                header.BackgroundColor = Color.LightGray;
                // Fixed height for the header row (points)
                header.FixedRowHeight = 20F;

                // ----- Data rows -----
                for (int i = 1; i <= 5; i++)
                {
                    Row dataRow = table.Rows.Add();
                    dataRow.Cells.Add($"Row {i} - A");
                    dataRow.Cells.Add($"Row {i} - B");
                    // Apply the same fixed height to every data row
                    dataRow.FixedRowHeight = 20F;
                }

                // Add the table to the page's paragraph collection
                page.Paragraphs.Add(table);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF with table saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
