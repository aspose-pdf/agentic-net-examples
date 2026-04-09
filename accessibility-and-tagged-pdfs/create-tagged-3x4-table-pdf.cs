using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string outputPath = "tagged_table.pdf";

        // Create a new PDF document and add a blank page (required for tagged content)
        using (Document doc = new Document())
        {
            doc.Pages.Add();

            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Optional: set language and title for the tagged PDF
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Table Example");

            // Get the root element of the structure tree
            StructureElement root = tagged.RootElement;

            // Create a Table element and attach it to the root
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample 3x4 table";
            root.AppendChild(table);

            // Create the table body and attach it to the table
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            // Build 3 rows × 4 columns
            for (int row = 1; row <= 3; row++)
            {
                // Create a table row and attach it to the body
                TableTRElement tr = tagged.CreateTableTRElement();
                tbody.AppendChild(tr);

                for (int col = 1; col <= 4; col++)
                {
                    // Create a table cell, set its text, and attach it to the row
                    TableTDElement td = tagged.CreateTableTDElement();
                    td.SetText($"R{row}C{col}");
                    tr.AppendChild(td);
                }
            }

            // Save the PDF (no PreSave call needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF with table saved to '{outputPath}'.");
    }
}