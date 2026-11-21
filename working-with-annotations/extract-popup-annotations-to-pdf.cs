using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class RetrievePopupAnnotationResource
{
    static void Main()
    {
        // Input PDF that contains a popup (file attachment) annotation
        const string inputPdfPath = "input.pdf";

        // Output path for the extracted embedded PDF
        const string outputFilePath = "extracted_attachment.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            bool extracted = false;

            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= pdfDocument.Pages.Count; pageIndex++)
            {
                Page page = pdfDocument.Pages[pageIndex];

                // Iterate over all annotations on the current page
                foreach (Annotation annotation in page.Annotations)
                {
                    // Look for a FileAttachmentAnnotation (the popup that holds an embedded file)
                    if (annotation is FileAttachmentAnnotation fileAttachment)
                    {
                        // The attached file specification is accessed via the File property
                        FileSpecification fileSpec = fileAttachment.File;

                        // Ensure the file specification and its content stream are present
                        if (fileSpec != null && fileSpec.Contents != null)
                        {
                            // Copy the embedded stream to a separate PDF file
                            using (Stream sourceStream = fileSpec.Contents)
                            using (FileStream destinationStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
                            {
                                sourceStream.CopyTo(destinationStream);
                            }

                            Console.WriteLine($"Embedded file extracted to: {outputFilePath}");
                            extracted = true;
                            break; // Stop after the first successful extraction
                        }
                    }
                }

                if (extracted)
                    break;
            }

            if (!extracted)
                Console.WriteLine("No popup/file attachment annotation with an embedded file was found.");
        }
    }
}