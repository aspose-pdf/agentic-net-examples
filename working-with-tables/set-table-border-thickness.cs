using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_tagged_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent taggedContent = doc.TaggedContent;
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root element of the structure tree
            StructureElement root = taggedContent.RootElement;

            // Create a table element
            TableElement table = taggedContent.CreateTableElement();
            table.AlternativeText = "Sample table with 2‑point border thickness";

            // Set the border thickness to 2 points using the BorderInfo constructor
            table.Border = new BorderInfo(BorderSide.All, 2f);

            // Attach the table to the root
            root.AppendChild(table);

            // ----- Build a simple table (header + one data row) -----
            TableTHeadElement thead = taggedContent.CreateTableTHeadElement();
            table.AppendChild(thead);
            TableTRElement headerRow = taggedContent.CreateTableTRElement();
            thead.AppendChild(headerRow);

            TableTHElement th1 = taggedContent.CreateTableTHElement();
            th1.SetText("Column 1");
            headerRow.AppendChild(th1);

            TableTHElement th2 = taggedContent.CreateTableTHElement();
            th2.SetText("Column 2");
            headerRow.AppendChild(th2);

            TableTBodyElement tbody = taggedContent.CreateTableTBodyElement();
            table.AppendChild(tbody);
            TableTRElement bodyRow = taggedContent.CreateTableTRElement();
            tbody.AppendChild(bodyRow);

            TableTDElement td1 = taggedContent.CreateTableTDElement();
            td1.SetText("Cell A");
            bodyRow.AppendChild(td1);

            TableTDElement td2 = taggedContent.CreateTableTDElement();
            td2.SetText("Cell B");
            bodyRow.AppendChild(td2);
            // ---------------------------------------------------------

            // Save the modified PDF (no PreSave call needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}
