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
        const string outputPath = "accessible_table_with_bg.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content interface
            ITaggedContent taggedContent = doc.TaggedContent;
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle("PDF with Accessible Table");

            // Get the root structure element (no cast required)
            StructureElement root = taggedContent.RootElement;

            // Create a table element via the tagged content factory
            TableElement table = taggedContent.CreateTableElement();

            // Set a light gray background color with 50% opacity using ARGB
            // Alpha 128 (out of 255) = 50% opacity
            table.BackgroundColor = Aspose.Pdf.Color.FromArgb(128, 230, 230, 230); // light gray, semi‑transparent

            // Optional description for accessibility
            table.AlternativeText = "Sample data table with background color";

            // Attach the table to the document's structure tree
            root.AppendChild(table);

            // ----- Build table header -----
            TableTHeadElement thead = taggedContent.CreateTableTHeadElement();
            table.AppendChild(thead);

            TableTRElement headerRow = taggedContent.CreateTableTRElement();
            thead.AppendChild(headerRow);

            TableTHElement thName = taggedContent.CreateTableTHElement();
            thName.SetText("Name");
            headerRow.AppendChild(thName);

            TableTHElement thValue = taggedContent.CreateTableTHElement();
            thValue.SetText("Value");
            headerRow.AppendChild(thValue);

            // ----- Build table body -----
            TableTBodyElement tbody = taggedContent.CreateTableTBodyElement();
            table.AppendChild(tbody);

            TableTRElement dataRow = taggedContent.CreateTableTRElement();
            tbody.AppendChild(dataRow);

            TableTDElement tdItem = taggedContent.CreateTableTDElement();
            tdItem.SetText("Item A");
            dataRow.AppendChild(tdItem);

            TableTDElement tdNumber = taggedContent.CreateTableTDElement();
            tdNumber.SetText("42");
            dataRow.AppendChild(tdNumber);

            // Save the modified PDF (no PreSave call required)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with accessible table and background color: '{outputPath}'.");
    }
}
