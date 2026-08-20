using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string updatedDescription = "Latest version of the attached file";

        // ------------------------------------------------------------
        // 1. Ensure a source PDF exists – create a minimal PDF with a
        //    file‑attachment annotation so the example is self‑contained.
        // ------------------------------------------------------------
        if (!File.Exists(inputPdfPath))
        {
            // Create a dummy file that will be attached.
            byte[] dummyContent = System.Text.Encoding.UTF8.GetBytes("Dummy file content");
            using var dummyStream = new MemoryStream(dummyContent);

            // Build the PDF.
            using (var doc = new Document())
            {
                // Add a single page.
                Page page = doc.Pages.Add();

                // Create a FileSpecification for the dummy file.
                var fileSpec = new FileSpecification(dummyStream, "dummy.txt")
                {
                    Description = "Initial description",
                    // MIMEType property does not exist in Aspose.Pdf.FileSpecification –
                    // it is optional for this example and therefore omitted.
                    // AFRelationship is optional; keep it if needed.
                    AFRelationship = AFRelationship.Data
                };

                // Define the rectangle where the annotation will appear.
                var rect = new Aspose.Pdf.Rectangle(100, 600, 120, 620);

                // Create the file‑attachment annotation. NOTE: the constructor requires the
                // owning Page as the first argument, then the rectangle, then the FileSpecification.
                var fileAttachment = new FileAttachmentAnnotation(page, rect, fileSpec);

                // Add the annotation to the page.
                page.Annotations.Add(fileAttachment);

                // Save the placeholder PDF.
                doc.Save(inputPdfPath);
            }
        }

        // ------------------------------------------------------------
        // 2. Load the PDF and update the description of any file‑attachment
        //    annotations.
        // ------------------------------------------------------------
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            foreach (Page page in pdfDoc.Pages)
            {
                // Annotations collection is 1‑based.
                for (int idx = 1; idx <= page.Annotations.Count; idx++)
                {
                    Annotation annotation = page.Annotations[idx];
                    if (annotation is FileAttachmentAnnotation fileAttachment)
                    {
                        if (fileAttachment.File != null)
                        {
                            fileAttachment.File.Description = updatedDescription;
                        }
                    }
                }
            }

            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Attachment description updated and saved to '{outputPdfPath}'.");
    }
}
