using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "rotated_table.pdf";

        // Ensure the output directory exists
        string outDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outDir) && !Directory.Exists(outDir))
            Directory.CreateDirectory(outDir);

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Rotate the whole page by 90 degrees using the correct Rotation enum
            page.Rotate = Rotation.on90;

            // Create a table with 3 columns
            Table table = new Table
            {
                ColumnWidths = "100 100 100", // three equal columns
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f)
            };

            // Populate the table with sample data (2 rows)
            for (int r = 0; r < 2; r++)
            {
                Row row = table.Rows.Add();
                for (int c = 0; c < 3; c++)
                {
                    Cell cell = row.Cells.Add();
                    // Add a simple text fragment to each cell
                    TextFragment tf = new TextFragment($"R{r + 1}C{c + 1}");
                    tf.TextState.FontSize = 12;
                    cell.Paragraphs.Add(tf);
                }
            }

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with rotated table saved to '{outputPath}'.");
    }
}
