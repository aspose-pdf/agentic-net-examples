using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

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

        // Capture start timestamp
        DateTime startTime = DateTime.UtcNow;
        Console.WriteLine($"Form extraction started at {startTime:O}");

        // Load PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // TextAbsorber will collect text from pages and form XObjects
            TextAbsorber absorber = new TextAbsorber();

            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Extract text from the page itself (optional)
                page.Accept(absorber);

                // Extract text from each form XObject on the page
                foreach (var form in page.Resources.Forms)
                {
                    absorber.Visit(form);
                }
            }

            // Output the extracted text (could be written to a file instead)
            Console.WriteLine("Extracted text:");
            Console.WriteLine(absorber.Text);
        }

        // Capture end timestamp
        DateTime endTime = DateTime.UtcNow;
        Console.WriteLine($"Form extraction ended at {endTime:O}");
        Console.WriteLine($"Total duration: {(endTime - startTime).TotalSeconds} seconds");
    }
}
