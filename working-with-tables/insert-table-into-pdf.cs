using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input parameters
        const string inputPdfPath  = "input.pdf";          // existing PDF
        const string outputPdfPath = "output.pdf";         // result PDF
        const int    targetPage    = 2;                    // 1‑based page number where the table will be placed
        const double posX          = 100;                  // X coordinate (points) of the table's lower‑left corner
        const double posY          = 500;                  // Y coordinate (points) of the table's lower‑left corner

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Open the PDF inside a using block (deterministic disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Validate requested page number
            if (targetPage < 1 || targetPage > pdfDoc.Pages.Count)
            {
                Console.Error.WriteLine($"Invalid page number {targetPage}. Document has {pdfDoc.Pages.Count} pages.");
                return;
            }

            // Retrieve the target page (1‑based indexing)
            Page page = pdfDoc.Pages[targetPage];

            // -------------------------------------------------
            // Construct a visual Table (Aspose.Pdf.Table)
            // -------------------------------------------------
            Table table = new Table
            {
                // Optional visual styling
                Border = new BorderInfo(BorderSide.All, 0.5f),
                Margin = new MarginInfo { Top = 0, Bottom = 0, Left = 0, Right = 0 },
                // Define column widths (relative percentages)
                ColumnWidths = "50 50"
            };

            // First row – header
            Row headerRow = table.Rows.Add();
            Cell headerCell1 = headerRow.Cells.Add("Product");
            Cell headerCell2 = headerRow.Cells.Add("Price");
            // Header styling – use DefaultCellTextState instead of the non‑existent TextInfo
            headerCell1.BackgroundColor = Aspose.Pdf.Color.LightGray;
            headerCell2.BackgroundColor = Aspose.Pdf.Color.LightGray;
            headerCell1.DefaultCellTextState = new TextState
            {
                FontSize = 12,
                FontStyle = FontStyles.Bold
            };
            headerCell2.DefaultCellTextState = new TextState
            {
                FontSize = 12,
                FontStyle = FontStyles.Bold
            };

            // Second row – data
            Row dataRow = table.Rows.Add();
            dataRow.Cells.Add("Widget A");
            dataRow.Cells.Add("$25.00");

            // Third row – data
            Row dataRow2 = table.Rows.Add();
            dataRow2.Cells.Add("Widget B");
            dataRow2.Cells.Add("$40.00");

            // Position the table on the page.
            // Table uses Left and Top properties (lower‑left corner coordinates).
            table.Left = (float)posX;
            table.Top  = (float)posY;

            // Add the table to the page's paragraph collection.
            page.Paragraphs.Add(table);

            // Save the modified document.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Table inserted on page {targetPage} and saved to '{outputPdfPath}'.");
    }
}
