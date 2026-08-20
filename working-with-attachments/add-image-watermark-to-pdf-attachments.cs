using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string watermarkImagePath = "watermark.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(watermarkImagePath))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkImagePath}");
            return;
        }

        // Load the main PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate over all embedded file attachments in the PDF
            foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
            {
                // Process only PDF attachments (case‑insensitive check)
                if (fileSpec.Name != null &&
                    fileSpec.Name.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                {
                    // Read the attachment content into a memory stream
                    using (MemoryStream originalStream = new MemoryStream())
                    {
                        // Ensure the source stream is at the beginning
                        if (fileSpec.Contents.CanSeek)
                            fileSpec.Contents.Position = 0;
                        fileSpec.Contents.CopyTo(originalStream);
                        originalStream.Position = 0;

                        // Load the attachment as a separate PDF document
                        using (Document attachedDoc = new Document(originalStream))
                        {
                            // Add the image watermark to every page of the attached PDF
                            foreach (Page page in attachedDoc.Pages)
                            {
                                ImageStamp stamp = new ImageStamp(watermarkImagePath)
                                {
                                    // Example size – adjust as needed
                                    Width = 200,
                                    Height = 100,
                                    HorizontalAlignment = HorizontalAlignment.Center,
                                    VerticalAlignment = VerticalAlignment.Center,
                                    Opacity = 0.5,
                                    Background = true // place behind page content
                                };
                                page.AddStamp(stamp);
                            }

                            // Save the modified attachment back into a new stream
                            using (MemoryStream updatedStream = new MemoryStream())
                            {
                                attachedDoc.Save(updatedStream);
                                updatedStream.Position = 0;

                                // Replace the original attachment data with the watermarked version
                                // Assign a fresh stream to the Contents property
                                fileSpec.Contents = new MemoryStream(updatedStream.ToArray());
                            }
                        }
                    }
                }
            }

            // Save the main document (now containing watermarked attachments)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with watermarked attachments saved to '{outputPdfPath}'.");
    }
}
