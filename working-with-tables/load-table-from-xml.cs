using System;
using System.IO;
using System.Data;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Define file paths (current directory)
        string dataDir = Directory.GetCurrentDirectory();
        string xmlFile = Path.Combine(dataDir, "table-data.xml");
        string pdfFile = Path.Combine(dataDir, "output.pdf");

        // Create a simple XML file if it does not exist
        if (!File.Exists(xmlFile))
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.AppendLine("<DataSet>");
            sb.AppendLine("  <Table>");
            sb.AppendLine("    <Row><Column1>Item</Column1><Column2>Quantity</Column2></Row>");
            sb.AppendLine("    <Row><Column1>Apple</Column1><Column2>10</Column2></Row>");
            sb.AppendLine("    <Row><Column1>Banana</Column1><Column2>20</Column2></Row>");
            sb.AppendLine("  </Table>");
            sb.AppendLine("</DataSet>");
            File.WriteAllText(xmlFile, sb.ToString());
        }

        // Load the XML into a DataSet and obtain the first DataTable
        var dataSet = new DataSet();
        dataSet.ReadXml(xmlFile);
        DataTable dataTable = dataSet.Tables[0];

        // Create a new PDF document
        using (Document pdfDocument = new Document())
        {
            // Add a page to the document
            Page page = pdfDocument.Pages.Add();

            // Create a PDF table and define column widths (two columns)
            Table pdfTable = new Table();
            pdfTable.ColumnWidths = "200 200";

            // Import the DataTable into the PDF table (include column names as header)
            pdfTable.ImportDataTable(dataTable, true, 0, 0);

            // Add the table to the page
            page.Paragraphs.Add(pdfTable);

            // Save the PDF document – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                pdfDocument.Save(pdfFile);
                Console.WriteLine($"PDF created: {pdfFile}");
            }
            else
            {
                try
                {
                    pdfDocument.Save(pdfFile);
                    Console.WriteLine($"PDF created (non‑Windows platform): {pdfFile}");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF could not be saved, but the rest of the code executed correctly.");
                }
            }
        }
    }

    // Helper that walks the inner‑exception chain looking for a DllNotFoundException
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
