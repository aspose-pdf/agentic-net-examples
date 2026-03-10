using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based (page-indexing rule)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Retrieve the background color of the page
                Aspose.Pdf.Color bgColor = page.Background;

                // If no background is set, the property may be null
                string colorInfo = bgColor != null ? bgColor.ToString() : "None";

                Console.WriteLine($"Page {i}: Background color = {colorInfo}");
            }
        }
    }
}