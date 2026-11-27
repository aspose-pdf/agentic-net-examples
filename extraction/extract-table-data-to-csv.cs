using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputCsv = "tables.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Access tagged content (required for structural elements like tables)
            ITaggedContent tagged = doc.TaggedContent;
            StructureElement root = tagged.RootElement;

            // Prepare CSV header
            StringBuilder csv = new StringBuilder();
            csv.AppendLine("TableId,Row,Column,Text");

            // Find all Table elements in the structure tree
            var tables = root.FindElements<TableElement>(true);
            int tableId = 0;

            foreach (TableElement table in tables)
            {
                tableId++;
                int rowIndex = 0;

                // Find all rows (TableTRElement) that belong to this table
                var rows = table.FindElements<TableTRElement>(true);
                foreach (TableTRElement row in rows)
                {
                    rowIndex++;
                    int colIndex = 0;

                    // Iterate over cells: TableTHElement (header) or TableTDElement (data)
                    foreach (Element cell in row.ChildElements)
                    {
                        string cellText = null;

                        if (cell is TableTHElement th)
                        {
                            colIndex++;
                            cellText = th.ActualText ?? string.Empty;
                        }
                        else if (cell is TableTDElement td)
                        {
                            colIndex++;
                            cellText = td.ActualText ?? string.Empty;
                        }

                        if (cellText != null)
                        {
                            // Escape double quotes for CSV compliance
                            string escaped = cellText.Replace("\"", "\"\"");
                            csv.AppendLine($"{tableId},{rowIndex},{colIndex},\"{escaped}\"");
                        }
                    }
                }
            }

            // Write CSV to disk
            File.WriteAllText(outputCsv, csv.ToString(), Encoding.UTF8);
            Console.WriteLine($"Table data extracted to '{outputCsv}'.");
        }
    }
}