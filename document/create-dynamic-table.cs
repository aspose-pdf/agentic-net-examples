using System;
using System.Data;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Prepare a DataTable with dynamic number of rows
        DataTable dataTable = new DataTable("Sample");
        dataTable.Columns.Add(new DataColumn("ID", typeof(int)));
        dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
        dataTable.Columns.Add(new DataColumn("Quantity", typeof(int)));

        int rowCount = 10; // can be changed to any size
        for (int i = 0; i < rowCount; i++)
        {
            DataRow row = dataTable.NewRow();
            row["ID"] = i + 1;
            row["Name"] = $"Item {i + 1}";
            row["Quantity"] = (i + 1) * 5;
            dataTable.Rows.Add(row);
        }

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            Table table = new Table
            {
                ColumnWidths = "100 200 100",
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5),
                DefaultCellTextState = new TextState
                {
                    FontSize = 12,
                    Font = FontRepository.FindFont("Helvetica"),
                    ForegroundColor = Color.Black
                }
            };

            // Import the DataTable; include column names as the first row
            table.ImportDataTable(dataTable, true, 0, 0);

            page.Paragraphs.Add(table);

            string outputPath = "dynamic_table.pdf";

            // Guard Document.Save against missing GDI+ (libgdiplus) on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}'. (non‑Windows platform, libgdiplus may be required)");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }
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
