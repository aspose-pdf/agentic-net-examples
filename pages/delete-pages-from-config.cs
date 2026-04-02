using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;

namespace DeletePagesFromConfigExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Create a sample PDF with **4** pages (evaluation mode limit).
            // Aspose.PDF unlicensed mode allows a maximum of 4 elements in any collection.
            // Reduce the page count to stay within this limit. A full license removes the restriction.
            using (Document sampleDoc = new Document())
            {
                for (int i = 0; i < 4; i++)
                {
                    sampleDoc.Pages.Add();
                }
                sampleDoc.Save("input.pdf");
            }

            // Step 2: Prepare a configuration file with page numbers to delete
            // (One page number per line, 1‑based indexing)
            string[] configLines = new string[] { "2", "4" };
            File.WriteAllLines("pages.txt", configLines);

            // Step 3: Read page numbers from the configuration file
            string[] lines = File.ReadAllLines("pages.txt");
            int[] pagesToDelete = lines
                .Where(line => !String.IsNullOrWhiteSpace(line))
                .Select(line => Int32.Parse(line.Trim()))
                .ToArray();

            // Step 4: Load the PDF, delete the specified pages, and save the result
            using (Document doc = new Document("input.pdf"))
            {
                // Delete pages using the overload that accepts an int array
                doc.Pages.Delete(pagesToDelete);
                doc.Save("output.pdf");
            }
        }
    }
}
