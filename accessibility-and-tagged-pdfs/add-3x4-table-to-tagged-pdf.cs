using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // existing PDF to tag
        const string outputPath = "output_tagged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and obtain the tagged‑content interface
        using (Document doc = new Document(inputPath))
        {
            ITaggedContent tagged = doc.TaggedContent;

            // Optional: set language and title for the tagged PDF
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root element of the structure tree (no cast required)
            StructureElement root = tagged.RootElement;

            // Create a table element and attach it to the root
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample 3x4 table";
            root.AppendChild(table);

            // Create the table body (rows container)
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            // Build three rows, each with four cells
            for (int row = 1; row <= 3; row++)
            {
                // Create a table row element
                TableTRElement tr = tagged.CreateTableTRElement();
                tbody.AppendChild(tr);

                for (int col = 1; col <= 4; col++)
                {
                    // Create a table cell element
                    TableTDElement td = tagged.CreateTableTDElement();
                    td.SetText($"R{row}C{col}");   // cell content, e.g., "R1C1"
                    tr.AppendChild(td);
                }
            }

            // Save the modified PDF (no PreSave required)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF with table saved to '{outputPath}'.");
    }
}