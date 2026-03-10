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
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                ITaggedContent tagged = doc.TaggedContent;

                // Locate all table structure elements in the document
                var tables = tagged.RootElement.FindElements<TableElement>(true);

                if (tables.Count > 0)
                {
                    // Work with the first table found
                    TableElement table = tables[0];

                    // Find the table header (THead) element
                    var theadElements = table.FindElements<TableTHeadElement>(true);
                    if (theadElements.Count > 0)
                    {
                        TableTHeadElement thead = theadElements[0];

                        // Modify heading properties
                        thead.Title = "Modified Table Header";
                        thead.AlternativeText = "Header of the table describing column titles";
                        thead.Language = "en-US";

                        // Optionally update each header cell (TH) text and alt text
                        var headerCells = thead.FindElements<TableTHElement>(true);
                        foreach (var th in headerCells)
                        {
                            th.SetText("Header");
                            th.AlternativeText = "Column header";
                        }
                    }
                    else
                    {
                        Console.WriteLine("No THead element found in the table.");
                    }
                }
                else
                {
                    Console.WriteLine("No table elements found in the document.");
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