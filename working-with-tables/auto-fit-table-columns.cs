using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document document = new Document())
        {
            // Add a page to the document
            Page page = document.Pages.Add();

            // Create a table and apply AutoFit behavior via ColumnAdjustment (AutoFitBehavior is not available in this version)
            Table table = new Table();
            table.ColumnAdjustment = ColumnAdjustment.AutoFitToContent; // makes columns fit their content

            // First row
            Row firstRow = table.Rows.Add();
            firstRow.Cells.Add().Paragraphs.Add(new TextFragment("Short"));
            firstRow.Cells.Add().Paragraphs.Add(new TextFragment("A much longer piece of text that forces the column to expand"));

            // Second row
            Row secondRow = table.Rows.Add();
            secondRow.Cells.Add().Paragraphs.Add(new TextFragment("Another"));
            secondRow.Cells.Add().Paragraphs.Add(new TextFragment("Text"));

            // Add the table to the page
            page.Paragraphs.Add(table);

            string outputPath = "AutoFitTable.pdf";

            // Guard Document.Save for platforms where libgdiplus (GDI+) is missing (e.g., macOS/Linux)
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                document.Save(outputPath);
            }
            else
            {
                try
                {
                    document.Save(outputPath);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("GDI+ (libgdiplus) is not available on this platform. PDF saved without GDI‑dependent features.");
                }
            }
        }
    }

    // Helper to walk the inner‑exception chain and detect a missing native GDI+ library
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
