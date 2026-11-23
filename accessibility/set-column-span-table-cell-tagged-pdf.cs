using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "tagged_input.pdf";
        const string outputPath = "tagged_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Check for tagged content – Document.IsTagged does not exist in current API
                ITaggedContent tagged = doc.TaggedContent;
                if (tagged != null)
                {
                    // Retrieve all table cell elements (TableTDElement) in the structure tree
                    var cells = tagged.RootElement.FindElements<TableTDElement>(true);

                    if (cells.Count > 0)
                    {
                        // Set column span on the first cell found – use ColSpan, not ColumnSpan
                        TableTDElement firstCell = cells[0];
                        firstCell.ColSpan = 2; // span two columns
                    }
                }
                else
                {
                    Console.WriteLine("The PDF is not tagged; no structure modifications applied.");
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
