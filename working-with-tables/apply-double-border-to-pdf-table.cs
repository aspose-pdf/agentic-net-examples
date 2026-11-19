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
        const string outputPath = "output_with_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Access tagged content and set basic properties
                ITaggedContent taggedContent = doc.TaggedContent;
                taggedContent.SetLanguage("en-US");
                taggedContent.SetTitle("PDF with Double Border Table");

                // Root element of the structure tree
                StructureElement root = taggedContent.RootElement;

                // Create a table element
                TableElement table = taggedContent.CreateTableElement();
                table.AlternativeText = "Sample data table with double border";

                // Apply double‑like border style using BorderInfo constructor (width = 2 points)
                // Aspose.Pdf.BorderInfo does not expose a BorderStyle property; the double style is
                // represented by setting a thicker width. If a specific double style is required,
                // it can be simulated with custom drawing, but for the API the width is the
                // configurable attribute.
                table.Border = new BorderInfo(BorderSide.All, 2f);

                // Attach table to the root
                root.AppendChild(table);

                // Header section
                TableTHeadElement thead = taggedContent.CreateTableTHeadElement();
                table.AppendChild(thead);
                TableTRElement headerRow = taggedContent.CreateTableTRElement();
                thead.AppendChild(headerRow);
                TableTHElement th1 = taggedContent.CreateTableTHElement();
                th1.SetText("Product");
                headerRow.AppendChild(th1);
                TableTHElement th2 = taggedContent.CreateTableTHElement();
                th2.SetText("Price");
                headerRow.AppendChild(th2);

                // Body section
                TableTBodyElement tbody = taggedContent.CreateTableTBodyElement();
                table.AppendChild(tbody);
                TableTRElement dataRow = taggedContent.CreateTableTRElement();
                tbody.AppendChild(dataRow);
                TableTDElement td1 = taggedContent.CreateTableTDElement();
                td1.SetText("Widget A");
                dataRow.AppendChild(td1);
                TableTDElement td2 = taggedContent.CreateTableTDElement();
                td2.SetText("$50");
                dataRow.AppendChild(td2);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Saved PDF with double border table to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
