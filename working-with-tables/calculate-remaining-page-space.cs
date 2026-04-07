using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // On non‑Windows platforms Aspose.Pdf may try to load GDI+ (libgdiplus) which can cause a
        // TypeInitializationException. Skip the whole PDF generation when the required native
        // library is not present.
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Console.WriteLine("Skipping PDF generation – GDI+ (libgdiplus) is not available on this platform.");
            return;
        }

        using (Document doc = new Document())
        {
            // Add a page and some existing content
            Page page = doc.Pages.Add();
            TextFragment tf = new TextFragment("Sample content on the page.");
            page.Paragraphs.Add(tf);

            // Calculate the remaining vertical space on the page (page height minus margins and existing content)
            double pagePureHeight = page.PageInfo.PureHeight;
            double topMargin = page.PageInfo.Margin?.Top ?? 0;
            double bottomMargin = page.PageInfo.Margin?.Bottom ?? 0;

            // Bounding box of the already placed content
            Aspose.Pdf.Rectangle contentRect = page.CalculateContentBBox();
            double contentHeight = contentRect.URY - contentRect.LLY; // Height of existing content

            double remainingHeight = pagePureHeight - (topMargin + bottomMargin + contentHeight);

            Console.WriteLine($"Page pure height: {pagePureHeight}");
            Console.WriteLine($"Top margin: {topMargin}");
            Console.WriteLine($"Bottom margin: {bottomMargin}");
            Console.WriteLine($"Existing content height: {contentHeight}");
            Console.WriteLine($"Remaining height for new content: {remainingHeight}");

            // Create a table that will be placed in the remaining space
            Table table = new Table { ColumnWidths = "100 100" };
            Row row = table.Rows.Add();
            row.Cells.Add("Cell 1");
            row.Cells.Add("Cell 2");

            // Position the table just below the existing content using the lower‑left Y (LLY) coordinate
            double tableTopFromPageTop = pagePureHeight - contentRect.URY; // distance from top edge to the top of existing content
            table.Margin = new MarginInfo { Top = tableTopFromPageTop };

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            try
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
            {
                Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF not saved.");
            }
        }
    }

    // Helper that walks the inner‑exception chain looking for a DllNotFoundException
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
