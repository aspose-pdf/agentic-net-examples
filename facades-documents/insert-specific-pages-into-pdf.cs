using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";      // PDF containing pages to insert
        const string destinationPath = "destination.pdf"; // PDF into which pages will be inserted
        const string outputPath = "result.pdf";      // Final PDF after insertion

        // Pages to insert from the source PDF (1‑based indexing)
        int[] pagesToInsert = new int[] { 2, 4, 5 };

        // Position in the destination PDF where the pages will be inserted.
        // Insert after this page number (1‑based). Use 0 to insert at the very beginning.
        int insertPosition = 3;

        if (!File.Exists(sourcePath) || !File.Exists(destinationPath))
        {
            Console.Error.WriteLine("Source or destination file not found.");
            return;
        }

        // Load both PDFs into memory streams
        using (FileStream srcFile = File.OpenRead(sourcePath))
        using (MemoryStream srcStream = new MemoryStream())
        using (FileStream destFile = File.OpenRead(destinationPath))
        using (MemoryStream destStream = new MemoryStream())
        {
            srcFile.CopyTo(srcStream);
            destFile.CopyTo(destStream);

            // Reset stream positions for reading
            srcStream.Position = 0;
            destStream.Position = 0;

            // Load the PDFs with the Document class
            Document srcDoc = new Document(srcStream);
            Document destDoc = new Document(destStream);

            // Ensure the pages are inserted in the order supplied
            Array.Sort(pagesToInsert);

            int offset = 0; // tracks how many pages have already been inserted
            foreach (int srcPageNumber in pagesToInsert)
            {
                // Aspose.Pdf inserts *before* the supplied page number.
                // To insert *after* insertPosition we add 1 + current offset.
                int targetPosition = insertPosition + offset + 1; // 1‑based index

                // Correct overload: insert a single Page object from the source document.
                // Pages collection is 1‑based, so we can access srcDoc.Pages[srcPageNumber].
                destDoc.Pages.Insert(targetPosition, srcDoc.Pages[srcPageNumber]);
                offset++;
            }

            // Save the modified PDF to the output file via a temporary stream
            using (MemoryStream resultStream = new MemoryStream())
            {
                destDoc.Save(resultStream);
                resultStream.Position = 0;
                using (FileStream outFile = File.Create(outputPath))
                {
                    resultStream.CopyTo(outFile);
                }
            }
        }

        Console.WriteLine($"Pages inserted successfully. Result saved to '{outputPath}'.");
    }
}
