using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a sample PDF containing a table with three columns
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            Table table = new Table();
            table.ColumnWidths = "100 100 100";

            // Header row
            Row header = table.Rows.Add();
            Cell headerCell1 = header.Cells.Add("Header 1");
            Cell headerCell2 = header.Cells.Add("Header 2");
            Cell headerCell3 = header.Cells.Add("Header 3");

            // Data rows
            for (int i = 1; i <= 3; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add("R" + i + "C1");
                row.Cells.Add("R" + i + "C2");
                row.Cells.Add("R" + i + "C3");
            }

            page.Paragraphs.Add(table);
            doc.Save("input.pdf");
        }

        // Reopen the PDF and remove the second column from the table
        using (Document doc = new Document("input.pdf"))
        {
            Page page = doc.Pages[1];
            Table targetTable = null;
            foreach (BaseParagraph paragraph in page.Paragraphs)
            {
                if (paragraph is Table)
                {
                    targetTable = (Table)paragraph;
                    break;
                }
            }

            if (targetTable != null)
            {
                // Delete the cell at column index 1 (second column) from each row
                foreach (Row row in targetTable.Rows)
                {
                    Cell cellToRemove = row.Cells[1];
                    row.Cells.Remove(cellToRemove);
                }

                // Update column widths for the remaining two columns
                targetTable.ColumnWidths = "150 150";
            }

            doc.Save("output.pdf");
        }
    }
}
