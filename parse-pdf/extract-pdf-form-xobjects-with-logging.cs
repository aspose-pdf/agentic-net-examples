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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                int formCounter = 0;
                // Iterate over each form XObject on the page
                foreach (XForm form in page.Resources.Forms)
                {
                    formCounter++;
                    // XForm provides a Name property; fall back to a generated name if null/empty
                    string formName = !string.IsNullOrEmpty(form.Name)
                        ? form.Name
                        : $"Form_{pageIndex}_{formCounter}";

                    // Extract text from the form using TextAbsorber
                    TextAbsorber absorber = new TextAbsorber();
                    absorber.Visit(form);

                    // Simple output of extracted form text
                    Console.WriteLine($"Form '{formName}' on page {pageIndex}:");
                    Console.WriteLine(absorber.Text);
                }
            }
        }

        // Capture end timestamp
        DateTime endTime = DateTime.UtcNow;
        Console.WriteLine($"Form extraction ended at {endTime:O}");
        Console.WriteLine($"Total duration: {(endTime - startTime).TotalSeconds} seconds");
    }
}
