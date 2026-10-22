using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "TableInParagraph.pdf";

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // -------------------------------------------------
            // 1. Add a paragraph with some introductory text
            // -------------------------------------------------
            TextFragment intro = new TextFragment("Below is a table inserted after this paragraph:");
            // Optional: set some formatting
            intro.TextState.FontSize = 12;
            intro.TextState.Font = FontRepository.FindFont("Arial");
            page.Paragraphs.Add(intro);

            // -------------------------------------------------
            // 2. Create a table and populate it
            // -------------------------------------------------
            Table table = new Table
            {
                // Define column widths (percentage of the page width)
                ColumnWidths = "200 200"
            };

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            // Header formatting
            foreach (Cell cell in header.Cells)
            {
                cell.BackgroundColor = Aspose.Pdf.Color.LightGray;
                cell.DefaultCellTextState.FontSize = 12;
                cell.DefaultCellTextState.Font = FontRepository.FindFont("Arial");
                cell.DefaultCellTextState.FontStyle = FontStyles.Bold;
                cell.Margin = new MarginInfo(5, 5, 5, 5);
            }

            // Add a few data rows
            for (int i = 1; i <= 3; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add($"Row {i} - Col 1");
                row.Cells.Add($"Row {i} - Col 2");
                foreach (Cell cell in row.Cells)
                {
                    cell.DefaultCellTextState.FontSize = 11;
                    cell.DefaultCellTextState.Font = FontRepository.FindFont("Arial");
                    cell.Margin = new MarginInfo(5, 5, 5, 5);
                }
            }

            // -------------------------------------------------
            // 3. Insert the table after the paragraph
            // -------------------------------------------------
            page.Paragraphs.Add(table);

            // Save the document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}