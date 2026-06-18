using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class UpdateAttachmentDescription
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF containing the attachment
        const string outputPdf = "output.pdf";         // PDF to write after update
        const string newDescription = "Updated attachment description";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            bool updated = false;

            // Iterate through all pages (1‑based indexing)
            for (int pageIdx = 1; pageIdx <= doc.Pages.Count; pageIdx++)
            {
                Page page = doc.Pages[pageIdx];

                // Annotations collection also uses 1‑based indexing
                for (int annIdx = 1; annIdx <= page.Annotations.Count; annIdx++)
                {
                    // Try to cast the annotation to FileAttachmentAnnotation
                    if (page.Annotations[annIdx] is FileAttachmentAnnotation fileAnn)
                    {
                        // The File property holds a FileSpecification object
                        // Its Description property stores the attachment description
                        fileAnn.File.Description = newDescription;
                        updated = true;
                        // If only one attachment needs updating, break out of loops
                        break;
                    }
                }

                if (updated) break;
            }

            if (!updated)
            {
                Console.WriteLine("No FileAttachmentAnnotation found in the document.");
            }

            // Save the modified document (PDF format)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Document saved as '{outputPdf}'.");
    }
}