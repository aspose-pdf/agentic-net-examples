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
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Table with Header");

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a table element and attach it to the root
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table";
            root.AppendChild(table);

            // ----- Header (THead) -----
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);

            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);

            // First header cell spans two columns (merged)
            TableTHElement th1 = tagged.CreateTableTHElement();
            th1.SetText("Merged Header");
            th1.ColSpan = 2;               // merge two columns
            th1.ActualText = "Merged Header";
            headerRow.AppendChild(th1);

            // Second header cell (regular)
            TableTHElement th2 = tagged.CreateTableTHElement();
            th2.SetText("Column C");
            th2.ActualText = "Column C";
            headerRow.AppendChild(th2);

            // ----- Body (TBody) -----
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            TableTRElement dataRow = tagged.CreateTableTRElement();
            tbody.AppendChild(dataRow);

            TableTDElement td1 = tagged.CreateTableTDElement();
            td1.SetText("A1");
            dataRow.AppendChild(td1);

            TableTDElement td2 = tagged.CreateTableTDElement();
            td2.SetText("B1");
            dataRow.AppendChild(td2);

            TableTDElement td3 = tagged.CreateTableTDElement();
            td3.SetText("C1");
            dataRow.AppendChild(td3);

            // Save the tagged PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}