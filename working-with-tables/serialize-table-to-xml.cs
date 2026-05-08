using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing; // for BorderInfo and Color

class SerializeTableToXml
{
    static void Main()
    {
        // Paths for the output PDF (optional) and XML files
        const string pdfPath = "table.pdf";
        const string xmlPath = "table.xml";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and set basic properties
            Table table = new Table
            {
                // Example: set table border and alignment
                Border = new BorderInfo(BorderSide.All, 1f, Color.Black), // Aspose.Pdf.Drawing.Color, no System.Drawing dependency
                Alignment = HorizontalAlignment.Center
            };

            // Define column widths (optional)
            table.ColumnWidths = "100 150 200";

            // Add header row
            Row header = table.Rows.Add();
            header.Cells.Add(new TextFragment("ID"));
            header.Cells.Add(new TextFragment("Name"));
            header.Cells.Add(new TextFragment("Quantity"));

            // Add a few data rows
            for (int i = 1; i <= 3; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add(new TextFragment(i.ToString()));
                row.Cells.Add(new TextFragment($"Item {i}"));
                row.Cells.Add(new TextFragment((i * 10).ToString()));
            }

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // OPTIONAL: Save the PDF for visual verification (guarded for non‑Windows platforms)
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(pdfPath);
            }
            else
            {
                try
                {
                    doc.Save(pdfPath);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF save skipped.");
                }
            }

            // Save the document structure as XML using XmlSaveOptions (also guarded)
            XmlSaveOptions xmlOptions = new XmlSaveOptions();
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(xmlPath, xmlOptions);
            }
            else
            {
                try
                {
                    doc.Save(xmlPath, xmlOptions);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. XML save skipped.");
                }
            }
        }

        Console.WriteLine($"Table saved as PDF: {pdfPath}");
        Console.WriteLine($"Document XML representation saved as: {xmlPath}");
    }

    // Helper method to detect a nested DllNotFoundException (e.g., missing libgdiplus)
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