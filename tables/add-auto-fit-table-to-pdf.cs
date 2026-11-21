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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page to add the table to
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create a new Table object
            Table table = new Table
            {
                // Set column adjustment to automatically fit the table to the page width
                ColumnAdjustment = ColumnAdjustment.AutoFitToWindow,
                // Optional: set border for visual clarity
                Border = new BorderInfo(BorderSide.All, 0.5f)
            };

            // Define column widths (optional, can be omitted for auto‑fit)
            // table.ColumnWidths = "100 200 100";

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add(new TextFragment("Header 1"));
            header.Cells.Add(new TextFragment("Header 2"));
            header.Cells.Add(new TextFragment("Header 3"));
            // Make header cells bold
            foreach (Cell cell in header.Cells)
                cell.DefaultCellTextState.FontStyle = FontStyles.Bold;

            // Add a few data rows
            for (int i = 1; i <= 5; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add(new TextFragment($"Row {i} Col 1"));
                row.Cells.Add(new TextFragment($"Row {i} Col 2"));
                row.Cells.Add(new TextFragment($"Row {i} Col 3"));
            }

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Save the modified PDF (still inside the using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table added and PDF saved to '{outputPath}'.");
    }
}