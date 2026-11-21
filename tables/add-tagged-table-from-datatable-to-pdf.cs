using System;
using System.IO;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Create a DataTable with 8 rows and 3 columns
        DataTable dt = new DataTable();
        dt.Columns.Add("Column1");
        dt.Columns.Add("Column2");
        dt.Columns.Add("Column3");
        for (int i = 1; i <= 8; i++)
        {
            dt.Rows.Add($"R{i}C1", $"R{i}C2", $"R{i}C3");
        }

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and add a tagged table
        using (Document doc = new Document(inputPath))
        {
            ITaggedContent tagged = doc.TaggedContent;

            // Root element of the structure tree
            StructureElement root = tagged.RootElement;

            // Create a table element
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table";
            table.RepeatingRowsCount = 1; // Header repeats on each page
            root.AppendChild(table);

            // Create table header
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);
            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);

            string[] headers = { "Header 1", "Header 2", "Header 3" };
            foreach (string h in headers)
            {
                TableTHElement th = tagged.CreateTableTHElement();
                th.SetText(h);
                headerRow.AppendChild(th);
            }

            // Create table body
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            // Populate rows from the DataTable
            foreach (DataRow dr in dt.Rows)
            {
                TableTRElement row = tagged.CreateTableTRElement();
                tbody.AppendChild(row);
                for (int col = 0; col < dt.Columns.Count; col++)
                {
                    TableTDElement td = tagged.CreateTableTDElement();
                    td.SetText(dr[col].ToString());
                    row.AppendChild(td);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with table saved to '{outputPath}'.");
    }
}