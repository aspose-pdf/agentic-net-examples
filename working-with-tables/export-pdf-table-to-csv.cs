using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;               // ITaggedContent
using Aspose.Pdf.LogicalStructure;    // Structure elements

class Program
{
    static void Main(string[] args)
    {
        const string inputPdf = "input.pdf";
        const string outputCsv = "tables.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Access tagged content (required to work with logical structure)
            ITaggedContent tagged = doc.TaggedContent;

            // Find all table elements in the document (recursive search)
            var tables = tagged.RootElement.FindElements<TableElement>(true);

            using (StreamWriter writer = new StreamWriter(outputCsv))
            {
                foreach (TableElement table in tables)
                {
                    // Process table header rows (if any)
                    ProcessTableSection(table.CreateTHead(), writer);
                    // Process table body rows
                    ProcessTableSection(table.CreateTBody(), writer);
                    // Process table footer rows (optional)
                    ProcessTableSection(table.CreateTFoot(), writer);
                }
            }

            Console.WriteLine($"CSV export completed: {outputCsv}");
        }
    }

    // Helper to walk through a table section (thead, tbody, tfoot) and write rows to CSV
    private static void ProcessTableSection(Element section, StreamWriter writer)
    {
        if (section == null) return;

        // Each child of the section is a TableTRElement (a table row)
        foreach (Element rowElem in section.ChildElements)
        {
            if (rowElem is TableTRElement row)
            {
                // Collect text from each cell in the row
                var cellTexts = new List<string>();

                foreach (Element cellElem in row.ChildElements)
                {
                    // Cells can be TH (header) or TD (data) elements
                    if (cellElem is TableTHElement th)
                        cellTexts.Add(EscapeCsv(th.ActualText));
                    else if (cellElem is TableTDElement td)
                        cellTexts.Add(EscapeCsv(td.ActualText));
                }

                // Write the CSV line
                writer.WriteLine(string.Join(",", cellTexts));
            }
        }
    }

    // Escape commas and quotes for CSV compliance
    private static string EscapeCsv(string text)
    {
        if (text == null) return string.Empty;
        bool mustQuote = text.Contains(",") || text.Contains("\"") || text.Contains("\n");
        if (mustQuote)
        {
            string escaped = text.Replace("\"", "\"\"");
            return $"\"{escaped}\"";
        }
        return text;
    }
}
