using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source PDF
        const string inputPdfPath = "input.pdf";

        // Root folder where attachments will be organized
        const string outputRootFolder = "Attachments";

        // Verify the PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPdfPath}'.");
            return;
        }

        // Ensure the root output folder exists
        Directory.CreateDirectory(outputRootFolder);

        // Load the PDF document
        Document pdfDoc = new Document(inputPdfPath);

        // If there are no embedded files, exit early
        if (pdfDoc.EmbeddedFiles == null || pdfDoc.EmbeddedFiles.Count == 0)
        {
            Console.WriteLine("No attachments found in the PDF.");
            return;
        }

        // Iterate over each embedded file and save it to the appropriate folder
        foreach (FileSpecification fileSpec in pdfDoc.EmbeddedFiles)
        {
            // The name stored in the PDF (may include extension)
            string originalFileName = fileSpec.Name ?? "unnamed";

            // Determine the file extension (e.g., ".png", ".txt")
            string extension = Path.GetExtension(originalFileName);
            string extensionFolderName = string.IsNullOrEmpty(extension)
                ? "no_extension"
                : extension.TrimStart('.').ToLowerInvariant();

            // Build the target directory path and ensure it exists
            string targetFolder = Path.Combine(outputRootFolder, extensionFolderName);
            Directory.CreateDirectory(targetFolder);

            // Destination file path (handle possible name collisions)
            string destinationPath = Path.Combine(targetFolder, originalFileName);
            int duplicateIndex = 1;
            while (File.Exists(destinationPath))
            {
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(originalFileName);
                string newFileName = $"{fileNameWithoutExt}_{duplicateIndex}{extension}";
                destinationPath = Path.Combine(targetFolder, newFileName);
                duplicateIndex++;
            }

            // Write the attachment contents to disk
            using (Stream contentStream = fileSpec.Contents)
            {
                if (contentStream == null)
                {
                    Console.WriteLine($"Warning: Attachment '{originalFileName}' has no content and will be skipped.");
                    continue;
                }
                // Ensure the stream is positioned at the beginning
                if (contentStream.CanSeek)
                    contentStream.Position = 0;

                using (FileStream fileStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write))
                {
                    contentStream.CopyTo(fileStream);
                }
            }
        }

        Console.WriteLine("All attachments have been extracted and organized by file extension.");
    }
}
