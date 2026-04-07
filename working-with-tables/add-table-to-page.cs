using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

public class Program
{
    public static void Main()
    {
        const string samplePath = "sample.pdf";
        const string outputPath = "output.pdf";

        // Create a sample PDF with a single page
        using (Document tempDoc = new Document())
        {
            tempDoc.Pages.Add();
            tempDoc.Save(samplePath);
        }

        // Open the sample PDF
        using (Document doc = new Document(samplePath))
        {
            // Access the target page (page numbers are 1‑based)
            Page targetPage = doc.Pages[1];

            // Create a table with three columns
            Table table = new Table
            {
                ColumnWidths = "100 100 100",
                Margin = new MarginInfo { Top = 20, Left = 20 }
            };

            // Add a row and cells
            Row row = table.Rows.Add();
            row.Cells.Add("Cell 1");
            row.Cells.Add("Cell 2");
            row.Cells.Add("Cell 3");

            // Insert the table into the page
            targetPage.Paragraphs.Add(table);

            // Save the PDF – guard against missing GDI+ on non‑Windows platforms
            SaveDocument(doc, outputPath);
        }
    }

    private static void SaveDocument(Document doc, string path)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            doc.Save(path);
            Console.WriteLine($"PDF saved to '{path}'.");
            return;
        }

        // Non‑Windows platforms may lack libgdiplus – try‑catch to handle it gracefully
        try
        {
            doc.Save(path);
            Console.WriteLine($"PDF saved to '{path}'.");
        }
        catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
        {
            Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                              "The PDF was not saved, but the rest of the code executed correctly.");
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