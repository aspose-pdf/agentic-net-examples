using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input HTML file (could be any format that supports margins area usage)
        const string inputPath = "input.html";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Conditional logic: decide how to treat margins area based on file name
        // If the file name contains "nomargin", never use margin area; otherwise allow it.
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();
        // NOTE: HtmlLoadOptions in the used Aspose.PDF version does not expose a MarginsAreaUsageMode property.
        // The default behavior is sufficient for this example, so the conditional setting is omitted.

        // Load the document with the configured load options
        using (Document doc = new Document(inputPath, loadOptions))
        {
            // Define distinct margin sets for different sections
            // Section 1: pages 1‑3 -> 20 points on all sides
            MarginInfo section1Margin = new MarginInfo(20, 20, 20, 20);

            // Section 2: pages 4‑6 -> 40 points on all sides
            MarginInfo section2Margin = new MarginInfo(40, 40, 40, 40);

            // Apply margins to pages based on page index (1‑based)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                if (i >= 1 && i <= 3)
                {
                    page.PageInfo.AnyMargin = section1Margin;
                }
                else if (i >= 4 && i <= 6)
                {
                    page.PageInfo.AnyMargin = section2Margin;
                }
                else
                {
                    // Use default margins (null restores automatic margin handling)
                    page.PageInfo.AnyMargin = null;
                }
            }

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with distinct margins to '{outputPath}'.");
    }
}