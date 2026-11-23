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
        const string outputPath = "output_cleared.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Determine whether the PDF is tagged by checking TaggedContent for null
                if (doc.TaggedContent == null)
                {
                    Console.WriteLine("Document is not tagged; no logical structure to modify.");
                }
                else
                {
                    ITaggedContent tagged = doc.TaggedContent;

                    // Find all TableElement objects in the structure tree (recursive search)
                    var tables = tagged.RootElement.FindElements<TableElement>(true);

                    foreach (TableElement table in tables)
                    {
                        // Remove all child elements from the table
                        table.ClearChilds();
                    }
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
