using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing the source PDFs
        string inputDirectory = "InputPdfs";
        // Directory where extracted attachments will be saved
        string outputDirectory = "ExtractedAttachments";

        // Ensure the output folder exists
        Directory.CreateDirectory(outputDirectory);

        // Verify the input directory exists; if not, inform the user and exit gracefully
        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory '{inputDirectory}' does not exist. No PDFs to process.");
            return;
        }

        // Get all PDF files in the input directory
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf");

        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found in the input directory.");
            return;
        }

        // Process each PDF in parallel
        Parallel.ForEach(pdfFiles, pdfPath =>
        {
            try
            {
                // Load the PDF document (lifecycle rule: wrap in using)
                using (Document doc = new Document(pdfPath))
                {
                    // If the document has no embedded files, skip it
                    if (doc.EmbeddedFiles == null || doc.EmbeddedFiles.Count == 0)
                        return;

                    // Iterate over each embedded file using dynamic to avoid compile‑time dependency on the concrete type
                    foreach (dynamic attachment in doc.EmbeddedFiles)
                    {
                        // Build a safe attachment name (fallback to "attachment" if name is missing)
                        string safeAttachmentName = string.IsNullOrWhiteSpace((string)attachment.Name)
                            ? "attachment"
                            : (string)attachment.Name;

                        // Build a unique file name to avoid collisions: <pdfFileName>_<attachmentName>
                        string pdfBaseName = Path.GetFileNameWithoutExtension(pdfPath);
                        string outputPath = Path.Combine(
                            outputDirectory,
                            $"{pdfBaseName}_{safeAttachmentName}");

                        // Save the attachment to disk
                        attachment.Save(outputPath);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log any errors but continue processing other files
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        });

        Console.WriteLine("Attachment extraction completed.");
    }
}
