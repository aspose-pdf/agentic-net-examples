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
        const string outputPath = "accessible_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Access tagged content and set language/title
                ITaggedContent taggedContent = doc.TaggedContent;
                taggedContent.SetLanguage("en-US");
                taggedContent.SetTitle("Accessible Table PDF");

                // Get the root structure element (no cast needed)
                StructureElement root = taggedContent.RootElement;

                // Create a Table element and attach it to the root
                TableElement table = taggedContent.CreateTableElement();
                table.AlternativeText = "Sample data table";
                root.AppendChild(table); // AppendChild with one argument

                // Create table header (THead) and a header row
                TableTHeadElement thead = taggedContent.CreateTableTHeadElement();
                table.AppendChild(thead);
                TableTRElement headerRow = taggedContent.CreateTableTRElement();
                thead.AppendChild(headerRow);

                // Add header cells (TH)
                TableTHElement th1 = taggedContent.CreateTableTHElement();
                th1.SetText("Name");
                headerRow.AppendChild(th1);
                TableTHElement th2 = taggedContent.CreateTableTHElement();
                th2.SetText("Value");
                headerRow.AppendChild(th2);

                // Create table body (TBody) and a data row
                TableTBodyElement tbody = taggedContent.CreateTableTBodyElement();
                table.AppendChild(tbody);
                TableTRElement dataRow = taggedContent.CreateTableTRElement();
                tbody.AppendChild(dataRow);

                // Add data cells (TD)
                TableTDElement td1 = taggedContent.CreateTableTDElement();
                td1.SetText("Item A");
                dataRow.AppendChild(td1);
                TableTDElement td2 = taggedContent.CreateTableTDElement();
                td2.SetText("42");
                dataRow.AppendChild(td2);

                // Save the modified PDF (no PreSave needed)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Accessible table PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}