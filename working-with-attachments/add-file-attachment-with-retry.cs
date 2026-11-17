using System;
using System.IO;
using System.Threading;
using Aspose.Pdf;

class Program
{
    // Maximum number of retry attempts when a network error occurs
    private const int MaxRetryAttempts = 3;
    // Base delay in milliseconds for exponential back‑off
    private const int BaseDelayMs = 500;

    static void Main()
    {
        const string pdfPath = @"\\networkshare\documents\sample.pdf";
        const string attachmentPath = @"C:\Files\attachment.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment not found: {attachmentPath}");
            return;
        }

        bool success = AddAttachmentWithRetry(pdfPath, attachmentPath);
        Console.WriteLine(success ? "Attachment added successfully." : "Failed to add attachment after retries.");
    }

    /// <summary>
    /// Adds an attachment to a PDF stored on a network share.
    /// Retries the operation on transient I/O errors.
    /// </summary>
    /// <param name="pdfPath">Full path to the PDF file.</param>
    /// <param name="attachmentPath">Full path to the file to attach.</param>
    /// <returns>True if the attachment was added; otherwise false.</returns>
    private static bool AddAttachmentWithRetry(string pdfPath, string attachmentPath)
    {
        for (int attempt = 0; attempt <= MaxRetryAttempts; attempt++)
        {
            try
            {
                // Open the PDF inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Create a FileSpecification for the attachment.
                    // The Name property (first argument) is the file name that will appear in the PDF.
                    var fileSpec = new FileSpecification(Path.GetFileName(attachmentPath))
                    {
                        // Load the file bytes into a MemoryStream and assign it to Contents.
                        Contents = new MemoryStream(File.ReadAllBytes(attachmentPath))
                    };

                    // Add the specification to the EmbeddedFiles collection.
                    doc.EmbeddedFiles.Add(fileSpec);

                    // Overwrite the original PDF with the updated version.
                    doc.Save(pdfPath);
                }

                // If we reach this point, the operation succeeded
                return true;
            }
            catch (IOException ex) when (IsTransientNetworkError(ex))
            {
                // Transient network I/O error – decide whether to retry
                if (attempt == MaxRetryAttempts)
                {
                    Console.Error.WriteLine($"Final attempt failed: {ex.Message}");
                    return false;
                }

                // Exponential back‑off before the next retry
                int delay = BaseDelayMs * (int)Math.Pow(2, attempt);
                Console.WriteLine($"I/O error on attempt {attempt + 1}: {ex.Message}. Retrying in {delay} ms...");
                Thread.Sleep(delay);
            }
            catch (UnauthorizedAccessException ex)
            {
                // Permission issues are not transient – abort immediately
                Console.Error.WriteLine($"Access denied: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                // Any other unexpected exception – abort
                Console.Error.WriteLine($"Unexpected error: {ex.Message}");
                return false;
            }
        }

        // Should never reach here
        return false;
    }

    /// <summary>
    /// Determines whether an IOException is likely caused by a transient network problem.
    /// </summary>
    private static bool IsTransientNetworkError(IOException ex)
    {
        // Simple heuristic: check for common network‑related HResult codes
        // 0x80070035 – network path not found
        // 0x80070020 – sharing violation (file locked)
        // 0x8007000E – insufficient memory (could be network latency)
        int hresult = System.Runtime.InteropServices.Marshal.GetHRForException(ex);
        return hresult == unchecked((int)0x80070035) ||
               hresult == unchecked((int)0x80070020) ||
               hresult == unchecked((int)0x8007000E);
    }
}
