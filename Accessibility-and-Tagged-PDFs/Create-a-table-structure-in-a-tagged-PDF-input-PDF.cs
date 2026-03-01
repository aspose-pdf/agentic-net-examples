using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;               // ITaggedContent
using Aspose.Pdf.LogicalStructure;    // StructureElement and table element types

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "tagged_table_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Access tagged content interface
                ITaggedContent tagged = doc.TaggedContent;

                // Set language and title for accessibility
                tagged.SetLanguage("en-US");
                tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

                // Root element of the logical structure tree
                StructureElement root = tagged.RootElement;

                // Create a table element and attach it to the root
                TableElement table = tagged.CreateTableElement();
                table.AlternativeText = "Sample data table";
                root.AppendChild(table);

                // ----- Table Header -----
                TableTHeadElement thead = tagged.CreateTableTHeadElement();
                table.AppendChild(thead);

                TableTRElement headerRow = tagged.CreateTableTRElement();
                thead.AppendChild(headerRow);

                TableTHElement th1 = tagged.CreateTableTHElement();
                th1.SetText("Name");
                headerRow.AppendChild(th1);

                TableTHElement th2 = tagged.CreateTableTHElement();
                th2.SetText("Value");
                headerRow.AppendChild(th2);

                // ----- Table Body -----
                TableTBodyElement tbody = tagged.CreateTableTBodyElement();
                table.AppendChild(tbody);

                TableTRElement dataRow = tagged.CreateTableTRElement();
                tbody.AppendChild(dataRow);

                TableTDElement td1 = tagged.CreateTableTDElement();
                td1.SetText("Item A");
                dataRow.AppendChild(td1);

                TableTDElement td2 = tagged.CreateTableTDElement();
                td2.SetText("42");
                dataRow.AppendChild(td2);

                // Save the modified PDF (no PreSave required)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Tagged PDF with table saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}