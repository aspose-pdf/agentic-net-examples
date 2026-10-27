using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;          // ITaggedContent
using Aspose.Pdf.LogicalStructure; // StructureElement, HeaderElement, ParagraphElement, TableElement, etc.

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "accessible_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF and obtain the tagging interface
        using (Document doc = new Document(inputPath))
        {
            ITaggedContent taggedContent = doc.TaggedContent;

            // Set language and title for the tagged PDF
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root of the logical structure tree
            StructureElement root = taggedContent.RootElement;

            // ----- Add a heading (H1) -----
            HeaderElement heading = taggedContent.CreateHeaderElement(1); // level 1 heading
            heading.SetText("Document Title");
            root.AppendChild(heading); // attach heading to root

            // ----- Add a paragraph -----
            ParagraphElement paragraph = taggedContent.CreateParagraphElement();
            paragraph.SetText("This paragraph provides an introduction to the document content. It is tagged for screen‑reader accessibility.");
            root.AppendChild(paragraph); // attach paragraph to root

            // ----- Add a table -----
            TableElement table = taggedContent.CreateTableElement();
            table.AlternativeText = "Sample data table showing product names and prices.";
            root.AppendChild(table); // attach table to root

            // Table header
            TableTHeadElement thead = taggedContent.CreateTableTHeadElement();
            table.AppendChild(thead);
            TableTRElement headerRow = taggedContent.CreateTableTRElement();
            thead.AppendChild(headerRow);

            TableTHElement thProduct = taggedContent.CreateTableTHElement();
            thProduct.SetText("Product");
            headerRow.AppendChild(thProduct);

            TableTHElement thPrice = taggedContent.CreateTableTHElement();
            thPrice.SetText("Price");
            headerRow.AppendChild(thPrice);

            // Table body
            TableTBodyElement tbody = taggedContent.CreateTableTBodyElement();
            table.AppendChild(tbody);

            // First data row
            TableTRElement row1 = taggedContent.CreateTableTRElement();
            tbody.AppendChild(row1);

            TableTDElement tdProd1 = taggedContent.CreateTableTDElement();
            tdProd1.SetText("Widget A");
            row1.AppendChild(tdProd1);

            TableTDElement tdPrice1 = taggedContent.CreateTableTDElement();
            tdPrice1.SetText("$10.00");
            row1.AppendChild(tdPrice1);

            // Second data row
            TableTRElement row2 = taggedContent.CreateTableTRElement();
            tbody.AppendChild(row2);

            TableTDElement tdProd2 = taggedContent.CreateTableTDElement();
            tdProd2.SetText("Widget B");
            row2.AppendChild(tdProd2);

            TableTDElement tdPrice2 = taggedContent.CreateTableTDElement();
            tdPrice2.SetText("$15.00");
            row2.AppendChild(tdPrice2);

            // Save the modified PDF; no PreSave() call is required
            doc.Save(outputPath);
        }

        Console.WriteLine($"Accessible PDF saved to '{outputPath}'.");
    }
}