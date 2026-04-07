using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with a single column
            Table table = new Table
            {
                ColumnWidths = "200" // width of the column
            };
            page.Paragraphs.Add(table);

            // Add a row and a cell
            Row row = table.Rows.Add();
            Cell cell = row.Cells.Add();

            // Set some text in the cell (optional)
            cell.Paragraphs.Add(new TextFragment("Accept Terms:"));

            // Define the rectangle for the checkbox inside the cell.
            // The coordinates are (llx, lly, urx, ury) in points.
            // Use float literals to avoid GDI+ issues.
            Aspose.Pdf.Rectangle chkRect = new Aspose.Pdf.Rectangle(120f, 750f, 140f, 770f);

            // Create a checkbox field and add it to the cell's paragraph collection.
            // Adding the field to the cell ensures it is rendered inside the table cell.
            CheckboxField checkbox = new CheckboxField(page, chkRect)
            {
                Name = "AcceptTerms",   // field name
                Checked = false,        // initial state
                ExportValue = "Yes"     // value exported when checked
            };
            cell.Paragraphs.Add(checkbox);

            // Save the PDF document – guard the call on non‑Windows platforms where libgdiplus may be missing.
            string outputPath = "checkbox_in_cell.pdf";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }
        }

        Console.WriteLine("PDF creation routine finished.");
    }

    // Helper that walks the inner‑exception chain looking for a DllNotFoundException.
    private static bool ContainsDllNotFound(Exception? ex)
    {
        while (ex != null)
        {
            if (ex is DllNotFoundException)
                return true;
            ex = ex.InnerException;
        }
        return false;
    }
}
