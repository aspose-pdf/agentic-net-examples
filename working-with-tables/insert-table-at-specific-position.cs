using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // needed for TextState, FontRepository, MarginInfo

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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a new table (use Aspose.Pdf.Table, not the one from Aspose.Pdf.Text)
            Table table = new Table();

            // Set absolute positioning using MarginInfo (Table inherits from Paragraph)
            // Left = 100 points, Bottom = 500 points from the bottom of the page
            table.Margin = new MarginInfo(100, 0, 0, 500);

            // Define column widths (optional)
            table.ColumnWidths = "200 200";

            // Add a header row
            Row header = table.Rows.Add();
            Cell cell1 = header.Cells.Add("Header 1");
            Cell cell2 = header.Cells.Add("Header 2");
            // Apply simple styling to header cells
            cell1.DefaultCellTextState = new TextState { FontSize = 12, Font = FontRepository.FindFont("Helvetica-Bold") };
            cell2.DefaultCellTextState = new TextState { FontSize = 12, Font = FontRepository.FindFont("Helvetica-Bold") };

            // Add a data row
            Row dataRow = table.Rows.Add();
            dataRow.Cells.Add("Value A");
            dataRow.Cells.Add("Value B");

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table inserted and saved to '{outputPath}'.");
    }
}
