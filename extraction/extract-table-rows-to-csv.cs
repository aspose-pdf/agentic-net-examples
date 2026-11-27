using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputCsv = "output.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        using (Document doc = new Document(inputPdf))
        {
            // Check if the PDF has a tagged structure tree
            if (doc.TaggedContent == null)
            {
                Console.WriteLine("Document is not tagged; cannot extract table structure.");
                return;
            }

            ITaggedContent tagged = doc.TaggedContent;

            // Find all tables in the document (recursive search)
            var tables = tagged.RootElement.FindElements<TableElement>(true);

            using (StreamWriter writer = new StreamWriter(outputCsv))
            {
                foreach (TableElement table in tables)
                {
                    // Each table may contain THead, TBody, TFoot sections.
                    foreach (Element section in table.ChildElements)
                    {
                        ProcessSection(section, writer);
                    }
                }
            }
        }

        Console.WriteLine($"CSV file written to '{outputCsv}'.");
    }

    // Process a table section (THead, TBody, TFoot) and write each row to CSV
    static void ProcessSection(Element section, StreamWriter writer)
    {
        // Section types inherit from StructureElement; rows are TableTRElement objects
        foreach (Element rowElem in section.ChildElements)
        {
            if (rowElem is TableTRElement row)
            {
                // Concatenate texts of all cells in the row
                string concatenated = string.Empty;
                foreach (Element cellElem in row.ChildElements)
                {
                    switch (cellElem)
                    {
                        case TableTHElement th:
                            concatenated += th.ActualText ?? string.Empty;
                            break;
                        case TableTDElement td:
                            concatenated += td.ActualText ?? string.Empty;
                            break;
                    }
                }

                // Write the concatenated string as a CSV line (escaping if needed)
                writer.WriteLine(EscapeCsv(concatenated));
            }
        }
    }

    // Simple CSV field escaper
    static string EscapeCsv(string field)
    {
        if (field.Contains("\"") || field.Contains(",") || field.Contains("\n"))
        {
            field = field.Replace("\"", "\"\"");
            return $"\"{field}\"";
        }
        return field;
    }
}
