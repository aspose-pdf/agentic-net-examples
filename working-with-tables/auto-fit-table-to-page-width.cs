using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths for the output PDF
        const string outputPath = "table_auto_fit.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a table and configure it to stretch across the page width
            Table table = new Table();

            // ColumnAdjustment.AutoFitToWindow makes the table fit the page width
            table.ColumnAdjustment = ColumnAdjustment.AutoFitToWindow;

            // Optionally set the left and top positions (in points) of the table
            table.Left = 0;   // start at the left margin
            table.Top = 100;  // some offset from the top of the page

            // Define the number of columns (e.g., 3) by adding a row with cells
            // First row – header
            Row headerRow = table.Rows.Add();
            headerRow.Cells.Add("Header 1");
            headerRow.Cells.Add("Header 2");
            headerRow.Cells.Add("Header 3");

            // Second row – data
            Row dataRow = table.Rows.Add();
            dataRow.Cells.Add("Cell 1");
            dataRow.Cells.Add("Cell 2");
            dataRow.Cells.Add("Cell 3");

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the document as PDF (no SaveOptions needed for PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with auto‑fit table saved to '{outputPath}'.");
    }
}