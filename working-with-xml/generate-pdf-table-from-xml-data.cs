using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

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

        // Load XML into a DataSet (which contains DataTables)
        DataSet dataSet = new DataSet();
        dataSet.ReadXml(xmlPath);

        // Assume the first DataTable contains the data we want to render
        if (dataSet.Tables.Count == 0)
        {
            Console.Error.WriteLine("No tables found in the XML file.");
            return;
        }

        DataTable tableData = dataSet.Tables[0];

        // Load the (empty) PDF document using XmlLoadOptions (required for XML input)
        XmlLoadOptions loadOptions = new XmlLoadOptions(); // creates XmlLoadOptions without XSL
        using (Document pdfDocument = new Document(xmlPath, loadOptions))
        {
            // Create a new Table instance
            Table pdfTable = new Table
            {
                // Optional visual settings
                Border = new BorderInfo(BorderSide.All, 0.5f),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5),
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f)
            };

            // Import the DataTable into the PDF Table
            // Parameters: (DataTable, import column names, first row, first column)
            pdfTable.ImportDataTable(tableData, true, 0, 0);

            // Add the table to the first page (creates page if none exists)
            Page page = pdfDocument.Pages.Count > 0 ? pdfDocument.Pages[1] : pdfDocument.Pages.Add();
            page.Paragraphs.Add(pdfTable);

            // Save the resulting PDF
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"PDF report generated: {pdfPath}");
    }
}