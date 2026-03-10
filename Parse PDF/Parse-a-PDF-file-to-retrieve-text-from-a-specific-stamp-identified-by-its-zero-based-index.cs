using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    // Retrieves the text (Contents) of a StampAnnotation at the given zero‑based index on the first page.
    static void Main()
    {
        const string inputPath = "input.pdf";
        const int stampZeroBasedIndex = 0; // change as needed

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use using for deterministic disposal)
            using (Document doc = new Document(inputPath))
            {
                // Ensure the document has at least one page
                if (doc.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The document contains no pages.");
                    return;
                }

                // Aspose.Pdf uses 1‑based indexing for page collections and annotation collections.
                Page page = doc.Pages[1];

                // Validate the requested stamp index against the annotation count.
                // Annotation collections are also 1‑based, so we add 1 to the zero‑based index.
                int annotationIndex = stampZeroBasedIndex + 1;

                if (annotationIndex < 1 || annotationIndex > page.Annotations.Count)
                {
                    Console.Error.WriteLine($"Stamp index {stampZeroBasedIndex} is out of range. Page has {page.Annotations.Count} annotations.");
                    return;
                }

                // Retrieve the annotation at the calculated position.
                Annotation annotation = page.Annotations[annotationIndex];

                // We are interested only in StampAnnotation instances.
                if (annotation is StampAnnotation stamp)
                {
                    // The textual content of a stamp annotation is stored in the Contents property.
                    string stampText = stamp.Contents ?? string.Empty;
                    Console.WriteLine($"Stamp text at index {stampZeroBasedIndex}: \"{stampText}\"");
                }
                else
                {
                    Console.Error.WriteLine($"Annotation at index {stampZeroBasedIndex} is not a StampAnnotation (type: {annotation.GetType().Name}).");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}