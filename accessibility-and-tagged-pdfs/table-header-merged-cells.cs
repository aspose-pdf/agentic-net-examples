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

            // Header row
            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);

            // First header cell – spans two columns (merged cell)
            TableTHElement thMerged = tagged.CreateTableTHElement();
            thMerged.ColSpan = 2;                     // merge across two columns
            thMerged.SetText("Merged Header");        // visible text
            thMerged.ActualText = "Merged Header";    // /ActualText attribute
            headerRow.AppendChild(thMerged);

            // Second header cell – normal column
            TableTHElement thNormal = tagged.CreateTableTHElement();
            thNormal.SetText("Column 3");
            thNormal.ActualText = "Column 3";
            headerRow.AppendChild(thNormal);

            // ----- Body (TBody) -----
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            // Example data row
            TableTRElement dataRow = tagged.CreateTableTRElement();
            tbody.AppendChild(dataRow);

            TableTDElement td1 = tagged.CreateTableTDElement();
            td1.SetText("Cell 1");
            dataRow.AppendChild(td1);

            TableTDElement td2 = tagged.CreateTableTDElement();
            td2.SetText("Cell 2");
            dataRow.AppendChild(td2);

            TableTDElement td3 = tagged.CreateTableTDElement();
            td3.SetText("Cell 3");
            dataRow.AppendChild(td3);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}