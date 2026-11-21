using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facades namespace is available as requested

class Program
{
    static void Main()
    {
        // Paths to the source and destination PDF files
        const string destinationPath = "destination.pdf";
        const string sourcePath      = "source.pdf";

        // Verify that both files exist
        if (!File.Exists(destinationPath))
        {
            Console.Error.WriteLine($"Destination file not found: {destinationPath}");
            return;
        }
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Load both PDFs completely into memory
        byte[] destBytes = File.ReadAllBytes(destinationPath);
        byte[] srcBytes  = File.ReadAllBytes(sourcePath);

        // Use nested using blocks for deterministic disposal (global rule)
        using (MemoryStream destStream = new MemoryStream(destBytes))
        using (MemoryStream srcStream  = new MemoryStream(srcBytes))
        using (Aspose.Pdf.Document destDoc = new Aspose.Pdf.Document(destStream))
        using (Aspose.Pdf.Document srcDoc  = new Aspose.Pdf.Document(srcStream))
        {
            // Insert all pages of the source document at the end of the destination document
            // Pages are 1‑based; inserting at Count+1 appends the pages.
            int insertPosition = destDoc.Pages.Count + 1;
            destDoc.Pages.Insert(insertPosition, srcDoc.Pages); // Correct overload: Insert(int, PageCollection)

            // Save the combined PDF into a MemoryStream
            using (MemoryStream outputStream = new MemoryStream())
            {
                destDoc.Save(outputStream);
                // Reset position so the stream can be read from the beginning by the caller
                outputStream.Position = 0;

                // Example: write the result to a file (optional)
                File.WriteAllBytes("combined.pdf", outputStream.ToArray());

                Console.WriteLine("Pages inserted and PDF saved to 'combined.pdf'.");
            }
        }
    }
}