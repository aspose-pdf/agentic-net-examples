using System;
using System.IO;
using Aspose.Pdf;

class AttachmentTest
{
    static void Main()
    {
        // Create a temporary working directory
        string tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);
        try
        {
            string basePdfPath = Path.Combine(tempDir, "base.pdf");
            string attachedPdfPath = Path.Combine(tempDir, "attached.pdf");
            string attachmentPath = Path.Combine(tempDir, "sample.txt");

            // Create a small text file that will be attached to the PDF
            File.WriteAllText(attachmentPath, "Sample attachment content.");

            // Create a simple PDF with one blank page and save it as the baseline file
            using (Document doc = new Document())
            {
                doc.Pages.Add();
                doc.Save(basePdfPath);
            }

            long baseSize = new FileInfo(basePdfPath).Length;

            // Load the baseline PDF, add the attachment via EmbeddedFiles, and save the result
            using (Document doc = new Document(basePdfPath))
            {
                // Create a FileSpecification for the attachment and set its contents
                var fileSpec = new FileSpecification(Path.GetFileName(attachmentPath));
                fileSpec.Contents = new MemoryStream(File.ReadAllBytes(attachmentPath));

                // Add the file specification to the EmbeddedFiles collection
                doc.EmbeddedFiles.Add(fileSpec);

                doc.Save(attachedPdfPath);
            }

            long attachedSize = new FileInfo(attachedPdfPath).Length;

            // Verify that the file size increased after adding the attachment
            if (attachedSize <= baseSize)
            {
                throw new InvalidOperationException($"Attached PDF size ({attachedSize}) should be greater than base PDF size ({baseSize}).");
            }
            Console.WriteLine("Test passed: attached PDF size increased as expected.");
        }
        finally
        {
            // Clean up temporary files and directory
            if (Directory.Exists(tempDir))
            {
                Directory.Delete(tempDir, true);
            }
        }
    }
}
