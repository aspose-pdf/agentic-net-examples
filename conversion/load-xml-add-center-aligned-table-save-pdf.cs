using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string outputPdf = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load PDF structure from the XML representation
        using (Document doc = new Document(xmlPath, new XmlLoadOptions()))
        {
            // Ensure there is at least one page to place the table on
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Create a table and set its alignment to Center
            Table table = new Table
            {
                Alignment = HorizontalAlignment.Center
            };

            // Example header row
            Row headerRow = table.Rows.Add();
            headerRow.Cells.Add("Header 1");
            headerRow.Cells.Add("Header 2");

            // Example data row
            Row dataRow = table.Rows.Add();
            dataRow.Cells.Add("Value 1");
            dataRow.Cells.Add("Value 2");

            // Add the table to the first page's paragraph collection
            doc.Pages[1].Paragraphs.Add(table);

            // Save the PDF document
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }
}