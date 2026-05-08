using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "trimmed_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Skip completely blank pages using the default fill‑threshold (e.g., 0.01)
                if (page.IsBlank(0.01))
                    continue;

                // Calculate the bounding box of the actual content on the page
                Aspose.Pdf.Rectangle contentBox = page.CalculateContentBBox();

                // If a valid content box is returned, set it as the new TrimBox
                if (contentBox != null && contentBox.Width > 0 && contentBox.Height > 0)
                {
                    page.TrimBox = contentBox;
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Trimmed PDF saved to '{outputPath}'.");
    }
}