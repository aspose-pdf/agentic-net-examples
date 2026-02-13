using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class ExtractAttachment
{
    static void Main(string[] args)
    {
        // Input PDF file containing the attachment
        const string pdfPath = "sample.pdf";

        // Directory where extracted files will be saved
        const string outputDir = "extracted_attachments";

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        // Load the PDF document (load rule)
        Document pdfDocument = new Document(pdfPath);

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Iterate through all pages (Aspose.Pdf collections are 1‑based)
        for (int pageIndex = 1; pageIndex <= pdfDocument.Pages.Count; pageIndex++)
        {
            Page page = pdfDocument.Pages[pageIndex];

            // Iterate over annotations on the current page
            foreach (Annotation annotation in page.Annotations)
            {
                // Look for a FileAttachmentAnnotation
                if (annotation is FileAttachmentAnnotation fileAnnot)
                {
                    // The attached file specification
                    FileSpecification fileSpec = fileAnnot.File;
                    if (fileSpec == null)
                        continue; // No file attached, skip

                    // Determine the output file path
                    string fileName = fileSpec.Name ?? "attachment.bin";
                    string outputPath = Path.Combine(outputDir, fileName);

                    // Extract the file contents to disk
                    using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        fileSpec.Contents.CopyTo(outputStream);
                    }

                    Console.WriteLine($"Extracted attachment '{fileName}' to '{outputPath}'.");
                }
            }
        }

        // No modifications were made, but if you need to save the document you can use the save rule:
        // pdfDocument.Save("output.pdf");
    }
}