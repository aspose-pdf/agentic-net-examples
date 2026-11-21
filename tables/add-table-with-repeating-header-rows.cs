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
        const string outputPath = "output.pdf";

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

            // Root element of the structure tree
            StructureElement root = taggedContent.RootElement;

            // Create a table element and attach it to the root
            TableElement table = taggedContent.CreateTableElement();
            table.AlternativeText = "Sample table with repeating header rows";
            root.AppendChild(table);

            // ----- Table Header (first two rows will be repeated) -----
            TableTHeadElement thead = taggedContent.CreateTableTHeadElement();
            table.AppendChild(thead);

            // First header row
            TableTRElement headerRow1 = taggedContent.CreateTableTRElement();
            thead.AppendChild(headerRow1);
            TableTHElement th11 = taggedContent.CreateTableTHElement();
            th11.SetText("Column A");
            headerRow1.AppendChild(th11);
            TableTHElement th12 = taggedContent.CreateTableTHElement();
            th12.SetText("Column B");
            headerRow1.AppendChild(th12);

            // Second header row (also part of the repeating set)
            TableTRElement headerRow2 = taggedContent.CreateTableTRElement();
            thead.AppendChild(headerRow2);
            TableTHElement th21 = taggedContent.CreateTableTHElement();
            th21.SetText("Sub‑Header 1");
            headerRow2.AppendChild(th21);
            TableTHElement th22 = taggedContent.CreateTableTHElement();
            th22.SetText("Sub‑Header 2");
            headerRow2.AppendChild(th22);

            // ----- Table Body -----
            TableTBodyElement tbody = taggedContent.CreateTableTBodyElement();
            table.AppendChild(tbody);

            // Sample data row 1
            TableTRElement dataRow1 = taggedContent.CreateTableTRElement();
            tbody.AppendChild(dataRow1);
            TableTDElement td11 = taggedContent.CreateTableTDElement();
            td11.SetText("Data 1‑1");
            dataRow1.AppendChild(td11);
            TableTDElement td12 = taggedContent.CreateTableTDElement();
            td12.SetText("Data 1‑2");
            dataRow1.AppendChild(td12);

            // Sample data row 2
            TableTRElement dataRow2 = taggedContent.CreateTableTRElement();
            tbody.AppendChild(dataRow2);
            TableTDElement td21 = taggedContent.CreateTableTDElement();
            td21.SetText("Data 2‑1");
            dataRow2.AppendChild(td21);
            TableTDElement td22 = taggedContent.CreateTableTDElement();
            td22.SetText("Data 2‑2");
            dataRow2.AppendChild(td22);

            // Set the number of rows to repeat on each page (first two rows)
            table.RepeatingRowsCount = 2;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table with repeating rows added and saved to '{outputPath}'.");
    }
}