using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";          // Existing PDF (optional)
        const string outputPath = "centered_table.pdf";

        // Load existing PDF if it exists; otherwise create a new empty document.
        using (Document doc = File.Exists(inputPath) ? new Document(inputPath) : new Document())
        {
            // Ensure there is at least one page to host the table.
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Create a table instance and center it on the page.
            Table table = new Table
            {
                HorizontalAlignment = HorizontalAlignment.Center, // Center the whole table
                ColumnAdjustment = ColumnAdjustment.AutoFitToContent // Optional auto‑fit
            };

            // Add a simple row with two cells (demo content).
            Row row = table.Rows.Add();
            row.Cells.Add("Cell 1");
            row.Cells.Add("Cell 2");

            // Insert the table into the first page's paragraph collection.
            doc.Pages[1].Paragraphs.Add(table);

            // Save the document – guard against missing GDI+ on non‑Windows platforms.
            SaveDocument(doc, outputPath);
        }

        Console.WriteLine($"PDF processing completed. Output: '{outputPath}'.");
    }

    private static void SaveDocument(Document doc, string path)
    {
        // On Windows the native GDI+ library is always present.
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            doc.Save(path);
            return;
        }

        // On macOS / Linux the Aspose.Pdf engine may try to load libgdiplus.
        // Wrap the call in a try‑catch that looks for a nested DllNotFoundException.
        try
        {
            doc.Save(path);
        }
        catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
        {
            Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                              "The PDF was saved without GDI‑dependent features.");
        }
    }

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
