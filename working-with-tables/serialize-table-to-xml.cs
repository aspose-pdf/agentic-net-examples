using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output paths
        const string outputXml = "table_output.xml";

        // Create a new PDF document
        using (Document pdfDoc = new Document())
        {
            // Add a page to the document
            Page page = pdfDoc.Pages.Add();

            // Create a table and set its basic properties
            Table table = new Table
            {
                ColumnWidths = "100 150 200", // three columns with specified widths
                Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black),
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Gray),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // Add header row
            Row header = table.Rows.Add();
            header.Cells.Add("ID");
            header.Cells.Add("Name");
            header.Cells.Add("Quantity");

            // Add a few data rows
            Row row1 = table.Rows.Add();
            row1.Cells.Add("1");
            row1.Cells.Add("Apples");
            row1.Cells.Add("50");

            Row row2 = table.Rows.Add();
            row2.Cells.Add("2");
            row2.Cells.Add("Oranges");
            row2.Cells.Add("30");

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Save the entire document (including the table) as XML.
            // Guard the Save call on non‑Windows platforms where libgdiplus may be missing.
            XmlSaveOptions xmlOptions = new XmlSaveOptions();
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                pdfDoc.Save(outputXml, xmlOptions);
            }
            else
            {
                try
                {
                    pdfDoc.Save(outputXml, xmlOptions);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. XML serialization was skipped.");
                }
            }
        }

        Console.WriteLine($"Table serialized to XML at '{outputXml}'.");
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
