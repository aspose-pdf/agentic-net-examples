using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "updated.pdf";
        const string newDescription = "Updated attachment description";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            bool updated = false;

            // Iterate over all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Annotations collection is also 1‑based
                for (int i = 1; i <= page.Annotations.Count; i++)
                {
                    Annotation ann = page.Annotations[i];

                    // Look for a FileAttachmentAnnotation
                    if (ann is FileAttachmentAnnotation fileAnn)
                    {
                        // Ensure the FileSpecification exists before updating
                        if (fileAnn.File != null)
                        {
                            // Update the description of the attached file
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

            // Save the modified PDF (extension alone determines format; PDF is default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}