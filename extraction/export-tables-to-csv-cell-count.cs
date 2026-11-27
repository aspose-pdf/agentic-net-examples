using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class ExportTablesToCsv
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputCsvPath = "tables.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block (deterministic disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure the PDF is tagged; otherwise table structure is not accessible
            if (doc.TaggedContent == null)
            {
                Console.WriteLine("The PDF is not a tagged document; no table structure can be extracted.");
                return;
            }

            // Access the tagged content
            ITaggedContent taggedContent = doc.TaggedContent;

            // Root element of the structure tree
            StructureElement root = taggedContent.RootElement;

            // Find all table elements recursively
            var tables = root.FindElements<TableElement>(true);

            using (StreamWriter writer = new StreamWriter(outputCsvPath, false, Encoding.UTF8))
            {
                // Optional header line
                writer.WriteLine("TableIndex,RowIndex,Cell1,Cell2,Cell3,...,CellCount");

                int tableIndex = 0;
                foreach (TableElement table in tables)
                {
                    tableIndex++;

                    // Iterate over each table row (TableTRElement)
                    int rowIndex = 0;
                    foreach (Element rowElement in table.ChildElements)
                    {
                        if (rowElement is TableTRElement row)
                        {
                            rowIndex++;

                            // Collect cell texts for the current row
                            var cellTexts = new System.Collections.Generic.List<string>();
                            foreach (Element cellElement in row.ChildElements)
                            {
                                // Table header cells (TH) and data cells (TD) share the same base type
                                if (cellElement is TableTHElement th)
                                    cellTexts.Add(th.ActualText ?? string.Empty);
                                else if (cellElement is TableTDElement td)
                                    cellTexts.Add(td.ActualText ?? string.Empty);
                            }

                            // Number of cells in this row
                            int cellCount = cellTexts.Count;

                            // Build CSV line: TableIndex,RowIndex,cell1,cell2,...,CellCount
                            StringBuilder csvLine = new StringBuilder();
                            csvLine.Append(tableIndex).Append(',');
                            csvLine.Append(rowIndex);
                            foreach (string txt in cellTexts)
                            {
                                // Simple CSV escaping: wrap in double quotes if it contains a comma or quote
                                string escaped = txt;
                                if (escaped.Contains("\""))
                                    escaped = escaped.Replace("\"", "\"\"");
                                if (escaped.Contains(",") || escaped.Contains("\""))
                                    escaped = $"\"{escaped}\"";

                                csvLine.Append(',').Append(escaped);
                            }
                            csvLine.Append(',').Append(cellCount);

                            writer.WriteLine(csvLine.ToString());
                        }
                    }
                }
            }

            Console.WriteLine($"Tables exported to CSV: {outputCsvPath}");
        }
    }
}
