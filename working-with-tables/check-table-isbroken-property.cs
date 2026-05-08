using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths for input and output PDFs
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // If the input file does not exist, create a simple PDF with one blank page
        if (!File.Exists(inputPath))
        {
            using (Document newDoc = new Document())
            {
                newDoc.Pages.Add(); // add a blank page
                newDoc.Save(inputPath);
            }
        }

        // Load the PDF using the recommended load pattern
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create a table and configure basic layout properties
            Table table = new Table
            {
                ColumnWidths = "100 100 100", // three equal columns
                Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black),
                IsBroken = false // default: do not break across pages
            };

            // Add a single row with three cells
            Row row = table.Rows.Add();
            row.Cells.Add("Cell 1");
            row.Cells.Add("Cell 2");
            row.Cells.Add("Cell 3");

            // Insert the table into the page's paragraph collection
            page.Paragraphs.Add(table);

            // Check whether the table is set to break onto the next page
            bool willBreak = table.IsBroken;
            Console.WriteLine($"Table.IsBroken (initial) = {willBreak}");

            // For demonstration, force the table to break and verify the change
            table.IsBroken = true;
            Console.WriteLine($"Table.IsBroken (after setting) = {table.IsBroken}");

            // Save the modified document with a guard for platforms lacking GDI+ (e.g., macOS/Linux)
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

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }

    // Helper to detect a nested DllNotFoundException (libgdiplus) inside a TypeInitializationException
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