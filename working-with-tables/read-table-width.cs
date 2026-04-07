using Aspose.Pdf;
using System;
using System.IO;
using System.Runtime.InteropServices;

class Program
{
    static void Main()
    {
        using (Document document = new Document())
        {
            Page page = document.Pages.Add();

            Table table = new Table();
            table.ColumnWidths = "100 150 200";

            Row row = table.Rows.Add();
            row.Cells.Add("Cell 1");
            row.Cells.Add("Cell 2");
            row.Cells.Add("Cell 3");

            page.Paragraphs.Add(table);

            // Save to a memory stream only on platforms where GDI+ (libgdiplus) is available.
            using (MemoryStream ms = new MemoryStream())
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    // On Windows the native GDI+ library is always present, so we can safely save.
                    document.Save(ms);
                }
                else
                {
                    // On macOS / Linux the save operation may require libgdiplus. Guard it to avoid a crash.
                    try
                    {
                        document.Save(ms);
                    }
                    catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                    {
                        Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. Skipping in‑memory save.");
                    }
                }

                // After the layout pass (which occurs during Save or when the document is rendered),
                // the Table object knows its actual width.
                double calculatedWidth = table.GetWidth();
                Console.WriteLine($"Calculated table width: {calculatedWidth}");
            }

            // Final PDF file save – also guarded for non‑Windows platforms.
            string outputPath = "output.pdf";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                document.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    document.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}'.");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF not saved.");
                }
            }
        }
    }

    // Helper that walks the inner‑exception chain to detect a missing native GDI+ library.
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
