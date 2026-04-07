using System;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class PdfReportGenerator
{
    static void Main()
    {
        // Paths (adjust as needed)
        const string outputPath = "Report.pdf";

        // Assume these DataTables are filled from different databases
        DataTable customersTable = GetCustomersData();   // placeholder method
        DataTable ordersTable    = GetOrdersData();      // placeholder method
        DataTable productsTable  = GetProductsData();    // placeholder method

        // Collection of tables to be added to the PDF
        DataTable[] dataTables = new DataTable[] { customersTable, ordersTable, productsTable };
        string[] titles = new string[] { "Customers", "Orders", "Products" };

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Iterate over each DataTable and create a PDF page with a table
            for (int i = 0; i < dataTables.Length; i++)
            {
                // Add a new page for the current table
                Page page = doc.Pages.Add();

                // Optional: add a title above the table
                TextFragment titleFragment = new TextFragment(titles[i])
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new MarginInfo { Top = 20, Bottom = 10 }
                };
                // TextState is read‑only; modify its properties instead of assigning a new instance
                titleFragment.TextState.FontSize = 16;
                titleFragment.TextState.FontStyle = FontStyles.Bold;
                titleFragment.TextState.ForegroundColor = Aspose.Pdf.Color.DarkBlue;
                page.Paragraphs.Add(titleFragment);

                // Create a table and set basic appearance
                Table pdfTable = new Table
                {
                    // Example: set column widths (space‑separated string)
                    ColumnWidths = "100 150 150",
                    DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f),
                    DefaultCellPadding = new MarginInfo(5, 5, 5, 5),
                    DefaultCellTextState = new TextState
                    {
                        FontSize = 12,
                        ForegroundColor = Aspose.Pdf.Color.Black
                    }
                };

                // Import the DataTable into the PDF table.
                // Parameters: source DataTable, import column names as first row,
                // start at row 0, column 0 in the PDF table.
                pdfTable.ImportDataTable(dataTables[i], true, 0, 0);

                // Add the table to the page
                page.Paragraphs.Add(pdfTable);
            }

            // Save the document (guard against missing GDI+ on non‑Windows platforms)
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF report saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF report saved to '{outputPath}'.");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF was not saved.");
                }
            }
        }
    }

    // Helper to detect missing native GDI+ library
    private static bool ContainsDllNotFound(Exception ex)
    {
        while (ex != null)
        {
            if (ex is DllNotFoundException)
                return true;
            ex = ex.InnerException;
        }
        return false;
    }

    // Placeholder methods – replace with actual data‑access logic
    private static DataTable GetCustomersData()
    {
        DataTable dt = new DataTable("Customers");
        dt.Columns.Add("CustomerID", typeof(int));
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("Country", typeof(string));

        // Example rows
        dt.Rows.Add(1, "Acme Corp", "USA");
        dt.Rows.Add(2, "Globex", "UK");
        return dt;
    }

    private static DataTable GetOrdersData()
    {
        DataTable dt = new DataTable("Orders");
        dt.Columns.Add("OrderID", typeof(int));
        dt.Columns.Add("CustomerID", typeof(int));
        dt.Columns.Add("Total", typeof(decimal));

        dt.Rows.Add(1001, 1, 2500.00m);
        dt.Rows.Add(1002, 2, 1800.50m);
        return dt;
    }

    private static DataTable GetProductsData()
    {
        DataTable dt = new DataTable("Products");
        dt.Columns.Add("ProductID", typeof(int));
        dt.Columns.Add("ProductName", typeof(string));
        dt.Columns.Add("Price", typeof(decimal));

        dt.Rows.Add(10, "Widget", 19.99m);
        dt.Rows.Add(20, "Gadget", 29.99m);
        return dt;
    }
}
