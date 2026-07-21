using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // Existing PDF with attachment(s)
        const string outputPdf = "output.pdf";         // PDF after updating description
        const string newDescription = "Latest version of the attached file";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Iterate over all annotations on the page (1‑based indexing)
                for (int idx = 1; idx <= page.Annotations.Count; idx++)
                {
                    Annotation ann = page.Annotations[idx];

                    // Check if the annotation is a file attachment
                    if (ann is FileAttachmentAnnotation fileAnn)
                    {
                        // Update the description of the attached file
                        if (fileAnn.File != null)
                        {
                            fileAnn.File.Description = newDescription;
                        }
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Attachment description updated and saved to '{outputPdf}'.");
    }
}