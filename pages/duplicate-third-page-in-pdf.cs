using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document doc = new Document(inputPath))
            {
                // Ensure the document has at least three pages (1‑based indexing)
                if (doc.Pages.Count < 3)
                {
                    Console.Error.WriteLine("The PDF contains fewer than 3 pages.");
                    return;
                }

                // Duplicate page 3 and insert the copy immediately after it.
                // Aspose.Pdf no longer provides Page.Clone, so we copy the page via a temporary document.
                using (Document tempDoc = new Document())
                {
                    // Add a copy of page 3 to the temporary document. This creates a deep copy.
                    tempDoc.Pages.Add(doc.Pages[3]);

                    // Insert the copied page (which is now at index 1 in tempDoc) into the original document
                    // at position 4 (i.e., after the original page 3). Insert inserts *before* the given index.
                    doc.Pages.Insert(4, tempDoc.Pages[1]);
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Page 3 duplicated and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
