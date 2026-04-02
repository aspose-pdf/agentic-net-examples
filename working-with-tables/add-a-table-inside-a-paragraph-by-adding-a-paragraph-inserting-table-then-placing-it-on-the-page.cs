using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_in_paragraph.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Add a simple text paragraph before the table
            TextFragment intro = new TextFragment("Below is a table inserted into the paragraph collection:");
            page.Paragraphs.Add(intro);

            // Create a table (Table inherits from BaseParagraph)
            Table table = new Table();

            // Define column widths (comma‑separated or space‑separated string)
            table.ColumnWidths = "150 150";

            // First row
            Row row1 = table.Rows.Add();
            row1.Cells.Add("Header 1");
            row1.Cells.Add("Header 2");

            // Second row
            Row row2 = table.Rows.Add();
            row2.Cells.Add("Cell 1");
            row2.Cells.Add("Cell 2");

            // Insert the table into the paragraph collection after the intro paragraph
            // Paragraphs.Insert uses a zero‑based index; we insert at position 1 (after the first paragraph)
            page.Paragraphs.Insert(1, table);

            // Save the PDF – guard the call on non‑Windows platforms where libgdiplus (GDI+) may be missing.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF with table saved to '{outputPath}'.");
            }
            else
            {
                // On macOS / Linux Aspose.Pdf may require libgdiplus for rendering.
                // Either install libgdiplus or skip the save to avoid a TypeInitializationException.
                Console.WriteLine("Skipping Document.Save() because GDI+ (libgdiplus) is not available on this platform.");
            }
        }
    }
}
