using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string xmlPath   = "input.xml";
        const string outputPdf = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the PDF structure from an XML file
        XmlLoadOptions loadOptions = new XmlLoadOptions();
        using (Document doc = new Document(xmlPath, loadOptions))
        {
            // Ensure the document has at least one page (1‑based indexing)
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The document contains no pages.");
                return;
            }

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
                Console.Error.WriteLine("No Table found on the first page.");
                return;
            }

            // Add a new row to the table
            Row newRow = table.Rows.Add();

            // Add first cell with text
            Cell cell1 = newRow.Cells.Add();
            cell1.Paragraphs.Add(new TextFragment("Cell 1"));

            // Add second cell with text
            Cell cell2 = newRow.Cells.Add();
            cell2.Paragraphs.Add(new TextFragment("Cell 2"));

            // Save the modified document as PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF successfully saved to '{outputPdf}'.");
    }
}