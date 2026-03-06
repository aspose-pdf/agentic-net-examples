using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // PDF containing file attachment annotations
        const string outputDir = "ExtractedAttachments"; // Folder where attachments will be saved

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF document (lifecycle: using block ensures proper disposal)
            using (Document doc = new Document(inputPdf))
            {
                // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];

                    // Iterate through all annotations on the page
                    for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                    {
                        Annotation ann = page.Annotations[annIndex];

                        // Check if the annotation is a FileAttachmentAnnotation
                        if (ann is FileAttachmentAnnotation fileAnn)
                        {
                            // The attached file specification
                            FileSpecification fileSpec = fileAnn.File;

                            // Use the original file name if available; otherwise generate a unique name
                            string fileName = !string.IsNullOrEmpty(fileSpec.Name)
                                ? fileSpec.Name
                                : $"attachment_page{pageIndex}_ann{annIndex}";

                            string outputPath = Path.Combine(outputDir, fileName);

                            // Copy the attachment stream to the output file
                            using (Stream source = fileSpec.Contents)
                            using (FileStream dest = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                            {
                                source.CopyTo(dest);
                            }

                            Console.WriteLine($"Extracted attachment: {outputPath}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}