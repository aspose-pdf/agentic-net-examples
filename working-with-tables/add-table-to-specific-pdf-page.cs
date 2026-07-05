using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // Required for TextFragment if needed

class AddTableToPage
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_table.pdf";

        // The page number (1‑based) where the table will be inserted
        const int targetPageNumber = 2;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document inside a using block (ensures disposal)
        using (Document doc = new Document(inputPath))
        {
            // Validate the requested page exists
            if (targetPageNumber < 1 || targetPageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {targetPageNumber} is out of range. Document has {doc.Pages.Count} pages.");
                return;
            }

            // Retrieve the target page
            Page page = doc.Pages[targetPageNumber];

            // -------------------------------------------------
            // Create a simple table
            // -------------------------------------------------
            Table table = new Table();

            // Define column widths (comma‑separated or space‑separated string)
            // Example: two columns, each 200 points wide
            table.ColumnWidths = "200 200";

            // Optional: set table border and background
            table.Border = new BorderInfo(BorderSide.All, 1f, Color.Black);
            table.BackgroundColor = Color.LightGray;

            // Add header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            // Make header text bold
            foreach (Cell cell in header.Cells)
            {
                cell.DefaultCellTextState = new TextState
                {
                    Font = FontRepository.FindFont("Helvetica-Bold"),
                    FontSize = 12,
                    ForegroundColor = Color.Black
                };
            }

            // Add a data row
            Row data = table.Rows.Add();
            data.Cells.Add("Value A");
            data.Cells.Add("Value B");
            // Set regular text style for data cells
            foreach (Cell cell in data.Cells)
            {
                cell.DefaultCellTextState = new TextState
                {
                    Font = FontRepository.FindFont("Helvetica"),
                    FontSize = 11,
                    ForegroundColor = Color.Black
                };
            }

            // -------------------------------------------------
            // Insert the table into the page's paragraph collection
            // -------------------------------------------------
            page.Paragraphs.Add(table);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table added to page {targetPageNumber}. Saved as '{outputPath}'.");
    }
}