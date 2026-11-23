using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath  = "input_tagged.pdf";
        const string outputPath = "output_tagged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that the document contains a tagged structure
            if (doc.TaggedContent == null)
            {
                Console.WriteLine("Document is not tagged. No structure to modify.");
                return;
            }

            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");                                   // optional language setting
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));   // optional title

            // Root of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Find the first TableElement in the structure (adjust as needed)
            TableElement table = root.FindElements<TableElement>(true).FirstOrDefault();
            if (table == null)
            {
                Console.WriteLine("No table found in the tagged structure.");
                doc.Save(outputPath);
                return;
            }

            // Locate the specific TableTDElement (cell) by its displayed text.
            // Here we look for a cell containing the word "Target".
            TableTDElement targetCell = null;
            foreach (TableTDElement cell in table.FindElements<TableTDElement>(true))
            {
                if (!string.IsNullOrEmpty(cell.ActualText) && cell.ActualText.Contains("Target"))
                {
                    targetCell = cell;
                    break;
                }
            }

            if (targetCell != null)
            {
                // Set the row span (merge with the cell below). Value of 2 means the cell spans two rows.
                targetCell.RowSpan = 2;
                Console.WriteLine("RowSpan set for the target cell.");
            }
            else
            {
                Console.WriteLine("Target cell not found.");
            }

            // Save the modified PDF. No PreSave() call is required.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}
