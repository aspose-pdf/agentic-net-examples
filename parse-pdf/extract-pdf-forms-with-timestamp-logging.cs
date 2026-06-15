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

        try
        {
            // Capture start timestamp
            DateTime startTime = DateTime.UtcNow;
            Console.WriteLine($"Form extraction started at {startTime:O}");

            // Load PDF document (wrapped in using for proper disposal)
            using (Document doc = new Document(inputPath))
            {
                // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];

                    // Ensure the page has form resources
                    if (page.Resources?.Forms != null && page.Resources.Forms.Count > 0)
                    {
                        // Iterate over each XForm on the page
                        foreach (XForm form in page.Resources.Forms)
                        {
                            string formName = form.Name ?? "<unnamed>";

                            // Extract text from the form using TextAbsorber
                            TextAbsorber absorber = new TextAbsorber();
                            absorber.Visit(form);

                            Console.WriteLine($"Form '{formName}' on page {pageIndex}:");
                            Console.WriteLine(absorber.Text);
                        }
                    }
                }
            }

            // Capture end timestamp
            DateTime endTime = DateTime.UtcNow;
            Console.WriteLine($"Form extraction ended at {endTime:O}");
            Console.WriteLine($"Total duration: {(endTime - startTime).TotalSeconds} seconds");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}
