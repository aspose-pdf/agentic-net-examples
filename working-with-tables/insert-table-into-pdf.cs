using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class InsertTableExample
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Page number where the table will be inserted (1‑based indexing)
        const int targetPageNumber = 2;

        // Table position on the page (coordinates in points)
        const float tableLeft = 100f;   // distance from the left edge (float, not double)
        const float tableTop  = 500f;   // distance from the bottom edge (float, not double)

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Validate the target page number
            if (targetPageNumber < 1 || targetPageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Invalid page number: {targetPageNumber}");
                return;
            }

            // Get the target page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[targetPageNumber];

            // -------------------------------------------------
            // Construct a simple table with 3 columns and 2 rows
            // -------------------------------------------------
            Table table = new Table
            {
                // Position the table on the page (float values)
                Left = tableLeft,
                Top  = tableTop,

                // Optional visual styling
                Border = new BorderInfo(BorderSide.All, 1, Aspose.Pdf.Color.Black),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // Define column widths (optional)
            table.ColumnWidths = "100 150 200";

            // First row (header)
            Row headerRow = table.Rows.Add();
            Cell headerCell1 = headerRow.Cells.Add("Header 1");
            Cell headerCell2 = headerRow.Cells.Add("Header 2");
            Cell headerCell3 = headerRow.Cells.Add("Header 3");

            // Apply bold style to header cells
            TextState headerStyle = new TextState
            {
                FontSize = 12,
                Font = FontRepository.FindFont("Helvetica-Bold"),
                ForegroundColor = Aspose.Pdf.Color.White
            };
            headerCell1.DefaultCellTextState = headerStyle;
            headerCell2.DefaultCellTextState = headerStyle;
            headerCell3.DefaultCellTextState = headerStyle;
            headerRow.BackgroundColor = Aspose.Pdf.Color.DarkGray;

            // Second row (data)
            Row dataRow = table.Rows.Add();
            dataRow.Cells.Add("Row 1, Col 1");
            dataRow.Cells.Add("Row 1, Col 2");
            dataRow.Cells.Add("Row 1, Col 3");

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Table inserted on page {targetPageNumber} and saved to '{outputPdf}'.");
    }
}
