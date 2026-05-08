using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // Path to the source PDF
        const string outputPdfPath = "output.pdf";        // Path where the updated PDF will be saved
        const string newDescription = "Latest version of the attached file"; // New description text

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Iterate through all annotations on the current page
                for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                {
                    Annotation ann = page.Annotations[annIndex];

                    // Check if the annotation is a FileAttachmentAnnotation
                    if (ann is FileAttachmentAnnotation fileAnn && fileAnn.File != null)
                    {
                        // Update the description of the attached file
                        fileAnn.File.Description = newDescription;
                        Console.WriteLine($"Updated description on page {pageIndex}, annotation {annIndex}.");
                    }
                }
            }

            // Save the modified document (standard Save overload)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with updated attachment description to '{outputPdfPath}'.");
    }
}