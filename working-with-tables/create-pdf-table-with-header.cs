using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "TableExample.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a table and set its basic properties
            Table table = new Table
            {
                // Optional: set table position on the page
                Left = 50,
                Top = 700,
                // Define column widths (in points)
                ColumnWidths = "150 150 150"
            };

            // ----- Add Header Row -----
            Row headerRow = table.Rows.Add(); // Add a new row
            // Add three header cells
            headerRow.Cells.Add(new Cell { Paragraphs = { new TextFragment("Product") }, BackgroundColor = Color.LightGray });
            headerRow.Cells.Add(new Cell { Paragraphs = { new TextFragment("Quantity") }, BackgroundColor = Color.LightGray });
            headerRow.Cells.Add(new Cell { Paragraphs = { new TextFragment("Price") }, BackgroundColor = Color.LightGray });

            // ----- Add Data Rows -----
            AddDataRow(table, "Widget A", "10", "$15.00");
            AddDataRow(table, "Widget B", "5", "$25.00");
            AddDataRow(table, "Widget C", "8", "$12.50");

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // ----- Save the PDF (guarded for non‑Windows platforms) -----
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

        Console.WriteLine("PDF with table created successfully.");
    }

    // Helper to add a data row to the table
    private static void AddDataRow(Table table, string product, string quantity, string price)
    {
        Row row = table.Rows.Add();
        row.Cells.Add(new Cell { Paragraphs = { new TextFragment(product) } });
        row.Cells.Add(new Cell { Paragraphs = { new TextFragment(quantity) } });
        row.Cells.Add(new Cell { Paragraphs = { new TextFragment(price) } });
    }

    // Helper that walks the exception chain looking for a DllNotFoundException (e.g., missing libgdiplus)
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