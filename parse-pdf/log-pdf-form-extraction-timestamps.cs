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

        // Load the PDF document (using the recommended lifecycle pattern)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Each page may contain XForm objects (form XObjects)
                foreach (XForm form in page.Resources.Forms)
                {
                    // Log start timestamp
                    DateTime startTime = DateTime.UtcNow;
                    Console.WriteLine($"[Form Extraction] Page {pageIndex}, Form '{form.Name}' started at {startTime:O}");

                    // Extract text from the form using TextAbsorber
                    TextAbsorber absorber = new TextAbsorber();
                    absorber.Visit(form); // extracts text from the XForm

                    // (Optional) Do something with the extracted text
                    string extractedText = absorber.Text;
                    // For demonstration, write the text to console (could be saved elsewhere)
                    Console.WriteLine($"Extracted Text ({form.Name}):");
                    Console.WriteLine(extractedText);

                    // Log end timestamp
                    DateTime endTime = DateTime.UtcNow;
                    Console.WriteLine($"[Form Extraction] Page {pageIndex}, Form '{form.Name}' ended at {endTime:O}");
                    Console.WriteLine($"Duration: {(endTime - startTime).TotalSeconds:F2} seconds");
                    Console.WriteLine(new string('-', 60));
                }
            }

            // No modifications are made, but if you need to save the document, use the standard Save method
            // doc.Save("output.pdf"); // Uncomment if saving is required
        }
    }
}