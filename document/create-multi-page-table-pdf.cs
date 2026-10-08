using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "MultiPageTable.pdf";

        // Create a new PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document())
        {
            // Add the first page where the table will start.
            Page page = doc.Pages.Add();

            // Create a table and define column widths (percentages of the page width).
            Table table = new Table
            {
                ColumnWidths = "20% 30% 30% 20%"
            };

            // Add a header row.
            Row header = table.Rows.Add();
            header.Cells.Add("ID");
            header.Cells.Add("Name");
            header.Cells.Add("Description");
            header.Cells.Add("Price");

            // Style the header cells (background color and bold text).
            foreach (Cell cell in header.Cells)
            {
                cell.BackgroundColor = Aspose.Pdf.Color.LightGray;
                cell.DefaultCellTextState = new TextState
                {
                    FontSize = 12,
                    FontStyle = FontStyles.Bold
                };
            }

            // Add many data rows to force the table to span multiple pages.
            for (int i = 1; i <= 200; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add(i.ToString());
                row.Cells.Add($"Item {i}");
                row.Cells.Add($"This is a description for item number {i}. It may be quite long to test wrapping.");
                row.Cells.Add($"${(i * 1.23):F2}");
            }

            // Add the table to the page. Aspose.Pdf automatically splits the table across pages.
            page.Paragraphs.Add(table);

            // Save the document as PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with multi‑page table saved to '{outputPath}'.");
    }
}