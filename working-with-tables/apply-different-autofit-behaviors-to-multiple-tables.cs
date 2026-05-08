using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for TextFragment

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page to add tables to
            Page page = doc.Pages.Count > 0 ? doc.Pages[1] : doc.Pages.Add();

            // ------------------------------------------------------------
            // Table 1 – AutoFit to content (columns shrink to fit cell content)
            // ------------------------------------------------------------
            Table tableFitToContent = new Table
            {
                // Position the table on the page
                Left = 50,
                Top  = 500,
                // Apply the AutoFit behavior
                ColumnAdjustment = ColumnAdjustment.AutoFitToContent
            };

            // Define a simple 2‑column layout (ColumnWidths is a string, not a collection)
            tableFitToContent.ColumnWidths = "200 200";

            // Add a header row
            Row headerRow = tableFitToContent.Rows.Add();
            headerRow.Cells.Add(new Cell { Paragraphs = { new TextFragment("Header 1") } });
            headerRow.Cells.Add(new Cell { Paragraphs = { new TextFragment("Header 2") } });

            // Add a data row with longer text to demonstrate fitting
            Row dataRow = tableFitToContent.Rows.Add();
            dataRow.Cells.Add(new Cell { Paragraphs = { new TextFragment("This is a very long piece of text that should cause the column to shrink to fit its content.") } });
            dataRow.Cells.Add(new Cell { Paragraphs = { new TextFragment("Short") } });

            // Add the table to the page
            page.Paragraphs.Add(tableFitToContent);

            // ------------------------------------------------------------
            // Table 2 – AutoFit to window (columns expand to fill the table width)
            // ------------------------------------------------------------
            Table tableFitToWindow = new Table
            {
                Left = 50,
                Top  = 300,
                // Apply the other AutoFit behavior
                ColumnAdjustment = ColumnAdjustment.AutoFitToWindow
            };

            // Same 2‑column layout
            tableFitToWindow.ColumnWidths = "200 200";

            // Header row
            Row headerRow2 = tableFitToWindow.Rows.Add();
            headerRow2.Cells.Add(new Cell { Paragraphs = { new TextFragment("Column A") } });
            headerRow2.Cells.Add(new Cell { Paragraphs = { new TextFragment("Column B") } });

            // Data row
            Row dataRow2 = tableFitToWindow.Rows.Add();
            dataRow2.Cells.Add(new Cell { Paragraphs = { new TextFragment("Short") } });
            dataRow2.Cells.Add(new Cell { Paragraphs = { new TextFragment("Another short text") } });

            // Add the second table to the page
            page.Paragraphs.Add(tableFitToWindow);

            // Save the modified PDF (using the standard Save method)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with tables to '{outputPath}'.");
    }
}
