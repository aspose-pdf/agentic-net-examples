using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_no_annotations.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        int beforeCount = 0; // moved declaration outside the try block so it is visible later
        try
        {
            // Load the source PDF
            using (Document srcDoc = new Document(inputPath))
            {
                // Count annotations before deletion
                beforeCount = GetAnnotationsCount(srcDoc);
                Console.WriteLine($"Annotations before deletion: {beforeCount}");

                // Delete all annotations
                DeleteAllAnnotations(srcDoc);

                // Save the result to a new file
                srcDoc.Save(outputPath);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
            return; // exit if loading or processing fails
        }

        // Load the resulting PDF to verify the count after deletion
        using (Document outDoc = new Document(outputPath))
        {
            int afterCount = GetAnnotationsCount(outDoc);
            Console.WriteLine($"Annotations after deletion: {afterCount}");

            // Verify that the expected number of annotations were removed
            if (afterCount == 0 && beforeCount > 0)
            {
                Console.WriteLine("All annotations successfully removed.");
            }
            else if (afterCount == beforeCount)
            {
                Console.WriteLine("No annotations were removed.");
            }
            else
            {
                Console.WriteLine($"Removed {beforeCount - afterCount} annotation(s).");
            }
        }
    }

    // Helper method to count all annotations in a document
    private static int GetAnnotationsCount(Document doc)
    {
        int count = 0;
        foreach (Page page in doc.Pages)
        {
            count += page.Annotations.Count;
        }
        return count;
    }

    // Helper method to delete all annotations from a document
    private static void DeleteAllAnnotations(Document doc)
    {
        foreach (Page page in doc.Pages)
        {
            // Annotations collection is 1‑based; delete in reverse order to avoid index shift
            for (int i = page.Annotations.Count; i >= 1; i--)
            {
                page.Annotations.Delete(i);
            }
        }
    }
}
