using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least three pages
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document does not contain a third page.");
                return;
            }

            // Access the third page (Aspose.Pdf uses 1‑based indexing)
            Page thirdPage = doc.Pages[3];

            // Remove all annotations from the page
            // AnnotationCollection.Clear deletes every annotation in the collection
            thirdPage.Annotations.Clear();

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotations removed from page 3. Saved to '{outputPath}'.");
    }
}