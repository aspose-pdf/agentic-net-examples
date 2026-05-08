using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Operators;   // GSave, GRestore, ConcatenateMatrix
using Aspose.Pdf.Text;        // for TextState if needed

class Program
{
    static void Main()
    {
        const string outputPath = "rotated_table.pdf";

        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Build a simple table
            Table table = new Table
            {
                ColumnWidths = "100 100 100", // three columns, each 100 points wide
                // Use Aspose.Pdf.Color to avoid System.Drawing dependency on non‑Windows platforms
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // Add header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");

            // Add a few data rows
            for (int i = 1; i <= 5; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add($"R{i}C1");
                row.Cells.Add($"R{i}C2");
                row.Cells.Add($"R{i}C3");
            }

            // Create a rotation matrix for 90 degrees (π/2 radians)
            // For a 90° rotation about the origin the matrix is:
            // [ 0  1 -1  0  0  0 ]
            // (a, b, c, d, e, f) = (0, 1, -1, 0, 0, 0)
            var rotationMatrix = new ConcatenateMatrix(0, 1, -1, 0, 0, 0);

            // Apply the transformation only to the table:
            // Save graphics state, apply rotation, add the table, then restore state.
            page.Contents.Add(new GSave());                 // Save current graphics state
            page.Contents.Add(rotationMatrix);               // Apply 90° rotation
            page.Paragraphs.Add(table);                     // Table will be rendered with the rotation applied
            page.Contents.Add(new GRestore());              // Restore graphics state

            // Save the document – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF with rotated table saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF with rotated table saved to '{outputPath}'. (non‑Windows platform, libgdiplus must be present)");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. The PDF could not be saved.");
                }
            }
        }
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus)
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
