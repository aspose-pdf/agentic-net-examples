using System;
using System.Data;
using System.IO;
using Aspose.Pdf;               // Core PDF API
using Aspose.Pdf.Text;          // For text-related types if needed

class Program
{
    static void Main()
    {
        const string xmlPath = "report.xml";   // Input XML containing tabular data
        const string pdfPath = "report.pdf";   // Output PDF file

        // Verify that the XML source exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML into a DataSet; the first DataTable is assumed to hold the report data
        DataSet dataSet = new DataSet();
        dataSet.ReadXml(xmlPath);
        if (dataSet.Tables.Count == 0)
        {
            Console.Error.WriteLine("No tables found in the XML file.");
            return;
        }

        DataTable dataTable = dataSet.Tables[0];   // Use the first table

        // Create a new PDF document (lifecycle: create)
        using (Document pdfDoc = new Document())
        {
            // Add a single page to host the table
            Page page = pdfDoc.Pages.Add();

            // Instantiate a Table object (lifecycle: create)
            Table table = new Table
            {
                // Optional visual styling
                Border = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black),
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Gray),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
                // Column widths can be set via table.ColumnWidths if required
            };

            // Import the DataTable into the Aspose.Pdf.Table
            // Parameters:
            //   dataTable                – source data
            //   true                     – import column names as the first row
            //   0                        – start at the first row of the PDF table (zero‑based)
            //   0                        – start at the first column of the PDF table (zero‑based)
            table.ImportDataTable(dataTable, true, 0, 0);

            // Add the populated table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF document to disk (lifecycle: save)
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF report generated successfully: {pdfPath}");
    }
}