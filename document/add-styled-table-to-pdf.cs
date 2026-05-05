using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace
using Aspose.Pdf.Text;          // For text state if needed (not used here)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // Existing PDF to modify
        const string outputPath = "output_with_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document (1‑based page indexing)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Create a new table
            Table table = new Table();

            // Set overall table border (black, 1 point)
            table.Border = new BorderInfo(BorderSide.All, 1, Aspose.Pdf.Color.Black);

            // Set default cell border (black, 0.5 point)
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black);

            // Define column widths (example: three columns)
            table.ColumnWidths = "150 150 150";

            // Sample data: 6 rows, 3 columns
            int totalRows = 6;
            int totalCols = 3;

            for (int r = 0; r < totalRows; r++)
            {
                // Add a new row to the table
                Row row = table.Rows.Add();

                // Alternate row background colors
                if (r % 2 == 0)
                    row.BackgroundColor = Aspose.Pdf.Color.LightGray;   // Even rows
                else
                    row.BackgroundColor = Aspose.Pdf.Color.White;      // Odd rows

                // Add cells to the row
                for (int c = 0; c < totalCols; c++)
                {
                    string cellText = $"R{r + 1}C{c + 1}";
                    row.Cells.Add(cellText);
                }
            }

            // Position the table on the first page (optional: set Left/Top)
            // Here we let Aspose.Pdf place it automatically in the flow.
            doc.Pages[1].Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table added and saved to '{outputPath}'.");
    }
}