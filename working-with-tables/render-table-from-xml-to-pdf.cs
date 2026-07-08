using System;
using System.Data;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the XML definition and the resulting PDF.
        const string xmlPath = "TableDefinition.xml";
        const string pdfPath = "RenderedTable.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML into a DataSet. The XML is expected to represent a table
        // (e.g., rows and columns) that can be read into a DataTable.
        DataSet dataSet = new DataSet();
        dataSet.ReadXml(xmlPath);

        // Assume the first DataTable contains the table data.
        if (dataSet.Tables.Count == 0)
        {
            Console.Error.WriteLine("No tables found in the XML file.");
            return;
        }

        DataTable sourceTable = dataSet.Tables[0];

        // Create a new PDF document (creation rule).
        using (Document pdfDocument = new Document())
        {
            // Add a page to the document.
            Page page = pdfDocument.Pages.Add();

            // Create a Table object.
            Table table = new Table();

            // Import the DataTable into the Aspose.Pdf.Table.
            // - true  : import column names as the first row.
            // - 0     : start importing at the first row of the target table.
            // - 0     : start importing at the first column of the target table.
            table.ImportDataTable(sourceTable, true, 0, 0);

            // Optional styling (example: set a border and background color).
            table.Border = new BorderInfo(BorderSide.All, 0.5f);
            table.BackgroundColor = Aspose.Pdf.Color.LightGray;

            // Add the table to the page's paragraph collection.
            page.Paragraphs.Add(table);

            // Save the PDF (saving rule).
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"Table rendered and saved to '{pdfPath}'.");
    }
}