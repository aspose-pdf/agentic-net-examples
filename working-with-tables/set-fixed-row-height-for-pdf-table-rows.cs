using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string outputPath = "table_fixed_row_height.pdf";

        // Fixed height (in points) to be applied to every row
        const double fixedRowHeight = 30.0;

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and set its position on the page
            Table table = new Table
            {
                // Position the table (left, top)
                // Coordinates are measured from the bottom-left corner of the page
                // Adjust as needed for your layout
                // Here we place the table 50 points from the left and 700 points from the bottom
                // (i.e., near the top of a standard A4 page)
                // Note: Table does not have a DefaultRowHeight property, so we set the height per row.
                // The FixedRowHeight property of each Row will enforce the desired height.
                // We'll apply this setting when rows are created.
                // No additional table properties are required for this example.
            };

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Example: create 5 rows with 3 columns each
            for (int i = 0; i < 5; i++)
            {
                // Add a new row to the table
                Row row = table.Rows.Add();

                // Set the fixed row height before adding cells
                row.FixedRowHeight = fixedRowHeight;

                // Add three cells to the row
                for (int j = 0; j < 3; j++)
                {
                    // Create a text fragment for the cell content
                    TextFragment tf = new TextFragment($"R{i + 1}C{j + 1}");
                    // Add the cell to the row
                    row.Cells.Add(tf);
                }
            }

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with fixed row height saved to '{outputPath}'.");
    }
}