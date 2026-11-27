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
        const string inputPdf = "input.pdf";
        const string outputCsv = "tables.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        StringBuilder csvBuilder = new StringBuilder();

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Access the tagged content tree
            ITaggedContent tagged = doc.TaggedContent;

            // Retrieve all TableElement objects recursively
            var tables = tagged.RootElement.FindElements<TableElement>(true);

            int tableIndex = 0;
            foreach (TableElement table in tables)
            {
                tableIndex++;

                // Retrieve all rows (TableTRElement) within the current table
                var rows = table.FindElements<TableTRElement>(true);
                int rowIndex = 0;

                foreach (TableTRElement row in rows)
                {
                    rowIndex++;

                    // Concatenate text from each cell (TH or TD) in the row
                    StringBuilder cellTextBuilder = new StringBuilder();
                    foreach (Element cell in row.ChildElements)
                    {
                        if (cell is TableTHElement th)
                            AppendCellText(th, cellTextBuilder);
                        else if (cell is TableTDElement td)
                            AppendCellText(td, cellTextBuilder);
                    }

                    // CSV line: tableIndex,rowIndex,concatenatedCellText
                    // Escape double quotes by doubling them
                    string escapedText = cellTextBuilder.ToString().Replace("\"", "\"\"");
                    csvBuilder.AppendLine($"{tableIndex},{rowIndex},\"{escapedText}\"");
                }
            }
        }

        // Write the CSV content to the output file (UTF‑8)
        File.WriteAllText(outputCsv, csvBuilder.ToString(), Encoding.UTF8);
        Console.WriteLine($"CSV file saved to '{outputCsv}'.");
    }

    // Helper: extracts the ActualText of a structure element and appends it with a space separator
    private static void AppendCellText(StructureElement element, StringBuilder sb)
    {
        string text = element.ActualText ?? string.Empty;
        if (sb.Length > 0)
            sb.Append(' ');
        sb.Append(text);
    }
}