using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
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
            // Load the existing PDF
            using (Document doc = new Document(inputPath))
            {
                // Access tagged content (creates structure if not present)
                ITaggedContent taggedContent = doc.TaggedContent;
                taggedContent.SetLanguage("en-US");
                taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

                // Root element of the structure tree
                StructureElement root = taggedContent.RootElement;

                // Create a table element and attach it to the root
                TableElement table = taggedContent.CreateTableElement();
                table.AlternativeText = "Sample data table";
                root.AppendChild(table);

                // ----- Header row (TH) -----
                TableTHeadElement thead = taggedContent.CreateTableTHeadElement();
                table.AppendChild(thead);

                TableTRElement headerRow = taggedContent.CreateTableTRElement();
                thead.AppendChild(headerRow);

                // First header cell
                TableTHElement th1 = taggedContent.CreateTableTHElement();
                th1.SetText("Product");
                th1.ActualText = "Product"; // set /ActualText attribute
                headerRow.AppendChild(th1);

                // Second header cell
                TableTHElement th2 = taggedContent.CreateTableTHElement();
                th2.SetText("Revenue");
                th2.ActualText = "Revenue";
                headerRow.AppendChild(th2);

                // ----- Body rows (TD) -----
                TableTBodyElement tbody = taggedContent.CreateTableTBodyElement();
                table.AppendChild(tbody);

                // Example data row
                TableTRElement dataRow = taggedContent.CreateTableTRElement();
                tbody.AppendChild(dataRow);

                TableTDElement td1 = taggedContent.CreateTableTDElement();
                td1.SetText("Widget A");
                td1.ActualText = "Widget A";
                dataRow.AppendChild(td1);

                TableTDElement td2 = taggedContent.CreateTableTDElement();
                td2.SetText("$50,000");
                td2.ActualText = "$50,000";
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