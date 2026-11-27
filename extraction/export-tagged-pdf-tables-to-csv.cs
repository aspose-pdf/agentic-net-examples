using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    // Escapes a CSV field according to RFC 4180
    static string EscapeCsv(string field)
    {
        if (field == null) return "";
        bool mustQuote = field.Contains(",") || field.Contains("\"") || field.Contains("\r") || field.Contains("\n");
        if (mustQuote)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append('"');
            foreach (char c in field)
            {
                if (c == '"')
                    sb.Append("\"\""); // escape double quote by doubling it
                else
                    sb.Append(c);
            }
            sb.Append('"');
            return sb.ToString();
        }
        return field;
    }

    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputFolder = "TablesCsv";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // The Document.IsTagged property does not exist. Use TaggedContent null‑check instead.
                if (doc.TaggedContent == null)
                {
                    Console.WriteLine("Document is not tagged. No table structure available.");
                    return;
                }

                ITaggedContent taggedContent = doc.TaggedContent;
                StructureElement root = taggedContent.RootElement;

                // Find all TableElement objects recursively
                var tables = root.FindElements<TableElement>(true);
                int tableIndex = 1;

                foreach (TableElement table in tables)
                {
                    string csvPath = Path.Combine(outputFolder, $"table_{tableIndex}.csv");
                    using (StreamWriter writer = new StreamWriter(csvPath, false, Encoding.UTF8))
                    {
                        // Process Table Header, Body, Footer in order
                        ProcessTableSection(table, writer, typeof(TableTHeadElement));
                        ProcessTableSection(table, writer, typeof(TableTBodyElement));
                        ProcessTableSection(table, writer, typeof(TableTFootElement));
                    }

                    Console.WriteLine($"Table {tableIndex} exported to '{csvPath}'.");
                    tableIndex++;
                }

                if (tables.Count == 0)
                {
                    Console.WriteLine("No tables found in the tagged PDF.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Writes rows of a specific section (thead, tbody, tfoot) to the CSV writer
    static void ProcessTableSection(TableElement table, StreamWriter writer, Type sectionType)
    {
        foreach (Element child in table.ChildElements)
        {
            if (child.GetType() == sectionType)
            {
                // Section element (e.g., TableTHeadElement)
                foreach (Element rowElem in child.ChildElements)
                {
                    if (rowElem is TableTRElement row)
                    {
                        var cellValues = new System.Collections.Generic.List<string>();
                        foreach (Element cellElem in row.ChildElements)
                        {
                            // Cells can be TH or TD elements
                            if (cellElem is TableTHElement th)
                            {
                                cellValues.Add(EscapeCsv(th.ActualText ?? ""));
                            }
                            else if (cellElem is TableTDElement td)
                            {
                                cellValues.Add(EscapeCsv(td.ActualText ?? ""));
                            }
                        }
                        writer.WriteLine(string.Join(",", cellValues));
                    }
                }
            }
        }
    }
}
