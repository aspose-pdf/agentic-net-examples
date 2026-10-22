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
        const string outputPath = "output_with_header_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and work with its tagged content
        using (Document doc = new Document(inputPath))
        {
            ITaggedContent tagged = doc.TaggedContent;
            // Set language and title (optional)
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root element of the structure tree
            StructureElement root = tagged.RootElement;

            // Create a table element
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table with repeating header row";
            // Set the first row to repeat on each new page
            table.RepeatingRowsCount = 1;
            root.AppendChild(table);

            // ----- Header row (repeats on each new page) -----
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);

            // Create a table row for the header
            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);

            // Header cells
            TableTHElement thName = tagged.CreateTableTHElement();
            thName.SetText("Name");
            headerRow.AppendChild(thName);

            TableTHElement thValue = tagged.CreateTableTHElement();
            thValue.SetText("Value");
            headerRow.AppendChild(thValue);

            // ----- Body rows (example data) -----
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            // First data row
            TableTRElement row1 = tagged.CreateTableTRElement();
            tbody.AppendChild(row1);

            TableTDElement td1a = tagged.CreateTableTDElement();
            td1a.SetText("Item A");
            row1.AppendChild(td1a);

            TableTDElement td1b = tagged.CreateTableTDElement();
            td1b.SetText("123");
            row1.AppendChild(td1b);

            // Second data row
            TableTRElement row2 = tagged.CreateTableTRElement();
            tbody.AppendChild(row2);

            TableTDElement td2a = tagged.CreateTableTDElement();
            td2a.SetText("Item B");
            row2.AppendChild(td2a);

            TableTDElement td2b = tagged.CreateTableTDElement();
            td2b.SetText("456");
            row2.AppendChild(td2b);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with repeating table header saved to '{outputPath}'.");
    }
}
