using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string newDescription = "Updated attachment description";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            bool updated = false;

            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Iterate through all annotations on the page (1‑based indexing)
                for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                {
                    Annotation ann = page.Annotations[annIndex];

                    // Check if the annotation is a file attachment
                    if (ann is FileAttachmentAnnotation fileAnn)
                    {
                        // Ensure the FileSpecification exists before updating
                        if (fileAnn.File != null)
                        {
                            fileAnn.File.Description = newDescription;
                            updated = true;
                        }
                    }
                }
            }

            if (!updated)
            {
                Console.WriteLine("No file attachment annotation found in the document.");
            }

            // Re‑save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}