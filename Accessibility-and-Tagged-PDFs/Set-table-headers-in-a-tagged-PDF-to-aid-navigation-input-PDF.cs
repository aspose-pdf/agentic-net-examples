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

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Set language and title for accessibility metadata
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root element of the logical structure tree (no cast needed)
            StructureElement root = tagged.RootElement;

            // Create a table element and attach it to the root
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table with headers";
            root.AppendChild(table);

            // Create the table header (THead) group
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);

            // Create a header row inside THead
            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);

            // First header cell (TH) with text
            TableTHElement th1 = tagged.CreateTableTHElement();
            th1.SetText("Column A");
            headerRow.AppendChild(th1);

            // Second header cell (TH) with text
            TableTHElement th2 = tagged.CreateTableTHElement();
            th2.SetText("Column B");
            headerRow.AppendChild(th2);

            // OPTIONAL: add a body row to illustrate the table structure
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);
            TableTRElement bodyRow = tagged.CreateTableTRElement();
            tbody.AppendChild(bodyRow);
            TableTDElement td1 = tagged.CreateTableTDElement();
            td1.SetText("Value 1");
            bodyRow.AppendChild(td1);
            TableTDElement td2 = tagged.CreateTableTDElement();
            td2.SetText("Value 2");
            bodyRow.AppendChild(td2);

            // Save the modified PDF (no PreSave call needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF with table headers saved to '{outputPath}'.");
    }
}