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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Access the tagged content API
                ITaggedContent taggedContent = doc.TaggedContent;
                taggedContent.SetLanguage("en-US");
                taggedContent.SetTitle("Accessible Table PDF");

                // Get the root structure element (no cast needed)
                StructureElement root = taggedContent.RootElement;

                // Create a Table element via the ITaggedContent factory
                TableElement table = taggedContent.CreateTableElement();

                // Set a semi‑transparent background color (50% opacity, light gray)
                // FromArgb expects integer values (0‑255). 128 = 50% opacity.
                table.BackgroundColor = Aspose.Pdf.Color.FromArgb(128, 230, 230, 230);

                // Attach the table to the root element
                root.AppendChild(table);

                // ----- Header row -----
                TableTHeadElement thead = taggedContent.CreateTableTHeadElement();
                table.AppendChild(thead);

                TableTRElement headerRow = taggedContent.CreateTableTRElement();
                thead.AppendChild(headerRow);

                TableTHElement th1 = taggedContent.CreateTableTHElement();
                th1.SetText("Name");
                headerRow.AppendChild(th1);

                TableTHElement th2 = taggedContent.CreateTableTHElement();
                th2.SetText("Value");
                headerRow.AppendChild(th2);

                // ----- Body rows -----
                TableTBodyElement tbody = taggedContent.CreateTableTBodyElement();
                table.AppendChild(tbody);

                TableTRElement dataRow = taggedContent.CreateTableTRElement();
                tbody.AppendChild(dataRow);

                TableTDElement td1 = taggedContent.CreateTableTDElement();
                td1.SetText("Item A");
                dataRow.AppendChild(td1);

                TableTDElement td2 = taggedContent.CreateTableTDElement();
                td2.SetText("42");
                dataRow.AppendChild(td2);

                // Save the modified PDF (no PreSave call required)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Tagged PDF with table background saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
