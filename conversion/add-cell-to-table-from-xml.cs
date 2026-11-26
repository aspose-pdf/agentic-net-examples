using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string outputPdf = "output.pdf";
        const string cellContent = "New Cell Content";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the PDF structure from the XML file
        XmlLoadOptions loadOptions = new XmlLoadOptions();
        using (Document doc = new Document(xmlPath, loadOptions))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("Document contains no pages.");
                return;
            }

            // Access the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Locate the first Table object on the page
            Table table = null;
            foreach (var paragraph in page.Paragraphs)
            {
                if (paragraph is Table tbl)
                {
                    table = tbl;
                    break;
                }
            }

            if (table == null)
            {
                Console.Error.WriteLine("No table found in the document.");
                return;
            }

            // Ensure the table has at least one row
            if (table.Rows.Count == 0)
            {
                Console.Error.WriteLine("The table has no rows.");
                return;
            }

            // Get the first row (1‑based indexing) and add a new cell
            Row row = table.Rows[1];
            Cell newCell = new Cell();

            // Add the desired text to the new cell
            newCell.Paragraphs.Add(new TextFragment(cellContent));

            // Append the cell to the row
            row.Cells.Add(newCell);

            // Save the modified document as PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }
}