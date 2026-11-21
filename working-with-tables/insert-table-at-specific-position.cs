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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the table will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Create a new table
            Table table = new Table();

            // Position the table using Left and Top properties (absolute coordinates)
            // X = 100 points from the left edge, Y = 500 points from the bottom edge of the page
            table.Left = 100; // X coordinate
            table.Top = 500;  // Y coordinate

            // Define column widths (space‑separated values)
            table.ColumnWidths = "150 150";

            // Add a header row
            Row headerRow = table.Rows.Add();
            Cell headerCell1 = headerRow.Cells.Add("Header 1");
            Cell headerCell2 = headerRow.Cells.Add("Header 2");

            // Apply simple formatting to header cells
            headerCell1.BackgroundColor = Color.LightGray;
            headerCell2.BackgroundColor = Color.LightGray;

            // Add a data row
            Row dataRow = table.Rows.Add();
            dataRow.Cells.Add("Data A");
            dataRow.Cells.Add("Data B");

            // Insert the table into the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table inserted and saved to '{outputPath}'.");
    }
}
