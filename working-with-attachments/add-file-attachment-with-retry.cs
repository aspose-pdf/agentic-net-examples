using System;
using System.IO;
using System.Threading;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

namespace PdfAttachmentUtility
{
    /// <summary>
    /// Helper for adding a file attachment to a PDF with retry logic for transient I/O errors.
    /// </summary>
    public static class PdfAttachmentHelper
    {
        /// <summary>
        /// Adds a file attachment to the first page of a PDF located on a network share.
        /// If a transient I/O error occurs, the operation is retried up to <paramref name="maxRetries"/> times.
        /// </summary>
        /// <param name="pdfPath">Full UNC or mapped path to the source PDF.</param>
        /// <param name="attachmentPath">Full path to the file that will be attached.</param>
        /// <param name="outputPath">Path where the modified PDF will be saved.</param>
        /// <param name="maxRetries">Maximum number of retry attempts (default 3).</param>
        /// <param name="delayMilliseconds">Delay between retries in milliseconds (default 1000).</param>
        public static void AddAttachmentWithRetry(
            string pdfPath,
            string attachmentPath,
            string outputPath,
            int maxRetries = 3,
            int delayMilliseconds = 1000)
        {
            if (string.IsNullOrWhiteSpace(pdfPath))
                throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));
            if (string.IsNullOrWhiteSpace(attachmentPath))
                throw new ArgumentException("Attachment path must be provided.", nameof(attachmentPath));
            if (string.IsNullOrWhiteSpace(outputPath))
                throw new ArgumentException("Output path must be provided.", nameof(outputPath));

            int attempt = 0;
            while (true)
            {
                try
                {
                    // Load the source PDF (using statement ensures deterministic disposal)
                    using (Document doc = new Document(pdfPath))
                    {
                        // Ensure the document has at least one page
                        if (doc.Pages.Count == 0)
                            throw new InvalidOperationException("The PDF contains no pages.");

                        // Use the first page for the attachment annotation
                        Page page = doc.Pages[1];

                        // Create a FileSpecification for the attachment file
                        FileSpecification fileSpec = new FileSpecification(attachmentPath);

                        // Define a rectangle where the attachment icon will appear
                        // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
                        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);

                        // Create the attachment annotation and add it to the page
                        FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec);
                        page.Annotations.Add(attachment);

                        // Save the modified PDF to the desired location
                        doc.Save(outputPath);
                    }

                    // If we reach this point the operation succeeded; exit the loop
                    break;
                }
                catch (IOException ex) when (attempt < maxRetries)
                {
                    // Transient I/O error – wait and retry
                    attempt++;
                    Console.Error.WriteLine($"I/O error on attempt {attempt}: {ex.Message}");
                    Thread.Sleep(delayMilliseconds);
                }
                catch (Exception ex)
                {
                    // Non‑retriable exception or max retries exceeded – rethrow
                    Console.Error.WriteLine($"Failed to add attachment: {ex.Message}");
                    throw;
                }
            }
        }
    }

    /// <summary>
    /// Minimal entry point required for a console application.
    /// It simply forwards command‑line arguments to <see cref="PdfAttachmentHelper.AddAttachmentWithRetry"/> when the correct number of arguments is supplied.
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            // Expected arguments: <pdfPath> <attachmentPath> <outputPath>
            if (args.Length == 3)
            {
                try
                {
                    PdfAttachmentHelper.AddAttachmentWithRetry(args[0], args[1], args[2]);
                    Console.WriteLine("Attachment added successfully.");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error: {ex.Message}");
                    Environment.Exit(1);
                }
            }
            else
            {
                Console.WriteLine("Usage: PdfAttachmentUtility <pdfPath> <attachmentPath> <outputPath>");
            }
        }
    }
}
