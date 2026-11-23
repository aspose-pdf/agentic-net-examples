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
        const string outputPath = "output_tagged_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Access the tagged content of the document
                ITaggedContent tagged = doc.TaggedContent;

                // Set language and title for accessibility (optional)
                tagged.SetLanguage("en-US");
                tagged.SetTitle("PDF with Table");

                // Root element of the structure tree
                StructureElement root = tagged.RootElement;

                // Create a table element and attach it to the root
                TableElement table = tagged.CreateTableElement();
                table.AlternativeText = "Sample data table";
                root.AppendChild(table);

                // Create table header
                TableTHeadElement thead = tagged.CreateTableTHeadElement();
                table.AppendChild(thead);
                TableTRElement headerRow = tagged.CreateTableTRElement();
                thead.AppendChild(headerRow);

                TableTHElement th1 = tagged.CreateTableTHElement();
                th1.SetText("Product");
                headerRow.AppendChild(th1);

                TableTHElement th2 = tagged.CreateTableTHElement();
                th2.SetText("Price");
                headerRow.AppendChild(th2);

                // Create table body
                TableTBodyElement tbody = tagged.CreateTableTBodyElement();
                table.AppendChild(tbody);
                TableTRElement dataRow = tagged.CreateTableTRElement();
                tbody.AppendChild(dataRow);

                TableTDElement td1 = tagged.CreateTableTDElement();
                td1.SetText("Widget A");
                dataRow.AppendChild(td1);

                TableTDElement td2 = tagged.CreateTableTDElement();
                td2.SetText("$25.00");
                dataRow.AppendChild(td2);

                // Save the modified PDF
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