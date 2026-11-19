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
        const string outputPath = "output_with_footnote.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Access the tagged content API
                ITaggedContent taggedContent = doc.TaggedContent;

                // Root of the structure tree
                var root = taggedContent.RootElement;

                // ------------------------------------------------------------
                // Create a Footnote‑like element and add it to the root
                // ------------------------------------------------------------
                // Use a DivElement as a container for the footnote content.
                var footnote = taggedContent.CreateDivElement();

                // The DivElement itself cannot hold plain text directly.
                // Create a ParagraphElement, set its text, and add it to the DivElement.
                var footnoteParagraph = taggedContent.CreateParagraphElement();
                footnoteParagraph.SetText("Footnote 1:"); // introductory text
                footnote.AppendChild(footnoteParagraph);

                // Append the footnote container to the document's structure root.
                root.AppendChild(footnote);

                // ------------------------------------------------------------
                // Build a table and attach it to the footnote
                // ------------------------------------------------------------
                TableElement table = taggedContent.CreateTableElement();
                table.AlternativeText = "Sample data table inside footnote";

                // Header section
                TableTHeadElement thead = taggedContent.CreateTableTHeadElement();
                table.AppendChild(thead);

                TableTRElement headerRow = taggedContent.CreateTableTRElement();
                thead.AppendChild(headerRow);

                TableTHElement th1 = taggedContent.CreateTableTHElement();
                th1.SetText("Item");
                headerRow.AppendChild(th1);

                TableTHElement th2 = taggedContent.CreateTableTHElement();
                th2.SetText("Quantity");
                headerRow.AppendChild(th2);

                // Body section
                TableTBodyElement tbody = taggedContent.CreateTableTBodyElement();
                table.AppendChild(tbody);

                // First data row
                TableTRElement dataRow1 = taggedContent.CreateTableTRElement();
                tbody.AppendChild(dataRow1);

                TableTDElement td1a = taggedContent.CreateTableTDElement();
                td1a.SetText("Apples");
                dataRow1.AppendChild(td1a);

                TableTDElement td1b = taggedContent.CreateTableTDElement();
                td1b.SetText("10");
                dataRow1.AppendChild(td1b);

                // Second data row
                TableTRElement dataRow2 = taggedContent.CreateTableTRElement();
                tbody.AppendChild(dataRow2);

                TableTDElement td2a = taggedContent.CreateTableTDElement();
                td2a.SetText("Oranges");
                dataRow2.AppendChild(td2a);

                TableTDElement td2b = taggedContent.CreateTableTDElement();
                td2b.SetText("5");
                dataRow2.AppendChild(td2b);

                // Attach the table to the footnote element
                footnote.AppendChild(table);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with footnote table: '{outputPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
