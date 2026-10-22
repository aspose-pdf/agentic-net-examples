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
        const string outputPath = "accessible_table_with_footer.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and work with its tagged content
        using (Document doc = new Document(inputPath))
        {
            ITaggedContent taggedContent = doc.TaggedContent;
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root element of the structure tree
            StructureElement root = taggedContent.RootElement;

            // Create a table element and attach it to the root
            TableElement table = taggedContent.CreateTableElement();
            table.AlternativeText = "Sample data table with footer";
            root.AppendChild(table);

            // ----- Header -----
            TableTHeadElement thead = taggedContent.CreateTableTHeadElement();
            table.AppendChild(thead);
            TableTRElement headerRow = taggedContent.CreateTableTRElement();
            thead.AppendChild(headerRow);

            TableTHElement th1 = taggedContent.CreateTableTHElement();
            th1.SetText("Product");
            headerRow.AppendChild(th1);

            TableTHElement th2 = taggedContent.CreateTableTHElement();
            th2.SetText("Revenue");
            headerRow.AppendChild(th2);

            // ----- Body -----
            TableTBodyElement tbody = taggedContent.CreateTableTBodyElement();
            table.AppendChild(tbody);
            TableTRElement dataRow = taggedContent.CreateTableTRElement();
            tbody.AppendChild(dataRow);

            TableTDElement td1 = taggedContent.CreateTableTDElement();
            td1.SetText("Widget A");
            dataRow.AppendChild(td1);

            TableTDElement td2 = taggedContent.CreateTableTDElement();
            td2.SetText("$50,000");
            dataRow.AppendChild(td2);

            // ----- Footer -----
            TableTFootElement tfoot = taggedContent.CreateTableTFootElement();
            // In the current Aspose.Pdf version the TableTFootElement does not expose a Repeat property.
            // Footers are automatically rendered on each page when the table spans multiple pages.
            table.AppendChild(tfoot);
            TableTRElement footerRow = taggedContent.CreateTableTRElement();
            tfoot.AppendChild(footerRow);

            TableTDElement footCell1 = taggedContent.CreateTableTDElement();
            footCell1.SetText("Total");
            footerRow.AppendChild(footCell1);

            TableTDElement footCell2 = taggedContent.CreateTableTDElement();
            footCell2.SetText("$50,000");
            footerRow.AppendChild(footCell2);

            // Save the modified PDF (no PreSave required)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF with table footer saved to '{outputPath}'.");
    }
}
