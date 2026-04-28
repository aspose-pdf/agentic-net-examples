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
        const string outputPath = "output_with_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access tagged‑content API
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Document with Table");

            // Root element of the structure tree
            StructureElement root = tagged.RootElement;

            // Create a table element and attach it to the root
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample 3x4 table";
            root.AppendChild(table); // AppendChild takes a single StructureElement argument

            // Create a table body (tbody) and attach it to the table
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            // Add three rows, each with four cells
            for (int row = 1; row <= 3; row++)
            {
                TableTRElement tr = tagged.CreateTableTRElement();
                tbody.AppendChild(tr);

                for (int col = 1; col <= 4; col++)
                {
                    TableTDElement td = tagged.CreateTableTDElement();
                    td.SetText($"R{row}C{col}"); // Set cell text
                    tr.AppendChild(td);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with table to '{outputPath}'.");
    }
}
