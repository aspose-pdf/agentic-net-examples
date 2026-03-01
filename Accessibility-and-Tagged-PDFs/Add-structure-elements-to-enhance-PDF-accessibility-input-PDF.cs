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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Access the tagged content interface
                ITaggedContent tagged = doc.TaggedContent;

                // Set language and title for accessibility
                tagged.SetLanguage("en-US");
                tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

                // Get the root structure element (no cast needed)
                StructureElement root = tagged.RootElement;

                // Add a paragraph element
                ParagraphElement paragraph = tagged.CreateParagraphElement();
                paragraph.SetText("This PDF has been enhanced for accessibility.");
                root.AppendChild(paragraph);

                // Add a figure element with alternate text
                FigureElement figure = tagged.CreateFigureElement();
                figure.AlternativeText = "Sample chart showing quarterly revenue.";
                root.AppendChild(figure);

                // Add a table element with header and one data row
                TableElement table = tagged.CreateTableElement();
                table.AlternativeText = "Quarterly sales data table.";
                root.AppendChild(table);

                // Table header
                TableTHeadElement thead = tagged.CreateTableTHeadElement();
                table.AppendChild(thead);
                TableTRElement headerRow = tagged.CreateTableTRElement();
                thead.AppendChild(headerRow);
                TableTHElement th1 = tagged.CreateTableTHElement();
                th1.SetText("Product");
                headerRow.AppendChild(th1);
                TableTHElement th2 = tagged.CreateTableTHElement();
                th2.SetText("Revenue");
                headerRow.AppendChild(th2);

                // Table body
                TableTBodyElement tbody = tagged.CreateTableTBodyElement();
                table.AppendChild(tbody);
                TableTRElement dataRow = tagged.CreateTableTRElement();
                tbody.AppendChild(dataRow);
                TableTDElement td1 = tagged.CreateTableTDElement();
                td1.SetText("Widget A");
                dataRow.AppendChild(td1);
                TableTDElement td2 = tagged.CreateTableTDElement();
                td2.SetText("$45,000");
                dataRow.AppendChild(td2);

                // Save the modified PDF
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