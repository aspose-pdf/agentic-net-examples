using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // Added for TextState and FontRepository

class InsertTableExample
{
    static void Main()
    {
        // Input PDF path, output PDF path, and target page number (1‑based)
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const int    targetPage = 2;          // Insert table on page 2

        // Table position (coordinates in points) and size
        const double tableLeft = 100;   // X coordinate of left side
        const double tableTop  = 500;   // Y coordinate of top side

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the requested page exists
            if (targetPage < 1 || targetPage > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {targetPage} is out of range. Document has {doc.Pages.Count} pages.");
                return;
            }

            // Create a new Table instance
            Table table = new Table();

            // Position the table on the page (cast double to float as required by the API)
            table.Left = (float)tableLeft;
            table.Top  = (float)tableTop;

            // Optional: set table appearance
            table.Border = new BorderInfo(BorderSide.All, 1, Aspose.Pdf.Color.Black);
            table.DefaultCellPadding = new MarginInfo(5, 5, 5, 5);
            table.DefaultCellTextState = new TextState { FontSize = 12, Font = FontRepository.FindFont("Helvetica") };

            // Add some rows and cells
            // Row 1 (header)
            Row headerRow = table.Rows.Add();
            Cell headerCell1 = headerRow.Cells.Add("Product");
            Cell headerCell2 = headerRow.Cells.Add("Price");
            // Apply bold style to header cells
            headerCell1.DefaultCellTextState = new TextState { FontSize = 12, Font = FontRepository.FindFont("Helvetica-Bold") };
            headerCell2.DefaultCellTextState = new TextState { FontSize = 12, Font = FontRepository.FindFont("Helvetica-Bold") };

            // Row 2 (data)
            Row dataRow = table.Rows.Add();
            dataRow.Cells.Add("Widget A");
            dataRow.Cells.Add("$25.00");

            // Row 3 (data)
            Row dataRow2 = table.Rows.Add();
            dataRow2.Cells.Add("Widget B");
            dataRow2.Cells.Add("$30.00");

            // Retrieve the target page and add the table to its paragraphs collection
            Page page = doc.Pages[targetPage];
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table inserted on page {targetPage} and saved to '{outputPath}'.");
    }
}
