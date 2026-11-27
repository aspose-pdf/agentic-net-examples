using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string csvPath   = "ThirdTable.csv";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Table extraction requires a tagged PDF. Check via TaggedContent property.
            if (doc.TaggedContent == null)
            {
                Console.Error.WriteLine("PDF is not tagged. Cannot extract tables.");
                return;
            }

            ITaggedContent tagged = doc.TaggedContent;

            // Find all TableElement objects in the structure tree (recursive search)
            var tables = tagged.RootElement.FindElements<TableElement>(true);
            if (tables.Count < 3)
            {
                Console.Error.WriteLine($"Less than 3 tables found ({tables.Count}).");
                return;
            }

            // Zero‑based index: the third table is at position 2
            TableElement thirdTable = tables[2];

            // Collect all rows (TableTRElement) from possible sections: thead, tbody, tfoot
            List<TableTRElement> rows = new List<TableTRElement>();

            void AddRowsFromSection(Element section)
            {
                if (section is TableTHeadElement thead)
                {
                    foreach (Element child in thead.ChildElements)
                        if (child is TableTRElement tr) rows.Add(tr);
                }
                else if (section is TableTBodyElement tbody)
                {
                    foreach (Element child in tbody.ChildElements)
                        if (child is TableTRElement tr) rows.Add(tr);
                }
                else if (section is TableTFootElement tfoot)
                {
                    foreach (Element child in tfoot.ChildElements)
                        if (child is TableTRElement tr) rows.Add(tr);
                }
            }

            foreach (Element child in thirdTable.ChildElements)
                AddRowsFromSection(child);

            // Write the extracted cells to a CSV file
            using (StreamWriter writer = new StreamWriter(csvPath))
            {
                foreach (TableTRElement row in rows)
                {
                    List<string> cells = new List<string>();
                    foreach (Element cell in row.ChildElements)
                    {
                        if (cell is TableTHElement th)
                            cells.Add(EscapeCsv(th.ActualText));
                        else if (cell is TableTDElement td)
                            cells.Add(EscapeCsv(td.ActualText));
                    }
                    writer.WriteLine(string.Join(",", cells));
                }
            }

            Console.WriteLine($"Third table exported to {csvPath}");
        }
    }

    // Helper to escape CSV fields containing commas, quotes or newlines
    static string EscapeCsv(string input)
    {
        if (input == null) return "";
        bool mustQuote = input.Contains(",") || input.Contains("\"") || input.Contains("\n") || input.Contains("\r");
        if (mustQuote)
        {
            string escaped = input.Replace("\"", "\"\"");
            return $"\"{escaped}\"";
        }
        return input;
    }
}
