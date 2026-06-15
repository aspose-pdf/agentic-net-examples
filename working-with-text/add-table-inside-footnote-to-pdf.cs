using System;
using System.Runtime.InteropServices; // for OS check
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing; // for BorderInfo, BorderSide, MarginInfo

class Program
{
    static void Main()
    {
        const string outputPath = "FootnoteWithTable.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create the main text fragment
            TextFragment mainText = new TextFragment("This is a paragraph with a footnote.");

            // Create a footnote (Note) object
            Note footnote = new Note();

            // Create a table to be placed inside the footnote
            Table footnoteTable = new Table
            {
                // Set table appearance using BorderInfo and MarginInfo
                Border = new BorderInfo(BorderSide.All, 0.5f),
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5),
                ColumnWidths = "100 100"
            };

            // Header row
            Row headerRow = footnoteTable.Rows.Add();
            headerRow.Cells.Add("Header 1");
            headerRow.Cells.Add("Header 2");

            // Data row
            Row dataRow = footnoteTable.Rows.Add();
            dataRow.Cells.Add("Value A");
            dataRow.Cells.Add("Value B");

            // Add the table to the footnote's paragraph collection
            footnote.Paragraphs.Add(footnoteTable);

            // Assign the footnote to the text fragment
            mainText.FootNote = footnote;

            // Add the text fragment (with footnote) to the page
            page.Paragraphs.Add(mainText);

            // Save the PDF – guard against missing libgdiplus on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF with footnote table saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF with footnote table saved to '{outputPath}'. (Saved on non‑Windows platform)");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. The PDF was not saved.");
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
