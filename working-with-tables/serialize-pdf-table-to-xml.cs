using System;
using System.Data;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths for the output files
        const string pdfPath = "table_output.pdf";
        const string xmlPath = "table_output.xml";

        // Create a sample DataTable to populate the PDF table
        DataTable dataTable = new DataTable("Sample");
        dataTable.Columns.Add("Product", typeof(string));
        dataTable.Columns.Add("Quantity", typeof(int));
        dataTable.Columns.Add("Price", typeof(decimal));

        dataTable.Rows.Add("Apple", 10, 0.5m);
        dataTable.Rows.Add("Banana", 5, 0.3m);
        dataTable.Rows.Add("Cherry", 20, 1.2m);

        // Create a new PDF document
        using (Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document())
        {
            // Add a page to the document
            Aspose.Pdf.Page page = pdfDocument.Pages.Add();

            // Create a table and import the DataTable contents
            Aspose.Pdf.Table table = new Aspose.Pdf.Table();
            table.ColumnWidths = "150 100 100"; // optional column widths
            table.ImportDataTable(dataTable, true, 0, 0);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF (optional, just to have a visual representation)
            pdfDocument.Save(pdfPath);
        }

        // Re-open the PDF to read the table structure for XML serialization
        using (Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(pdfPath))
        {
            // Assuming the table is on the first page and is the first paragraph
            Aspose.Pdf.Page page = pdfDocument.Pages[1];
            Aspose.Pdf.Table table = null;

            foreach (Aspose.Pdf.BaseParagraph paragraph in page.Paragraphs)
            {
                if (paragraph is Aspose.Pdf.Table tbl)
                {
                    table = tbl;
                    break;
                }
            }

            if (table == null)
            {
                Console.Error.WriteLine("No table found in the PDF.");
                return;
            }

            // Build XML from the table rows and cells
            XElement rootElement = new XElement("Table");

            foreach (Aspose.Pdf.Row row in table.Rows)
            {
                XElement rowElement = new XElement("Row");

                foreach (Aspose.Pdf.Cell cell in row.Cells)
                {
                    // Extract the textual content of the cell.
                    // When a table is created via ImportDataTable, each cell contains a single TextFragment.
                    string cellText = string.Empty;
                    if (cell.Paragraphs != null && cell.Paragraphs.Count > 0 && cell.Paragraphs[0] is TextFragment tf)
                    {
                        cellText = tf.Text ?? string.Empty;
                    }

                    XElement cellElement = new XElement("Cell", cellText);
                    rowElement.Add(cellElement);
                }

                rootElement.Add(rowElement);
            }

            XDocument xmlDocument = new XDocument(rootElement);
            xmlDocument.Save(xmlPath);
        }

        Console.WriteLine($"PDF saved to '{pdfPath}'.");
        Console.WriteLine($"Table serialized to XML at '{xmlPath}'.");
    }
}
