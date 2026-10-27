using System;
using System.IO;
using Aspose.Pdf;

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Temporary document to hold copies of pages 3‑6
            using (Document temp = new Document())
            {
                // Copy pages 3‑6 from the original document into the temporary document
                for (int i = 3; i <= 6; i++)
                {
                    // Pages.Add copies the page into the target document (deep copy)
                    temp.Pages.Add(doc.Pages[i]);
                }

                // Remove the original pages 3‑6 from the source document (delete from highest index downwards)
                for (int i = 6; i >= 3; i--)
                {
                    doc.Pages.Delete(i);
                }

                // Append the copied pages to the end of the original document
                foreach (Page copiedPage in temp.Pages)
                {
                    doc.Pages.Add(copiedPage);
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Pages 3‑6 have been moved to the end. Saved as '{outputPath}'.");
    }
}
