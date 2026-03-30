using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using System.Runtime.InteropServices;

class Program
{
    static void Main()
    {
        // Prepare a sample DataTable
        DataTable dataTable = new DataTable("Sample");
        DataColumn columnId = new DataColumn("ID", typeof(int));
        DataColumn columnName = new DataColumn("Name", typeof(string));
        dataTable.Columns.Add(columnId);
        dataTable.Columns.Add(columnName);
        for (int i = 1; i <= 10; i++)
        {
            DataRow row = dataTable.NewRow();
            row["ID"] = i;
            row["Name"] = "Item " + i;
            dataTable.Rows.Add(row);
        }

        // Create PDF document
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Create a table and add it to the page
            Table table = new Table();
            page.Paragraphs.Add(table);
            table.ColumnWidths = "100 200"; // simple column width definition

            // Optional header row
            Row header = table.Rows.Add();
            header.Cells.Add("ID");
            header.Cells.Add("Name");

            // Process each DataTable row
            for (int rowIndex = 0; rowIndex < dataTable.Rows.Count; rowIndex++)
            {
                DataRow sourceRow = dataTable.Rows[rowIndex];
                Row pdfRow = table.Rows.Add();
                pdfRow.Cells.Add(sourceRow["ID"].ToString());
                pdfRow.Cells.Add(sourceRow["Name"].ToString());

                // Log the processing of the current row and the page number
                Console.WriteLine("Processed DataTable row {0} into page {1}", rowIndex + 1, doc.Pages.Count);
            }

            // Save the PDF – guard against missing GDI+ on non‑Windows platforms
            string outputPath = "output.pdf";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine("PDF saved to '" + outputPath + "'.");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine("PDF saved to '" + outputPath + "'.");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ is not available on this platform. PDF not saved.");
                }
            }
        }
    }

    private static bool ContainsDllNotFound(Exception ex)
    {
        Exception current = ex;
        while (current != null)
        {
            if (current is DllNotFoundException)
                return true;
            current = current.InnerException;
        }
        return false;
    }
}