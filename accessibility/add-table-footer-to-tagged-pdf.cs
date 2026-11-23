using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "input_tagged.pdf";
        const string outputPath = "output_with_footer.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Verify that the PDF is tagged by checking the TaggedContent property (IsTagged does not exist).
            ITaggedContent? tagged = doc.TaggedContent;
            if (tagged == null)
            {
                Console.Error.WriteLine("The PDF is not a tagged PDF.");
                return;
            }

            // Find the first TableElement in the structure tree (recursive search).
            TableElement? table = null;
            var tables = tagged.RootElement.FindElements<TableElement>(true);
            if (tables.Count > 0)
                table = tables[0];

            // If no table exists, create one and attach it to the root element.
            if (table == null)
            {
                table = tagged.CreateTableElement();
                table.AlternativeText = "Generated table with footer";
                tagged.RootElement.AppendChild(table);
            }

            // Create the table footer (TFoot) – this method automatically attaches it to the table.
            TableTFootElement tFoot = table.CreateTFoot();

            // Create a row inside the footer.
            TableTRElement footRow = tFoot.CreateTR();

            // Create a cell (TD) and set its text.
            TableTDElement footCell = footRow.CreateTD();
            footCell.SetText("Footer: Summary information");
            footCell.AlternativeText = "Table footer cell containing summary";

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with table footer to '{outputPath}'.");
    }
}
