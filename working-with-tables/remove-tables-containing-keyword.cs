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
        const string keyword    = "CONFIDENTIAL"; // keyword to search for (case‑insensitive)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block (lifecycle rule)
            using (Document doc = new Document(inputPath))
            {
                // Access tagged content (required for structural manipulation)
                ITaggedContent taggedContent = doc.TaggedContent;

                // Root of the logical structure tree
                StructureElement root = taggedContent.RootElement;

                // Find all table elements in the structure tree
                var tables = root.FindElements<TableElement>(true);

                foreach (TableElement table in tables)
                {
                    bool containsKeyword = false;

                    // Check header cells (TH) for the keyword
                    var headerCells = table.FindElements<TableTHElement>(true);
                    foreach (TableTHElement th in headerCells)
                    {
                        if (!string.IsNullOrEmpty(th.ActualText) &&
                            th.ActualText.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            containsKeyword = true;
                            break;
                        }
                    }

                    // If not found yet, check data cells (TD)
                    if (!containsKeyword)
                    {
                        var dataCells = table.FindElements<TableTDElement>(true);
                        foreach (TableTDElement td in dataCells)
                        {
                            if (!string.IsNullOrEmpty(td.ActualText) &&
                                td.ActualText.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                containsKeyword = true;
                                break;
                            }
                        }
                    }

                    // If the table contains the keyword, remove it from the structure tree
                    if (containsKeyword)
                    {
                        // Element.Remove() detaches the element from its parent
                        table.Remove();
                        Console.WriteLine("Removed a table containing the keyword.");
                    }
                }

                // Save the modified PDF (save inside the using block)
                doc.Save(outputPath);
                Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}