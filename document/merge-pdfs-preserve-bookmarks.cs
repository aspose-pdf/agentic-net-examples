using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string firstPdf  = "first.pdf";
        const string secondPdf = "second.pdf";
        const string outputPdf = "merged.pdf";

        // Verify input files exist
        if (!File.Exists(firstPdf))
        {
            Console.Error.WriteLine($"File not found: {firstPdf}");
            return;
        }
        if (!File.Exists(secondPdf))
        {
            Console.Error.WriteLine($"File not found: {secondPdf}");
            return;
        }

        try
        {
            // Load the first document (will become the target)
            using (Document target = new Document(firstPdf))
            {
                // Load the second document
                using (Document source = new Document(secondPdf))
                {
                    // Merge the source into the target.
                    // This operation preserves outlines (bookmarks) from both PDFs.
                    target.Merge(source);
                }

                // Save the merged result
                target.Save(outputPdf);
            }

            Console.WriteLine($"Merged PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during merge: {ex.Message}");
        }
    }
}