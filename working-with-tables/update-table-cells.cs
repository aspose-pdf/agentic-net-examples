using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a sample PDF file (self‑contained example)
        using (Document createDoc = new Document())
        {
            createDoc.Pages.Add();

            // Guard Save on platforms without GDI+ (e.g., macOS/Linux)
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                createDoc.Save("input.pdf");
            }
            else
            {
                try
                {
                    createDoc.Save("input.pdf");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("GDI+ not available; skipping PDF creation on this platform.");
                }
            }
        }

        // Load the sample PDF and add a table
        using (Document pdfDoc = new Document("input.pdf"))
        {
            Table table = new Table();
            table.ColumnWidths = "100 100";
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f);

            // First row
            Row firstRow = table.Rows.Add();
            firstRow.Cells.Add("Cell 1");
            firstRow.Cells.Add("Cell 2");

            // Second row
            Row secondRow = table.Rows.Add();
            secondRow.Cells.Add("Cell 3");
            secondRow.Cells.Add("Cell 4");

            // Update specific cell values programmatically
            Cell cellToUpdate1 = table.Rows[0].Cells[0];
            cellToUpdate1.Paragraphs.Clear();
            cellToUpdate1.Paragraphs.Add(new TextFragment("Updated 1"));

            Cell cellToUpdate2 = table.Rows[1].Cells[1];
            cellToUpdate2.Paragraphs.Clear();
            cellToUpdate2.Paragraphs.Add(new TextFragment("Updated 4"));

            // Add the table to the first page (page indexing is 1‑based)
            pdfDoc.Pages[1].Paragraphs.Add(table);

            // Guard Save on platforms without GDI+
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                pdfDoc.Save("output.pdf");
            }
            else
            {
                try
                {
                    pdfDoc.Save("output.pdf");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("GDI+ not available; cannot save PDF on this platform.");
                }
            }
        }
    }

    private static bool ContainsDllNotFound(Exception ex)
    {
        Exception? current = ex;
        while (current != null)
        {
            if (current is DllNotFoundException)
                return true;
            current = current.InnerException;
        }
        return false;
    }
}