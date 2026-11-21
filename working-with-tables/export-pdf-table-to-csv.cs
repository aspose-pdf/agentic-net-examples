using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class ExportTableToCsv
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputCsv = "tables.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Access tagged content (requires the PDF to be tagged)
            ITaggedContent tagged = doc.TaggedContent;
            if (tagged == null)
            {
                Console.Error.WriteLine("Document does not contain tagged content.");
                return;
            }

            // Root of the structure tree
            StructureElement root = tagged.RootElement;

            // Find all table elements in the document (recursive search)
            var tables = root.FindElements<TableElement>(true);

            using (StreamWriter writer = new StreamWriter(outputCsv, false, Encoding.UTF8))
            {
                foreach (TableElement table in tables)
                {
                    // Find all row elements (TableTRElement) within the table
                    var rows = table.FindElements<TableTRElement>(true);
                    foreach (TableTRElement row in rows)
                    {
                        // Collect cell texts for the current row
                        var cellTexts = new System.Collections.Generic.List<string>();
                        foreach (Element cell in row.ChildElements)
                        {
                            // Cells are either TableTHElement (header) or TableTDElement (data)
                            string text = (cell as StructureElement)?.ActualText ?? string.Empty;
                            // Escape CSV special characters
                            if (text.Contains("\""))
                                text = text.Replace("\"", "\"\"");
                            if (text.Contains(",") || text.Contains("\"") || text.Contains("\n") || text.Contains("\r"))
                                text = $"\"{text}\"";
                            cellTexts.Add(text);
                        }

                        // Write the CSV line
                        writer.WriteLine(string.Join(",", cellTexts));
                    }
                }
            }

            Console.WriteLine($"Table data exported to CSV: {outputCsv}");
        }
    }
}