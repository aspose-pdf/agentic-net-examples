using System;
using System.Data;
using System.IO;
using Aspose.Pdf;

class ReportGenerator
{
    static void Main()
    {
        // Paths for input XML and output PDF
        const string xmlPath = "report_data.xml";
        const string pdfPath = "report.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML into a DataSet (tabular data)
        DataSet dataSet = new DataSet();
        dataSet.ReadXml(xmlPath);

        // Assume the first DataTable contains the data to report
        if (dataSet.Tables.Count == 0)
        {
            Console.Error.WriteLine("No tables found in the XML file.");
            return;
        }

        DataTable tableData = dataSet.Tables[0];

        // Create a new PDF document
        using (Document pdfDoc = new Document())
        {
            // Add a page to the document
            Page page = pdfDoc.Pages.Add();

            // Create a Table object
            Table pdfTable = new Table();

            // Optional: set table appearance
            // Use BorderInfo (the correct class for table borders in recent Aspose.Pdf versions)
            pdfTable.DefaultCellBorder = new BorderInfo(BorderSide.All);
            pdfTable.DefaultCellPadding = new MarginInfo(5, 5, 5, 5);
            pdfTable.Alignment = HorizontalAlignment.Center;

            // Import the DataTable into the PDF table.
            // Parameters: (DataTable, import column names, first row, first column)
            pdfTable.ImportDataTable(tableData, true, 0, 0);

            // Add the table to the page
            page.Paragraphs.Add(pdfTable);

            // Save the PDF document
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"Report generated: {pdfPath}");
    }
}
