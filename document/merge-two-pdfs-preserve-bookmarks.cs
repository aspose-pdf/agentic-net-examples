using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF files to be merged
        const string firstPdf  = "first.pdf";
        const string secondPdf = "second.pdf";
        // Output merged PDF file
        const string outputPdf = "merged.pdf";

        // Verify that both source files exist
        if (!File.Exists(firstPdf) || !File.Exists(secondPdf))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            // Load the first document as the target; it will receive the pages and bookmarks of the second
            using (Document target = new Document(firstPdf))
            // Load the second document
            using (Document source = new Document(secondPdf))
            {
                // Merge the source document into the target.
                // This method preserves bookmarks from both documents.
                target.Merge(source);

                // Save the merged result as a PDF.
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