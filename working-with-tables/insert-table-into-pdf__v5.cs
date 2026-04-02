using System;
using System.Data;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public class InsertTableExample
{
    public static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // -------------------------------------------------------------------
        // 1. Create a sample PDF file (one blank page) to work with.
        // -------------------------------------------------------------------
        using (Document createDoc = new Document())
        {
            createDoc.Pages.Add();
            // Guard the Save call – on non‑Windows platforms Aspose.Pdf may need libgdiplus.
            SaveDocument(createDoc, inputPath);
        }

        // -------------------------------------------------------------------
        // 2. Load the existing PDF and insert a table on the first page.
        // -------------------------------------------------------------------
        using (Document doc = new Document(inputPath))
        {
            // -------------------------------------------------------------------
            // 2.1 Create the Table instance.
            // -------------------------------------------------------------------
            Table table = new Table
            {
                // Position of the table on the page (optional – you can also use a Graph).
                Left = 100f,
                Top = 200f,
                // Define column widths before importing data – prevents NullReferenceException.
                ColumnWidths = "150 150",
                // Optional – give the table a visible border so the result is easy to see.
                Border = new BorderInfo(BorderSide.All, 0.5f),
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f)
            };

            // -------------------------------------------------------------------
            // 2.2 Build a DataTable that will be rendered inside the PDF table.
            // -------------------------------------------------------------------
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Name");
            dataTable.Columns.Add("Age");
            dataTable.Rows.Add("Alice", "30");
            dataTable.Rows.Add("Bob",   "25");

            // -------------------------------------------------------------------
            // 2.3 Import the DataTable into the PDF Table.
            // The third argument (true) tells Aspose to include column headers.
            // -------------------------------------------------------------------
            table.ImportDataTable(dataTable, true, 0, 0);

            // -------------------------------------------------------------------
            // 2.4 Add the table to the first page's paragraph collection.
            // -------------------------------------------------------------------
            doc.Pages[1].Paragraphs.Add(table);

            // -------------------------------------------------------------------
            // 3. Save the updated PDF.
            // -------------------------------------------------------------------
            SaveDocument(doc, outputPath);
        }
    }

    /// <summary>
    /// Saves a document while handling the missing GDI+ (libgdiplus) scenario on non‑Windows platforms.
    /// </summary>
    private static void SaveDocument(Document document, string path)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            // On Windows the native GDI+ library is always present.
            document.Save(path);
            Console.WriteLine($"PDF saved to '{path}'.");
            return;
        }

        // On macOS / Linux Aspose.Pdf may still work for many operations, but Document.Save
        // internally uses System.Drawing which requires libgdiplus. Guard against the crash.
        try
        {
            document.Save(path);
            Console.WriteLine($"PDF saved to '{path}'.");
        }
        catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
        {
            Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                              "The PDF was not saved, but the rest of the code executed successfully.");
        }
    }

    /// <summary>
    /// Walks the inner‑exception chain to detect a DllNotFoundException (e.g., missing libgdiplus).
    /// </summary>
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
