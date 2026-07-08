using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class InsertTableExample
{
    static void Main()
    {
        // Input PDF path, output PDF path and the page where the table will be placed
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const int    targetPageNumber = 2;          // 1‑based page index
        const float  tableLeft  = 100f;             // X coordinate (points) – float required by Table.Left
        const float  tableTop   = 600f;             // Y coordinate (points) – float required by Table.Top

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the requested page exists
            if (targetPageNumber < 1 || targetPageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {targetPageNumber} is out of range.");
                return;
            }

            Page page = doc.Pages[targetPageNumber];

            // -------------------------------------------------
            // Build a simple 3‑column, 2‑row table
            // -------------------------------------------------
            Table table = new Table
            {
                // Position the table on the page (float values)
                Left = tableLeft,
                Top  = tableTop,

                // Optional visual styling
                Border = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black),
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Gray),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // Define column widths (relative percentages of the table width)
            table.ColumnWidths = "33 33 34";

            // Row 1 – header cells
            Row headerRow = table.Rows.Add();
            headerRow.Cells.Add(CreateCell("Product"));
            headerRow.Cells.Add(CreateCell("Quantity"));
            headerRow.Cells.Add(CreateCell("Price"));

            // Row 2 – data cells
            Row dataRow = table.Rows.Add();
            dataRow.Cells.Add(CreateCell("Widget A"));
            dataRow.Cells.Add(CreateCell("15"));
            dataRow.Cells.Add(CreateCell("$12.99"));

            // Add the constructed table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Table inserted and saved to '{outputPdf}'.");
    }

    // Helper method to create a table cell containing a TextFragment
    private static Cell CreateCell(string text)
    {
        // Each cell holds a collection of paragraphs; we add a single TextFragment
        Cell cell = new Cell();
        TextFragment tf = new TextFragment(text);
        tf.TextState.Font = FontRepository.FindFont("Helvetica");
        tf.TextState.FontSize = 12;
        tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
        cell.Paragraphs.Add(tf);
        return cell;
    }
}
