using System;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "template.pdf";
        const string outputPdf = "output.pdf";

        // Sample data table
        DataTable dt = new DataTable();
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("Age", typeof(int));
        dt.Columns.Add("Country", typeof(string));
        dt.Rows.Add("Alice", 30, "USA");
        dt.Rows.Add("Bob", 25, "Canada");
        dt.Rows.Add("Charlie", 35, "USA");

        // Filter rows to include only records where Country = 'USA'
        DataView view = new DataView(dt);
        view.RowFilter = "Country = 'USA'";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPdf))
        {
            // Create a table
            Aspose.Pdf.Table table = new Aspose.Pdf.Table();
            table.ColumnWidths = "100 100 100"; // three equal columns
            table.DefaultCellBorder = new Aspose.Pdf.BorderInfo(Aspose.Pdf.BorderSide.All, 0.5f);
            table.DefaultCellPadding = new Aspose.Pdf.MarginInfo(5, 5, 5, 5);
            table.DefaultCellTextState = new Aspose.Pdf.Text.TextState
            {
                Font = Aspose.Pdf.Text.FontRepository.FindFont("Helvetica"),
                FontSize = 12,
                ForegroundColor = Aspose.Pdf.Color.Black
            };

            // Import the filtered DataView into the table
            // Parameters: sourceDataView, isColumnNamesImported, firstFilledRow, firstFilledColumn, maxRows, maxColumns
            table.ImportDataView(view, true, 0, 0, view.Count, view.Table.Columns.Count);

            // Position the table on the first page
            Aspose.Pdf.Page page = doc.Pages[1];
            table.Left = 50;
            table.Top = 700;
            page.Paragraphs.Add(table);

            // Save the document with OS check for GDI+ availability
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPdf);
            }
            else
            {
                try
                {
                    doc.Save(outputPdf);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("GDI+ (libgdiplus) not available on this platform; PDF saved may be incomplete.");
                }
            }
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }

    // Helper to detect nested DllNotFoundException
    static bool ContainsDllNotFound(Exception ex)
    {
        while (ex != null)
        {
            if (ex is DllNotFoundException) return true;
            ex = ex.InnerException;
        }
        return false;
    }
}