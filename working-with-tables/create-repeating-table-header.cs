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
        const string outputPath = "output_with_header.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content API
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Document with repeating table header");

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a table element and attach it to the root
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table with repeating header";
            root.AppendChild(table);

            // Create the table header (THead) group
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);

            // Create a header row inside the THead
            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);

            // Mark the row as a header that should repeat on new pages
            headerRow.IsInNewPage = true;          // makes the row repeat
            table.RepeatingRowsCount = 1;          // first row (header) repeats

            // Add header cells
            TableTHElement th1 = tagged.CreateTableTHElement();
            th1.SetText("Column A");
            headerRow.AppendChild(th1);

            TableTHElement th2 = tagged.CreateTableTHElement();
            th2.SetText("Column B");
            headerRow.AppendChild(th2);

            // Create the table body (TBody) group
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            // Add a sample data row
            TableTRElement dataRow = tagged.CreateTableTRElement();
            tbody.AppendChild(dataRow);

            TableTDElement td1 = tagged.CreateTableTDElement();
            td1.SetText("Value 1");
            dataRow.AppendChild(td1);

            TableTDElement td2 = tagged.CreateTableTDElement();
            td2.SetText("Value 2");
            dataRow.AppendChild(td2);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with repeating table header to '{outputPath}'.");
    }
}