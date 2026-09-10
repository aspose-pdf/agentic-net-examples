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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate over all annotations on the page (1‑based indexing)
                for (int j = 1; j <= page.Annotations.Count; j++)
                {
                    Annotation ann = page.Annotations[j];

                    // Process only file attachment annotations
                    if (ann is FileAttachmentAnnotation fileAnn)
                    {
                        // Set a custom description (Subject property) for the attachment
                        fileAnn.Subject = "Custom attachment description";

                        // The attached file is described by a FileSpecification object.
                        // Set a custom description on the file specification itself.
                        if (fileAnn.File != null)
                        {
                            fileAnn.File.Description = "Custom file description";
                            // NOTE: Aspose.Pdf's FileSpecification does not expose a MimeType property
                            // in the current API version. If a MIME type needs to be stored, it can be
                            // embedded in the description or handled via a custom dictionary entry.
                        }
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with updated attachment metadata to '{outputPath}'.");
    }
}
