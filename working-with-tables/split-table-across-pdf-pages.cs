using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_split.pdf";

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Create a table that can split across pages
            Table table = new Table
            {
                // Enable automatic breaking of the table when it exceeds page height
                IsBroken = true,

                // Define column widths (percentage of the page width)
                ColumnWidths = "30% 40% 30%",

                // Optional: set border for visual clarity
                Border = new BorderInfo(BorderSide.All, 0.5f)
            };

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");
            // Make header bold
            foreach (Cell cell in header.Cells)
                cell.DefaultCellTextState = new TextState { FontStyle = FontStyles.Bold };

            // Add many data rows to force the table to span multiple pages
            for (int i = 1; i <= 100; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add($"Row {i} - Col 1");
                row.Cells.Add($"Row {i} - Col 2");
                row.Cells.Add($"Row {i} - Col 3");
            }

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Save the document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with split‑able table saved to '{outputPath}'.");
    }
}