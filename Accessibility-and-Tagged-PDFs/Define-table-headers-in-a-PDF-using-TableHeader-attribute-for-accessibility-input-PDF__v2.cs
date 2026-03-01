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
        const string outputPath = "output_accessible.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Set language and title for the tagged PDF (optional but recommended)
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a Table structure element and attach it to the root
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table with header row";
            root.AppendChild(table); // AppendChild with one argument

            // ----- Create the table header (THead) -----
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);

            // Create a header row inside THead
            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);

            // Define header cells (TH) – these are automatically recognized as table headers
            TableTHElement thName = tagged.CreateTableTHElement();
            thName.SetText("Name");               // Set visible header text
            headerRow.AppendChild(thName);

            TableTHElement thValue = tagged.CreateTableTHElement();
            thValue.SetText("Value");
            headerRow.AppendChild(thValue);

            // ----- Create the table body (TBody) -----
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            // Example data row
            TableTRElement dataRow = tagged.CreateTableTRElement();
            tbody.AppendChild(dataRow);

            // Data cell 1
            TableTDElement td1 = tagged.CreateTableTDElement();
            td1.SetText("Item A");
            dataRow.AppendChild(td1);

            // Data cell 2
            TableTDElement td2 = tagged.CreateTableTDElement();
            td2.SetText("42");
            dataRow.AppendChild(td2);

            // Save the modified PDF (PDF format, no extra SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Accessible PDF with table headers saved to '{outputPath}'.");
    }
}