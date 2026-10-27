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
        const string outputPath = "tagged_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document doc = new Document(inputPath))
            {
                // Access the tagged content interface
                ITaggedContent taggedContent = doc.TaggedContent;

                // Set language and title for the tagged PDF
                taggedContent.SetLanguage("en-US");
                taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

                // Root element of the structure tree
                StructureElement root = taggedContent.RootElement;

                // Create a table element
                TableElement table = taggedContent.CreateTableElement();
                table.AlternativeText = "Sample data table with custom data‑type tags";
                root.AppendChild(table);

                // ----- Table Header -----
                TableTHeadElement thead = taggedContent.CreateTableTHeadElement();
                table.AppendChild(thead);

                TableTRElement headerRow = taggedContent.CreateTableTRElement();
                thead.AppendChild(headerRow);

                TableTHElement thName = taggedContent.CreateTableTHElement();
                thName.SetText("Name");
                // Use AlternativeText to store a custom tag for the column
                thName.AlternativeText = "type:string";
                headerRow.AppendChild(thName);

                TableTHElement thValue = taggedContent.CreateTableTHElement();
                thValue.SetText("Value");
                thValue.AlternativeText = "type:currency";
                headerRow.AppendChild(thValue);

                // ----- Table Body -----
                TableTBodyElement tbody = taggedContent.CreateTableTBodyElement();
                table.AppendChild(tbody);

                // First data row
                TableTRElement row1 = taggedContent.CreateTableTRElement();
                tbody.AppendChild(row1);

                TableTDElement tdName1 = taggedContent.CreateTableTDElement();
                tdName1.SetText("Product A");
                tdName1.AlternativeText = "type:string";
                row1.AppendChild(tdName1);

                TableTDElement tdValue1 = taggedContent.CreateTableTDElement();
                tdValue1.SetText("$12,345");
                tdValue1.AlternativeText = "type:currency";
                row1.AppendChild(tdValue1);

                // Second data row
                TableTRElement row2 = taggedContent.CreateTableTRElement();
                tbody.AppendChild(row2);

                TableTDElement tdName2 = taggedContent.CreateTableTDElement();
                tdName2.SetText("Product B");
                tdName2.AlternativeText = "type:string";
                row2.AppendChild(tdName2);

                TableTDElement tdValue2 = taggedContent.CreateTableTDElement();
                tdValue2.SetText("$67,890");
                tdValue2.AlternativeText = "type:currency";
                row2.AppendChild(tdValue2);

                // Save the modified PDF (no PreSave required)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Tagged PDF with custom cell tags saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}