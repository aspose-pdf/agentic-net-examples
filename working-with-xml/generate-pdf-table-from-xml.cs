using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class ReportGenerator
{
    static void Main()
    {
        // Paths for the source XML data and the output PDF report
        const string xmlPath = "reportData.xml";
        const string pdfPath = "report.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML data file not found: {xmlPath}");
            return;
        }

        // Load the XML file using XmlLoadOptions (required for Aspose.Pdf)
        XmlLoadOptions xmlLoadOptions = new XmlLoadOptions();
        using (Document doc = new Document(xmlPath, xmlLoadOptions))
        {
            // Parse the same XML into a DataSet to obtain a DataTable
            // (DataSet.ReadXml can read the XML structure into relational tables)
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(xmlPath);
            if (dataSet.Tables.Count == 0)
            {
                Console.Error.WriteLine("No tables found in the XML data.");
                return;
            }

            DataTable dataTable = dataSet.Tables[0]; // Use the first table

            // Create a new page for the report
            Page page = doc.Pages.Add();

            // Create a Table object and set basic appearance
            Aspose.Pdf.Table table = new Aspose.Pdf.Table
            {
                // Adjust columns to fit the content automatically
                ColumnAdjustment = ColumnAdjustment.AutoFitToContent,
                // Optional: set a border for visual clarity
                Border = new BorderInfo(BorderSide.All, 1, Aspose.Pdf.Color.Black)
            };

            // Import the DataTable into the Aspose.Pdf.Table
            // The first row will contain column names, start at row 0, column 0.
            table.ImportDataTable(dataTable, true, 0, 0);

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Save the resulting PDF document
            doc.Save(pdfPath);
        }

        Console.WriteLine($"Report generated successfully: {pdfPath}");
    }
}
