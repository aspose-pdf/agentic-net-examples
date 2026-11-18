using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Select the page to modify (first page in this example)
            Page page = doc.Pages[1]; // 1‑based indexing

            // Locate the first vector graphic (Path) in the page's contents collection
            int indexToRemove = -1;
            for (int i = 1; i <= page.Contents.Count; i++) // 1‑based indexing
            {
                // The collection returns objects; we can inspect their runtime type directly.
                var content = page.Contents[i];
                if (content is Path)
                {
                    indexToRemove = i;
                    break; // stop after finding the first Path object
                }
            }

            if (indexToRemove != -1)
            {
                // Remove the identified vector graphic from the contents collection
                page.Contents.Delete(indexToRemove);
                Console.WriteLine($"Removed vector graphic at contents index {indexToRemove}.");
            }
            else
            {
                Console.WriteLine("No vector graphic (Path) found on the selected page.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
