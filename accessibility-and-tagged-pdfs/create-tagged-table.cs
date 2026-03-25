using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_tagged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Access tagged‑content API
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("PDF with Tagged Table");

            // Root element of the structure tree
            StructureElement root = tagged.RootElement;

            // Create the table element and attach it to the root
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample 3x4 data table";
            root.AppendChild(table);

            // Create a table body (TBody) and attach it to the table
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            // Add three rows, each with four cells
            for (int r = 1; r <= 3; r++)
            {
                TableTRElement row = tagged.CreateTableTRElement();
                tbody.AppendChild(row);

                for (int c = 1; c <= 4; c++)
                {
                    TableTDElement cell = tagged.CreateTableTDElement();
                    cell.SetText($"R{r}C{c}"); // cell content
                    row.AppendChild(cell);
                }
            }

            // Persist the changes
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}