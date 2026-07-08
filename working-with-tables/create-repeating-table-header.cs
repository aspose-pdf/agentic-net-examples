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
        const string outputPath = "tagged_with_header.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF and work with its tagged content
        using (Document doc = new Document(inputPath))
        {
            ITaggedContent tagged = doc.TaggedContent;

            // Set language and title for the tagged PDF (optional)
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root element of the structure tree
            StructureElement root = tagged.RootElement;

            // Create a table element and add it to the root
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table with repeating header";
            root.AppendChild(table);

            // Mark the first row as a repeating header (repeat on each new page)
            table.RepeatingRowsCount = 1; // repeats the first row(s) on subsequent pages

            // Create the table header (THead) group
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);

            // Create a header row inside the THead
            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);

            // Create header cells (TH) and set their text
            TableTHElement th1 = tagged.CreateTableTHElement();
            th1.SetText("Column A");
            headerRow.AppendChild(th1);

            TableTHElement th2 = tagged.CreateTableTHElement();
            th2.SetText("Column B");
            headerRow.AppendChild(th2);

            TableTHElement th3 = tagged.CreateTableTHElement();
            th3.SetText("Column C");
            headerRow.AppendChild(th3);

            // Create a table body (TBody) and add a sample data row
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            TableTRElement dataRow = tagged.CreateTableTRElement();
            tbody.AppendChild(dataRow);

            TableTDElement td1 = tagged.CreateTableTDElement();
            td1.SetText("Value 1");
            dataRow.AppendChild(td1);

            TableTDElement td2 = tagged.CreateTableTDElement();
            td2.SetText("Value 2");
            dataRow.AppendChild(td2);

            TableTDElement td3 = tagged.CreateTableTDElement();
            td3.SetText("Value 3");
            dataRow.AppendChild(td3);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF with repeating table header saved to '{outputPath}'.");
    }
}