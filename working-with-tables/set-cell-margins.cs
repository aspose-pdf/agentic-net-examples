using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        string outputPath = "output.pdf";

        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and add it to the page
            Table table = new Table();
            page.Paragraphs.Add(table);

            // Add a row to the table
            Row row = new Row();
            table.Rows.Add(row);

            // Add a cell to the row
            Cell cell = new Cell();
            row.Cells.Add(cell);

            // Configure custom margins for the cell
            Aspose.Pdf.MarginInfo cellMargin = new Aspose.Pdf.MarginInfo();
            cellMargin.Left = 10f;
            cellMargin.Right = 10f;
            cellMargin.Top = 5f;
            cellMargin.Bottom = 5f;
            cell.Margin = cellMargin;

            // Add some sample text to the cell
            TextFragment tf = new TextFragment("Cell with custom margins");
            cell.Paragraphs.Add(tf);

            // Save the document (guarded for platforms without GDI+)
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                Console.WriteLine("Skipping PDF save on non‑Windows platform because GDI+ (libgdiplus) is not available.");
            }
        }
    }
}