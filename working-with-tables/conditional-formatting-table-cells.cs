using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class ConditionalFormattingExample
{
    static void Main()
    {
        // Paths for the output PDF
        const string outputPath = "ConditionalFormattedTable.pdf";

        // Create a new empty PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with 3 columns
            Table table = new Table
            {
                ColumnWidths = "100 100 100", // equal column widths
                Border = new BorderInfo(BorderSide.All, 0.5f) // thin border for visibility
            };

            // Sample numeric data
            double[,] data = new double[,] {
                { 45, 78, 12 },
                { 90, 55, 33 },
                { 20, 85, 60 }
            };

            // Populate the table with data
            for (int i = 0; i < data.GetLength(0); i++)
            {
                Row row = table.Rows.Add();
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    // Create a cell with the numeric value as text
                    Cell cell = row.Cells.Add(data[i, j].ToString());

                    // Conditional formatting: if value exceeds threshold, set background color
                    const double threshold = 70.0;
                    if (data[i, j] > threshold)
                    {
                        // Use Aspose.Pdf.Color to avoid System.Drawing ambiguity
                        cell.BackgroundColor = Aspose.Pdf.Color.LightGoldenrodYellow;
                    }
                }
            }

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF document – guard against missing libgdiplus on non‑Windows platforms
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF saved without GDI+ dependent features.");
                }
            }
        }

        Console.WriteLine($"PDF with conditional formatting saved to '{outputPath}'.");
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
