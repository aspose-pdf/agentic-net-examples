using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputJson = "tables.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        using (Document doc = new Document(inputPdf))
        {
            // Check if the PDF is tagged by verifying TaggedContent is not null
            if (doc.TaggedContent == null)
            {
                Console.WriteLine("The document is not a tagged PDF. No table structure can be extracted.");
                return;
            }

            ITaggedContent taggedContent = doc.TaggedContent;
            StructureElement root = taggedContent.RootElement;

            var tablesInfo = new List<object>();
            int tableIndex = 0;

            // Find all tables recursively
            foreach (TableElement table in root.FindElements<TableElement>(true))
            {
                // Count rows: each TableTRElement represents a row
                IList<TableTRElement> rows = table.FindElements<TableTRElement>(true);
                int rowCount = rows.Count;

                // Determine the maximum number of columns across all rows
                int maxColumnCount = 0;
                foreach (TableTRElement row in rows)
                {
                    int cellCount = row.FindElements<TableTHElement>(true).Count +
                                    row.FindElements<TableTDElement>(true).Count;
                    if (cellCount > maxColumnCount)
                        maxColumnCount = cellCount;
                }

                tablesInfo.Add(new
                {
                    TableIndex = tableIndex++,
                    RowCount = rowCount,
                    ColumnCount = maxColumnCount
                });
            }

            // Serialize the result to JSON (indented for readability)
            string json = JsonSerializer.Serialize(tablesInfo, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(outputJson, json);
            Console.WriteLine($"Table information written to '{outputJson}'.");
        }
    }
}
