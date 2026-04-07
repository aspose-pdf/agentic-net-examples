using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF containing the attachment
        const string outputPdf = "output.pdf";         // PDF to write after updating description
        const string newDescription = "Updated attachment description";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIdx = 1; pageIdx <= doc.Pages.Count; pageIdx++)
            {
                Page page = doc.Pages[pageIdx];

                // Iterate through annotations on the page (also 1‑based)
                for (int annIdx = 1; annIdx <= page.Annotations.Count; annIdx++)
                {
                    Annotation ann = page.Annotations[annIdx];

                    // Identify FileAttachmentAnnotation instances
                    if (ann is FileAttachmentAnnotation fileAnn && fileAnn.File != null)
                    {
                        // Update the description of the attached file
                        fileAnn.File.Description = newDescription;
                        Console.WriteLine($"Updated description on page {pageIdx}, annotation {annIdx}");
                    }
                }
            }

            // Save the modified document
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with updated attachment description: {outputPdf}");
    }
}