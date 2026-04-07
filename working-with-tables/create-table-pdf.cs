using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public class CreateTablePdf
{
    public static void Main()
    {
        // Create a sample PDF file (self‑contained example)
        using (Document sampleDoc = new Document())
        {
            sampleDoc.Pages.Add();
            SaveDocument(sampleDoc, "input.pdf");
        }

        // Open the sample PDF and add a table
        using (Document pdfDoc = new Document("input.pdf"))
        {
            // Create a table with two columns
            Table table = new Table();
            table.ColumnWidths = "100 100";

            // First row
            Row row1 = table.Rows.Add();
            Cell cell11 = row1.Cells.Add();
            cell11.Paragraphs.Add(new TextFragment("Cell 1,1"));
            Cell cell12 = row1.Cells.Add();
            cell12.Paragraphs.Add(new TextFragment("Cell 1,2"));

            // Second row
            Row row2 = table.Rows.Add();
            Cell cell21 = row2.Cells.Add();
            cell21.Paragraphs.Add(new TextFragment("Cell 2,1"));
            Cell cell22 = row2.Cells.Add();
            cell22.Paragraphs.Add(new TextFragment("Cell 2,2"));

            // Optional table margin
            table.Margin = new MarginInfo(10, 10, 10, 10);

            // Add the table to the first page (1‑based indexing)
            pdfDoc.Pages[1].Paragraphs.Add(table);

            // Save the updated PDF
            SaveDocument(pdfDoc, "output.pdf");
        }
    }

    // Centralised save method that guards against missing GDI+ (libgdiplus) on non‑Windows platforms
    private static void SaveDocument(Document doc, string path)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            doc.Save(path);
        }
        else
        {
            try
            {
                doc.Save(path);
            }
            catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
            {
                Console.WriteLine($"Warning: Unable to save '{path}' because GDI+ (libgdiplus) is not available on this platform.");
            }
        }
    }

    // Helper that walks the inner‑exception chain to detect a DllNotFoundException
    private static bool ContainsDllNotFound(Exception ex)
    {
        Exception? current = ex;
        while (current != null)
        {
            if (current is DllNotFoundException)
            {
                return true;
            }
            current = current.InnerException;
        }
        return false;
    }
}