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
        const int targetPage = 2; // 1‑based page number

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Verify that the requested page exists
            if (targetPage < 1 || targetPage > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {targetPage} is out of range (1‑{doc.Pages.Count}).");
                return;
            }

            // Retrieve the target page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[targetPage];

            // Create a simple 2‑column, 3‑row table
            Table table = new Table
            {
                // Position the table on the page (coordinates are in points)
                Left = 50,
                Top = 700,
                // Optional visual styling
                Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black),
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Gray),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // Define column widths as percentages (50% each)
            table.ColumnWidths = "50 50";

            // Populate the table with rows and cells
            for (int r = 0; r < 3; r++)
            {
                Row row = table.Rows.Add();
                for (int c = 0; c < 2; c++)
                {
                    // Each cell contains a TextFragment
                    TextFragment tf = new TextFragment($"R{r + 1}C{c + 1}");
                    tf.TextState.FontSize = 12;
                    tf.TextState.Font = FontRepository.FindFont("Helvetica");
                    tf.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

                    // Add the fragment to the cell
                    row.Cells.Add(tf);
                }
            }

            // Insert the table into the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table added to page {targetPage}. Saved as '{outputPath}'.");
    }
}
