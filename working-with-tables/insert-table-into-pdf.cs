using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Text;                // For text fragments if needed (optional)

class InsertTableExample
{
    static void Main()
    {
        // Paths for input and output PDF files
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the existing PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page where the table will be inserted (1‑based index)
            const int targetPageNumber = 1;
            if (targetPageNumber < 1 || targetPageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine("Invalid target page number.");
                return;
            }

            Page page = doc.Pages[targetPageNumber];

            // -------------------------------------------------
            // Construct the table
            // -------------------------------------------------
            Table table = new Table();

            // Position the table on the page (coordinates are in points)
            table.Left = 100f;   // distance from the left edge of the page
            table.Top  = 500f;   // distance from the bottom edge of the page

            // Optional visual settings (use float literals for widths)
            table.Border = new BorderInfo(BorderSide.All, 1f, Color.Black);
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Color.Gray);
            table.DefaultCellPadding = new MarginInfo(5f, 5f, 5f, 5f);
            table.DefaultCellTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 12f,
                ForegroundColor = Color.Black
            };

            // -------------------------------------------------
            // Populate the table with rows and cells
            // -------------------------------------------------
            // Header row
            Row header = table.Rows.Add();
            header.Cells.Add("Product");
            header.Cells.Add("Quantity");
            header.Cells.Add("Price");

            // Data rows
            Row row1 = table.Rows.Add();
            row1.Cells.Add("Widget A");
            row1.Cells.Add("10");
            row1.Cells.Add("$15.00");

            Row row2 = table.Rows.Add();
            row2.Cells.Add("Widget B");
            row2.Cells.Add("5");
            row2.Cells.Add("$25.00");

            // -------------------------------------------------
            // Add the table to the page's paragraph collection
            // -------------------------------------------------
            page.Paragraphs.Add(table);

            // Save the modified PDF (output format is PDF, no extra SaveOptions needed)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Table inserted and PDF saved to '{outputPdf}'.");
    }
}
