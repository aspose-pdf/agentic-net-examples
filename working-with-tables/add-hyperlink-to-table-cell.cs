using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with a single column
            Table table = new Table { ColumnWidths = "200" };

            // Add a row to the table
            Row row = table.Rows.Add();

            // Add a cell (no initial text – we will add a TextFragment with a hyperlink)
            Cell cell = row.Cells.Add();

            // Define text appearance for the cell
            TextState textState = new TextState
            {
                FontSize = 12,
                Font = FontRepository.FindFont("Arial")
            };
            cell.DefaultCellTextState = textState;

            // Create a TextFragment that contains a hyperlink
            TextFragment tf = new TextFragment("Click here")
            {
                Hyperlink = new WebHyperlink("https://example.com")
            };
            // Apply the same text state to the fragment (modify the existing TextState instance)
            tf.TextState.FontSize = textState.FontSize;
            tf.TextState.Font = textState.Font;

            cell.Paragraphs.Add(tf);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
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
                    Console.WriteLine("GDI+ (libgdiplus) is not available on this platform. PDF saved without GDI‑dependent features.");
                }
            }
        }
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus)
    private static bool ContainsDllNotFound(Exception ex)
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
