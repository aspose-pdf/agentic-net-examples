using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_watermarked.pdf";
        const string watermarkImagePath = "watermark.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(watermarkImagePath))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkImagePath}");
            return;
        }

        // Load the main PDF document (lifecycle rule: use using)
        using (Document mainDoc = new Document(inputPdfPath))
        {
            // Iterate over all embedded file attachments
            // In Aspose.Pdf each attachment is a FileSpecification object accessed via EmbeddedFiles collection
            foreach (FileSpecification fileSpec in mainDoc.EmbeddedFiles)
            {
                // Process only PDF attachments (skip others)
                if (!Path.GetExtension(fileSpec.Name).Equals(".pdf", StringComparison.OrdinalIgnoreCase))
                    continue;

                // Read the original attachment data into a memory stream
                using (MemoryStream originalAttachment = new MemoryStream())
                {
                    // Ensure the source stream is at the beginning
                    fileSpec.Contents.Position = 0;
                    fileSpec.Contents.CopyTo(originalAttachment);
                    originalAttachment.Position = 0;

                    // Load the attachment PDF from the stream
                    using (Document attachedDoc = new Document(originalAttachment))
                    {
                        // Create an ImageStamp that will serve as the watermark
                        ImageStamp watermarkStamp = new ImageStamp(watermarkImagePath)
                        {
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                            Opacity = 0.3f,
                            Background = true
                        };

                        // Apply the watermark to every page of the attached PDF
                        foreach (Page page in attachedDoc.Pages)
                        {
                            page.AddStamp(watermarkStamp);
                        }

                        // Save the modified attachment back to a new memory stream
                        using (MemoryStream updatedAttachment = new MemoryStream())
                        {
                            attachedDoc.Save(updatedAttachment);
                            updatedAttachment.Position = 0;

                            // Replace the original attachment data with the watermarked version
                            // The Contents property is settable – assign the new stream
                            fileSpec.Contents = updatedAttachment;
                        }
                    }
                }
            }

            // Save the main document with all attachments now watermarked
            mainDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdfPath}'.");
    }
}
