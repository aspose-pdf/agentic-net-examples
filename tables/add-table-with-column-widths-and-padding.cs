using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for TextState, FontStyles

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Aspose.Pdf uses 1‑based page indexing
            Page page = doc.Pages[1];

            // Create a table with the required column widths and cell padding
            Table table = new Table
            {
                ColumnWidths = "120 180",                 // two columns: 120pt and 180pt
                DefaultCellPadding = new MarginInfo(4, 4, 4, 4) // padding in points (left, bottom, right, top)
            };

            // Optional visual styling (border and color)
            table.Border = new BorderInfo(BorderSide.All, 0.5f, Color.Black);

            // Header row
            Row header = table.Rows.Add();
            Cell hdrCell1 = header.Cells.Add("Header 1");
            Cell hdrCell2 = header.Cells.Add("Header 2");
            // Make header text bold
            hdrCell1.DefaultCellTextState = new TextState { FontStyle = FontStyles.Bold };
            hdrCell2.DefaultCellTextState = new TextState { FontStyle = FontStyles.Bold };

            // Data row
            Row data = table.Rows.Add();
            data.Cells.Add("Row 1, Col 1");
            data.Cells.Add("Row 1, Col 2");

            // Add the table to the page's content. No explicit rectangle is needed – the table will flow
            // automatically. If absolute positioning is required, wrap the table in a FloatingBox.
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table added and PDF saved to '{outputPath}'.");
    }
}
