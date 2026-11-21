using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Enable tagging for accessibility
            ITaggedContent taggedContent = doc.TaggedContent;
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle("Table with Numbered List");

            // Root element of the structure tree
            StructureElement root = taggedContent.RootElement;

            // Create a table element
            TableElement table = taggedContent.CreateTableElement();
            table.AlternativeText = "Sample table containing a numbered list";
            root.AppendChild(table);

            // Create a table row (body)
            TableTBodyElement tbody = taggedContent.CreateTableTBodyElement();
            table.AppendChild(tbody);

            TableTRElement row = taggedContent.CreateTableTRElement();
            tbody.AppendChild(row);

            // Create a table cell (TD) where the numbered list will reside
            TableTDElement cell = taggedContent.CreateTableTDElement();
            row.AppendChild(cell);

            // Add numbered list items as separate paragraph elements
            for (int i = 1; i <= 5; i++)
            {
                ParagraphElement para = taggedContent.CreateParagraphElement();
                para.SetText($"{i}. List item number {i}");
                // Optionally set a language for each paragraph
                para.Language = "en-US";
                cell.AppendChild(para);
            }

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with table and numbered list saved to '{outputPath}'.");
    }
}