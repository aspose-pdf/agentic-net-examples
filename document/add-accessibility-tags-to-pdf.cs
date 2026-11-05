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
        const string outputPath = "tagged_output.pdf";

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
                // Access the tagged content interface
                ITaggedContent tagged = doc.TaggedContent;

                // Set language and title for the tagged PDF (write‑only setters)
                tagged.SetLanguage("en-US");
                tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

                // Root element of the logical structure tree (no cast required)
                StructureElement root = tagged.RootElement;

                // ----- Add a heading (H1) -----
                HeaderElement heading = tagged.CreateHeaderElement(1);
                heading.SetText("Document Title");
                root.AppendChild(heading); // AppendChild with single argument

                // ----- Add a paragraph -----
                ParagraphElement paragraph = tagged.CreateParagraphElement();
                paragraph.SetText("This is an introductory paragraph that provides an overview of the document content.");
                root.AppendChild(paragraph);

                // ----- Create a table -----
                TableElement table = tagged.CreateTableElement();
                table.AlternativeText = "Sample data table showing product names and quantities.";
                root.AppendChild(table);

                // Table header (thead)
                TableTHeadElement thead = tagged.CreateTableTHeadElement();
                table.AppendChild(thead);
                TableTRElement headerRow = tagged.CreateTableTRElement();
                thead.AppendChild(headerRow);
                TableTHElement th1 = tagged.CreateTableTHElement();
                th1.SetText("Product");
                headerRow.AppendChild(th1);
                TableTHElement th2 = tagged.CreateTableTHElement();
                th2.SetText("Quantity");
                headerRow.AppendChild(th2);

                // Table body (tbody)
                TableTBodyElement tbody = tagged.CreateTableTBodyElement();
                table.AppendChild(tbody);
                TableTRElement dataRow = tagged.CreateTableTRElement();
                tbody.AppendChild(dataRow);
                TableTDElement td1 = tagged.CreateTableTDElement();
                td1.SetText("Widget A");
                dataRow.AppendChild(td1);
                TableTDElement td2 = tagged.CreateTableTDElement();
                td2.SetText("150");
                dataRow.AppendChild(td2);

                // Save the modified PDF (no PreSave required)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}