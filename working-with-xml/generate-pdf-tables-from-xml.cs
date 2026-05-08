using System;
using System.Data;
using System.IO;
using Aspose.Pdf;

class ReportGenerator
{
    static void Main()
    {
        const string xmlPath   = "report_data.xml";   // Input XML containing one or more DataTables
        const string outputPdf = "report.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        try
        {
            // Load XML into a DataSet (DataSet.ReadXml parses the XML into DataTables)
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(xmlPath);

            // Load the (empty) PDF document – we will create pages as needed
            using (Document pdfDoc = new Document())
            {
                // Ensure at least one page exists
                if (pdfDoc.Pages.Count == 0)
                    pdfDoc.Pages.Add();

                // Iterate over each DataTable found in the XML
                foreach (DataTable tableData in dataSet.Tables)
                {
                    // Create a new Table object
                    Table pdfTable = new Table
                    {
                        // Optional styling – adjust as required
                        Border = new BorderInfo(BorderSide.All, 0.5f),
                        DefaultCellPadding = new MarginInfo(5, 5, 5, 5),
                        DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f)
                    };

                    // Import the DataTable into the PDF Table.
                    // Parameters: (DataTable, import column names, first row, first column)
                    pdfTable.ImportDataTable(tableData, true, 0, 0);

                    // Add the table to the first page (or create a new page per table if desired)
                    pdfDoc.Pages[1].Paragraphs.Add(pdfTable);
                }

                // Save the resulting PDF
                pdfDoc.Save(outputPdf);
            }

            Console.WriteLine($"Report generated successfully: {outputPdf}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error generating report: {ex.Message}");
        }
    }
}