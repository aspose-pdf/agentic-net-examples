using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class UpdateAttachmentDescription
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string newDescription = "Updated attachment description";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageIdx = 1; pageIdx <= doc.Pages.Count; pageIdx++)
            {
                Page page = doc.Pages[pageIdx];

                // Annotations collection also uses 1‑based indexing
                for (int annIdx = 1; annIdx <= page.Annotations.Count; annIdx++)
                {
                    Annotation ann = page.Annotations[annIdx];

                    // Look for a FileAttachmentAnnotation
                    if (ann is FileAttachmentAnnotation fileAnn && fileAnn.File != null)
                    {
                        // Update the description of the attached file
                        fileAnn.File.Description = newDescription;
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Attachment description updated and saved to '{outputPdf}'.");
    }
}