using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for BorderInfo and BorderSide if needed

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // existing PDF to modify
        const string outputPdf = "output.pdf";  // result PDF with table

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the existing PDF inside a using block for proper disposal
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                // If no pages exist, add a blank page
                doc.Pages.Add();
            }

            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a new table
            Table table = new Table();

            // Set overall table border (black, 1 point)
            table.Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black);

            // Set default cell border (gray, 0.5 point)
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Gray);

            // Define three columns with equal widths (adjust as needed)
            table.ColumnWidths = "150 150 150";

            // Number of rows to add
            int rowCount = 10;

            for (int i = 0; i < rowCount; i++)
            {
                // Add a new row to the table
                Row row = table.Rows.Add();

                // Add three cells with sample text
                row.Cells.Add($"Row {i + 1} - Col 1");
                row.Cells.Add($"Row {i + 1} - Col 2");
                row.Cells.Add($"Row {i + 1} - Col 3");

                // Apply alternating background colors
                if (i % 2 == 0)
                {
                    // Even rows: light gray background
                    row.BackgroundColor = Aspose.Pdf.Color.LightGray;
                }
                else
                {
                    // Odd rows: white background (default)
                    row.BackgroundColor = Aspose.Pdf.Color.White;
                }
            }

            // Add the configured table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified document
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Table added and PDF saved to '{outputPdf}'.");
    }
}