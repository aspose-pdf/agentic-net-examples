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

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create a table with two columns
            Table table = new Table
            {
                ColumnWidths = "200 200", // equal column widths
                Border = new BorderInfo(BorderSide.All, 0.5f) // thin border around cells
            };

            // First row (regular)
            Row row1 = table.Rows.Add();
            row1.Cells.Add("Header 1");
            row1.Cells.Add("Header 2");
            row1.DefaultCellTextState.FontSize = 12;
            row1.DefaultCellTextState.FontStyle = FontStyles.Bold;

            // Second row – start on a new page
            Row row2 = table.Rows.Add();
            row2.IsInNewPage = true; // forces a page break before this row
            row2.Cells.Add("Data A");
            row2.Cells.Add("Data B");
            row2.DefaultCellTextState.FontSize = 12;

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
