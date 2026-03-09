using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class InsertTableExample
{
    static void Main()
    {
        // Paths to the source PDF and the output PDF
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output_with_table.pdf";

        // Ensure the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the existing PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Choose the page where the table will be inserted (1‑based indexing)
            Page targetPage = pdfDoc.Pages[1];

            // -------------------------------------------------
            // Create and format the table
            // -------------------------------------------------
            Table table = new Table();

            // Set overall table border
            table.Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black);

            // Set default cell border
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Gray);

            // Set default cell padding
            table.DefaultCellPadding = new MarginInfo { Top = 5f, Bottom = 5f, Left = 5f, Right = 5f };

            // Define column widths (space‑separated values, in points)
            table.ColumnWidths = "120 150 180";

            // -------------------------------------------------
            // Add header row
            // -------------------------------------------------
            Row headerRow = table.Rows.Add();
            // Header cell 1
            Cell headerCell1 = headerRow.Cells.Add("Product");
            headerCell1.BackgroundColor = Aspose.Pdf.Color.LightGray;
            // Header cell 2
            Cell headerCell2 = headerRow.Cells.Add("Quantity");
            headerCell2.BackgroundColor = Aspose.Pdf.Color.LightGray;
            // Header cell 3
            Cell headerCell3 = headerRow.Cells.Add("Price");
            headerCell3.BackgroundColor = Aspose.Pdf.Color.LightGray;

            // -------------------------------------------------
            // Add data rows (example data)
            // -------------------------------------------------
            string[,] data = {
                { "Widget A", "10", "$15.00" },
                { "Widget B", "5",  "$25.00" },
                { "Widget C", "12", "$9.50" }
            };

            for (int i = 0; i < data.GetLength(0); i++)
            {
                Row dataRow = table.Rows.Add();
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    // Each cell can contain a TextFragment for richer formatting if needed
                    TextFragment tf = new TextFragment(data[i, j]);
                    dataRow.Cells.Add(tf);
                }
            }

            // -------------------------------------------------
            // Insert the table into the page's paragraph collection
            // -------------------------------------------------
            targetPage.Paragraphs.Add(table);

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Table inserted and PDF saved to '{outputPdfPath}'.");
    }
}