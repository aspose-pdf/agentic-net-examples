using System;
using System.IO;
using System.Threading;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

namespace PdfAttachmentDemo
{
    class PdfAttachmentHelper
    {
        /// <summary>
        /// Adds a file attachment to a PDF stored on a network share.
        /// Retries the operation if a transient I/O error occurs.
        /// </summary>
        /// <param name="pdfPath">Full UNC path to the source PDF.</param>
        /// <param name="attachmentPath">Full UNC path to the file to attach.</param>
        /// <param name="outputPath">Full UNC path where the updated PDF will be saved.</param>
        /// <param name="maxRetries">Maximum number of retry attempts.</param>
        /// <param name="delayMs">Delay between retries in milliseconds.</param>
        public static void AddAttachmentWithRetry(
            string pdfPath,
            string attachmentPath,
            string outputPath,
            int maxRetries = 3,
            int delayMs = 1000)
        {
            if (string.IsNullOrEmpty(pdfPath)) throw new ArgumentException("PDF path is required.", nameof(pdfPath));
            if (string.IsNullOrEmpty(attachmentPath)) throw new ArgumentException("Attachment path is required.", nameof(attachmentPath));
            if (string.IsNullOrEmpty(outputPath)) throw new ArgumentException("Output path is required.", nameof(outputPath));

            int attempt = 0;
            while (true)
            {
                try
                {
                    // Load the PDF (lifecycle rule: use using)
                    using (Document doc = new Document(pdfPath))
                    {
                        // Ensure the attachment file exists
                        if (!File.Exists(attachmentPath))
                            throw new FileNotFoundException("Attachment file not found.", attachmentPath);

                        // Create a file specification for the attachment
                        FileSpecification fileSpec = new FileSpecification(attachmentPath);

                        // Define a rectangle for the annotation (position on the first page)
                        // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
                        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

                        // Create the attachment annotation on the first page
                        Page page = doc.Pages[1];
                        FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec)
                        {
                            // Optional visual settings
                            Color = Aspose.Pdf.Color.Blue,
                            Title = Path.GetFileName(attachmentPath),
                            Contents = "Attached file: " + Path.GetFileName(attachmentPath)
                        };

                        // Add the annotation to the page
                        page.Annotations.Add(attachment);

                        // Save the modified PDF (lifecycle rule: use Save)
                        doc.Save(outputPath);
                    }

                    // If we reach this point, the operation succeeded
                    break;
                }
                catch (IOException ex) when (attempt < maxRetries)
                {
                    // Transient I/O error – wait and retry
                    attempt++;
                    Console.Error.WriteLine($"I/O error on attempt {attempt}: {ex.Message}. Retrying in {delayMs} ms...");
                    Thread.Sleep(delayMs);
                }
                catch (Exception ex)
                {
                    // Non‑retriable error or max retries exceeded
                    Console.Error.WriteLine($"Failed to add attachment: {ex.Message}");
                    throw;
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Simple command‑line interface: <pdfPath> <attachmentPath> <outputPath>
            if (args.Length == 3)
            {
                PdfAttachmentHelper.AddAttachmentWithRetry(args[0], args[1], args[2]);
            }
            else
            {
                Console.WriteLine("Usage: PdfAttachmentDemo <pdfPath> <attachmentPath> <outputPath>");
                // Example (uncomment to test directly):
                // PdfAttachmentHelper.AddAttachmentWithRetry(
                //     @"\\fileserver\share\documents\source.pdf",
                //     @"\\fileserver\share\attachments\image.png",
                //     @"\\fileserver\share\documents\source_with_attachment.pdf");
            }
        }
    }
}
