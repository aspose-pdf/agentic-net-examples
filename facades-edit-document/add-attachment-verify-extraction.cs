using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the file to attach, the PDF after attachment, and the extracted file
        const string sourcePdfPath      = "input.pdf";
        const string attachmentFilePath = "original.txt";
        const string outputPdfPath      = "withAttachment.pdf";
        const string extractedPath      = "extracted.txt";

        // Verify that the required files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(attachmentFilePath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFilePath}");
            return;
        }

        // ---------------------------------------------------------------
        // Add the attachment to the PDF using Document.EmbeddedFiles
        // ---------------------------------------------------------------
        using (var pdfDoc = new Document(sourcePdfPath))
        {
            // Create a FileSpecification for the attachment
            var fileSpec = new FileSpecification(Path.GetFileName(attachmentFilePath))
            {
                // Description is shown in the PDF attachment list (optional)
                Description = Path.GetFileName(attachmentFilePath)
            };

            // Provide the file data via a stream (required for EmbeddedFiles)
            fileSpec.Contents = new MemoryStream(File.ReadAllBytes(attachmentFilePath));

            // Add the specification to the EmbeddedFiles collection
            pdfDoc.EmbeddedFiles.Add(fileSpec);
            pdfDoc.Save(outputPdfPath);
        }

        // ---------------------------------------------------------------
        // Extract the attachment back from the PDF to verify its integrity
        // ---------------------------------------------------------------
        using (var pdfDoc = new Document(outputPdfPath))
        {
            FileSpecification targetSpec = null;
            string attachmentName = Path.GetFileName(attachmentFilePath);

            foreach (FileSpecification spec in pdfDoc.EmbeddedFiles)
            {
                // Match by Name or Description – both are commonly used
                if (spec.Name == attachmentName || spec.Description == attachmentName)
                {
                    targetSpec = spec;
                    break;
                }
            }

            if (targetSpec != null)
            {
                // Ensure the stream is positioned at the beginning
                if (targetSpec.Contents.CanSeek)
                    targetSpec.Contents.Position = 0;

                using (var outStream = new FileStream(extractedPath, FileMode.Create, FileAccess.Write))
                {
                    targetSpec.Contents.CopyTo(outStream);
                }
            }
            else
            {
                Console.Error.WriteLine($"Attachment '{attachmentName}' not found in PDF.");
                return;
            }
        }

        // -------------------------------------------------
        // Compare the original and extracted attachment bytes
        // -------------------------------------------------
        bool contentsMatch = false;
        if (File.Exists(extractedPath))
        {
            byte[] originalBytes  = File.ReadAllBytes(attachmentFilePath);
            byte[] extractedBytes = File.ReadAllBytes(extractedPath);

            if (originalBytes.Length == extractedBytes.Length)
            {
                contentsMatch = true;
                for (int i = 0; i < originalBytes.Length; i++)
                {
                    if (originalBytes[i] != extractedBytes[i])
                    {
                        contentsMatch = false;
                        break;
                    }
                }
            }
        }

        Console.WriteLine(contentsMatch
            ? "Attachment verified: contents match."
            : "Attachment verification failed: contents differ.");
    }
}
