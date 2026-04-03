using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for TextFragment if needed

class SerializeTableToXml
{
    static void Main()
    {
        // Output XML file path
        const string xmlOutputPath = "TableExport.xml";

        // Create a new empty PDF document
        using (Document pdfDoc = new Document())
        {
            // Add a page to the document
            Page page = pdfDoc.Pages.Add();

            // Create a table with 3 columns
            Table table = new Table
            {
                // Optional: set column widths (in points)
                ColumnWidths = "100 150 200"
            };

            // Add header row
            Row header = table.Rows.Add();
            header.Cells.Add("ID");
            header.Cells.Add("Name");
            header.Cells.Add("Quantity");

            // Add some data rows
            Row row1 = table.Rows.Add();
            row1.Cells.Add("1");
            row1.Cells.Add("Apples");
            row1.Cells.Add("50");

            Row row2 = table.Rows.Add();
            row2.Cells.Add("2");
            row2.Cells.Add("Oranges");
            row2.Cells.Add("30");

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Prepare XML save options (default constructor)
            XmlSaveOptions xmlOptions = new XmlSaveOptions();

            // Save the document as XML – guarded for platforms without GDI+ (libgdiplus)
            SaveDocument(pdfDoc, xmlOutputPath, xmlOptions);
        }

        Console.WriteLine($"Table serialized to XML at '{Path.GetFullPath(xmlOutputPath)}'.");
    }

    private static void SaveDocument(Document doc, string path, SaveOptions options)
    {
        // On Windows the native GDI+ library is always present, so we can save directly.
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            doc.Save(path, options);
            return;
        }

        // On non‑Windows platforms (macOS/Linux) Aspose.Pdf may require libgdiplus.
        // Wrap the call in a try/catch and handle the missing native library gracefully.
        try
        {
            doc.Save(path, options);
        }
        catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
        {
            Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. XML export was skipped.");
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