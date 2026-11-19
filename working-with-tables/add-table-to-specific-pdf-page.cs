using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // Added for FontStyles enum

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_table.pdf";
        const int    pageNumber = 2; // 1‑based page index

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Validate page number (Aspose.Pdf uses 1‑based indexing)
            if (pageNumber < 1 || pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Invalid page number {pageNumber}. Document has {doc.Pages.Count} pages.");
                return;
            }

            // Access the target page
            Page targetPage = doc.Pages[pageNumber];

            // Create a visual table
            Table table = new Table
            {
                // Define column widths (percentage of page width)
                ColumnWidths = "30% 40% 30%"
            };

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("Product");
            header.Cells.Add("Description");
            header.Cells.Add("Price");
            // Optional: style header cells
            foreach (Cell cell in header.Cells)
            {
                cell.BackgroundColor = Color.LightGray;
                cell.Border = new BorderInfo(BorderSide.All, 0.5f);
                cell.DefaultCellTextState.FontSize = 12;
                cell.DefaultCellTextState.FontStyle = FontStyles.Bold; // Fixed: FontStyles now resolved
            }

            // Add a few data rows
            Row row1 = table.Rows.Add();
            row1.Cells.Add("Widget A");
            row1.Cells.Add("Standard widget");
            row1.Cells.Add("$10.00");

            Row row2 = table.Rows.Add();
            row2.Cells.Add("Widget B");
            row2.Cells.Add("Advanced widget");
            row2.Cells.Add("$20.00");

            // Insert the table into the page's paragraph collection
            targetPage.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table added to page {pageNumber}. Saved as '{outputPath}'.");
    }
}
