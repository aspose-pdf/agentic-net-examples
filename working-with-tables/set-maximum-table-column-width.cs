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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page to work with
            Page page = doc.Pages.Count > 0 ? doc.Pages[1] : doc.Pages.Add();

            // Create a table with three columns
            Table table = new Table
            {
                // Set a default column width (in points). This acts as a maximum width
                // for any column that does not have an explicit width defined.
                DefaultColumnWidth = "120"
            };

            // Define explicit widths for each column to enforce a maximum width.
            // Values are in points; here each column is limited to 150 points.
            table.ColumnWidths = "150 150 150";

            // Add a row with sample data
            Row row = table.Rows.Add();
            row.Cells.Add("First column long text that should be limited by the column width.");
            row.Cells.Add("Second column text.");
            row.Cells.Add("Third column text.");

            // Insert the table into the page's content
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}