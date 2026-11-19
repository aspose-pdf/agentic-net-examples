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
        const string outputPath = "output.pdf";

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
                // Access tagged content (creates a tagged structure if none exists)
                ITaggedContent taggedContent = doc.TaggedContent;
                taggedContent.SetLanguage("en-US");
                taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

                // Root element of the structure tree
                StructureElement root = taggedContent.RootElement;

                // ---------- Create a table ----------
                TableElement table = taggedContent.CreateTableElement();
                table.AlternativeText = "Sample data table with footnote references";
                root.AppendChild(table);

                // Table header
                TableTHeadElement thead = taggedContent.CreateTableTHeadElement();
                table.AppendChild(thead);
                TableTRElement headerRow = taggedContent.CreateTableTRElement();
                thead.AppendChild(headerRow);

                TableTHElement thProduct = taggedContent.CreateTableTHElement();
                thProduct.SetText("Product");
                headerRow.AppendChild(thProduct);

                TableTHElement thRevenue = taggedContent.CreateTableTHElement();
                thRevenue.SetText("Revenue");
                headerRow.AppendChild(thRevenue);

                // Table body
                TableTBodyElement tbody = taggedContent.CreateTableTBodyElement();
                table.AppendChild(tbody);

                // First data row with a footnote reference (superscript ¹)
                TableTRElement row1 = taggedContent.CreateTableTRElement();
                tbody.AppendChild(row1);

                TableTDElement tdItemA = taggedContent.CreateTableTDElement();
                tdItemA.SetText("Widget A\u00B9"); // \u00B9 = superscript 1
                row1.AppendChild(tdItemA);

                TableTDElement tdRevenueA = taggedContent.CreateTableTDElement();
                tdRevenueA.SetText("$50,000");
                row1.AppendChild(tdRevenueA);

                // Second data row with a footnote reference (superscript ²)
                TableTRElement row2 = taggedContent.CreateTableTRElement();
                tbody.AppendChild(row2);

                TableTDElement tdItemB = taggedContent.CreateTableTDElement();
                tdItemB.SetText("Widget B\u00B2"); // \u00B2 = superscript 2
                row2.AppendChild(tdItemB);

                TableTDElement tdRevenueB = taggedContent.CreateTableTDElement();
                tdRevenueB.SetText("$30,000");
                row2.AppendChild(tdRevenueB);

                // ---------- Add footnote paragraphs ----------
                ParagraphElement footnote1 = taggedContent.CreateParagraphElement();
                footnote1.SetText("\u00B9 Revenue includes taxes.");
                root.AppendChild(footnote1);

                ParagraphElement footnote2 = taggedContent.CreateParagraphElement();
                footnote2.SetText("\u00B2 Figures are estimates.");
                root.AppendChild(footnote2);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF with table and footnotes saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}