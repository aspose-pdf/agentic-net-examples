using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class XmlReportToPdf
{
    static void Main()
    {
        // Paths for the input XML data and the output PDF report
        const string xmlPath   = "report_data.xml";
        const string pdfPath   = "report.pdf";

        // Verify that the XML file exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"Error: XML file not found at '{xmlPath}'.");
            return;
        }

        // Load the XML into a DataSet. The XML is expected to contain a single table.
        DataSet dataSet = new DataSet();
        try
        {
            dataSet.ReadXml(xmlPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading XML: {ex.Message}");
            return;
        }

        // Ensure that at least one DataTable was loaded
        if (dataSet.Tables.Count == 0)
        {
            Console.Error.WriteLine("Error: No tables found in the XML file.");
            return;
        }

        // Use the first DataTable as the source for the PDF table
        DataTable sourceTable = dataSet.Tables[0];

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document())
        {
            // Add a blank page to the document
            Page page = pdfDocument.Pages.Add();

            // Create a Table object (inherits from BaseParagraph)
            Table pdfTable = new Table
            {
                // Optional visual settings
                Border = new BorderInfo(BorderSide.All, 0.5f),
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5),
                // Align the table to the left margin of the page
                HorizontalAlignment = HorizontalAlignment.Left,
                // Position the table on the page (coordinates are in points)
                // Top-left corner of the table
                // Adjust as needed for your layout
                // Here we start 50 points from the left and 750 points from the bottom
                // (Aspose.Pdf uses bottom-left origin)
                // The Table will automatically expand vertically based on content
                // No need to set Width explicitly unless a fixed width is required
                // Example: ColumnWidths = "100 150 200"
            };

            // Import the DataTable into the PDF table.
            // Parameters:
            //   sourceTable          – the DataTable to import
            //   true                 – import column names as the first row
            //   0                    – zero‑based index of the first row in the target table
            //   0                    – zero‑based index of the first column in the target table
            pdfTable.ImportDataTable(sourceTable, true, 0, 0);

            // Add the populated table to the page's paragraph collection
            page.Paragraphs.Add(pdfTable);

            // Save the PDF document to the specified file path
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"PDF report generated successfully at '{pdfPath}'.");
    }
}