using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Ensure there is at least one page to work with
                Page page = doc.Pages.Count > 0 ? doc.Pages[1] : doc.Pages.Add();

                // Create a table and offset it from the left margin by 50 points
                Table table = new Table
                {
                    // Margin.Left sets the horizontal offset from the page's left edge
                    Margin = { Left = 50 }
                };

                // Define column widths (two columns, each 200 points wide)
                table.ColumnWidths = "200 200";

                // Add first row with two cells
                Row row1 = table.Rows.Add();
                row1.Cells.Add("Cell 1");
                row1.Cells.Add("Cell 2");

                // Add second row with two cells
                Row row2 = table.Rows.Add();
                row2.Cells.Add("Cell 3");
                row2.Cells.Add("Cell 4");

                // Insert the table into the page's content
                page.Paragraphs.Add(table);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Table added with left offset saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}