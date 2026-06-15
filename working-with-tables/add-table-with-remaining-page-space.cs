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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Get the rectangle that tightly encloses existing page content
            Aspose.Pdf.Rectangle contentRect = page.CalculateContentBBox();

            // Define custom margins (points). Adjust as needed.
            double topMargin    = 36; // 0.5 inch
            double bottomMargin = 36; // 0.5 inch

            // Usable page height = page height without margins minus our custom margins
            double usableHeight = page.PageInfo.PureHeight - topMargin - bottomMargin;

            // Height already occupied by existing content
            double occupiedHeight = contentRect.URY - contentRect.LLY;

            // Remaining vertical space for the new table
            double remainingHeight = usableHeight - occupiedHeight;

            // Create a simple table
            Table table = new Table();
            table.ColumnWidths = "200 200"; // two columns, each 200 points wide

            // Add a single row with two cells
            Row row = new Row();
            row.Cells.Add("Cell 1");
            row.Cells.Add("Cell 2");
            table.Rows.Add(row);

            // Determine how much height the table would need (optional)
            double tableHeight = table.GetHeight(page);

            // Place the table only if it fits in the remaining space
            if (tableHeight <= remainingHeight)
            {
                // Position the table just below the existing content,
                // respecting the bottom margin.
                table.Left = (float)bottomMargin;
                // In PDF coordinates Y grows upward; the top of the free area
                // starts at contentRect.LLY minus the bottom margin.
                table.Top = (float)(contentRect.LLY - bottomMargin);

                // Add the table to the page's paragraph collection
                page.Paragraphs.Add(table);
            }
            else
            {
                Console.WriteLine("Not enough space to add the table on this page.");
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}
