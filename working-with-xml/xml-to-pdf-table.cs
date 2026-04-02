using System;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;

namespace XmlToPdfTableExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Sample XML containing tabular data
            string xmlContent = "<Root><Record><Name>John Doe</Name><Age>30</Age></Record><Record><Name>Jane Smith</Name><Age>25</Age></Record></Root>";

            // Load XML into a DataSet and obtain the first DataTable
            DataSet dataSet = new DataSet();
            using (StringReader stringReader = new StringReader(xmlContent))
            {
                dataSet.ReadXml(stringReader);
            }
            DataTable dataTable = dataSet.Tables[0];

            // Create a new PDF document
            using (Document pdfDocument = new Document())
            {
                // Add a page to the document
                pdfDocument.Pages.Add();

                // Create a table and import the DataTable content
                Table pdfTable = new Table
                {
                    // Optional: set column widths (in points)
                    ColumnWidths = "150 100"
                };
                // Import data: include column names as the first row, start at row 0, column 0
                pdfTable.ImportDataTable(dataTable, true, 0, 0);

                // Add the table to the first page
                pdfDocument.Pages[1].Paragraphs.Add(pdfTable);

                string outputPath = "output.pdf";

                // Guard Document.Save against missing GDI+ (libgdiplus) on non‑Windows platforms
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    pdfDocument.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}'.");
                }
                else
                {
                    try
                    {
                        pdfDocument.Save(outputPath);
                        Console.WriteLine($"PDF saved to '{outputPath}' (non‑Windows platform).");
                    }
                    catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                    {
                        Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                          "The PDF was not saved because Aspose.Pdf requires GDI+ for rendering.");
                    }
                }
            }
        }

        // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus)
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
}
