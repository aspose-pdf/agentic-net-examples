using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // required by task specification

namespace AsposePdfAttachmentTest
{
    class Program
    {
        // Entry point required for compilation
        static void Main(string[] args)
        {
            // Paths for temporary files
            string sourcePdfPath = Path.Combine(Path.GetTempPath(), "source.pdf");
            string attachmentPath = Path.Combine(Path.GetTempPath(), "attachment.txt");
            string outputPdfPath = Path.Combine(Path.GetTempPath(), "output_with_attachment.pdf");

            // Create a simple PDF to work with
            CreateSimplePdf(sourcePdfPath);

            // Create a dummy attachment file
            File.WriteAllText(attachmentPath, "This is a test attachment.");

            // Add the attachment using EmbeddedFiles collection
            using (Document doc = new Document(sourcePdfPath))
            {
                // FileSpecification represents the embedded file
                Aspose.Pdf.FileSpecification fileSpec = new Aspose.Pdf.FileSpecification(attachmentPath);
                // Set the name that will appear in the PDF attachment list
                fileSpec.Name = Path.GetFileName(attachmentPath);

                // Add to the EmbeddedFiles collection
                doc.EmbeddedFiles.Add(fileSpec);

                // Save the PDF with the attachment
                doc.Save(outputPdfPath);
            }

            // Verify that the attachment exists after saving
            bool attachmentFound = false;
            using (Document verifyDoc = new Document(outputPdfPath))
            {
                if (verifyDoc.EmbeddedFiles != null && verifyDoc.EmbeddedFiles.Count > 0)
                {
                    foreach (Aspose.Pdf.FileSpecification spec in verifyDoc.EmbeddedFiles)
                    {
                        // The Name property holds the attachment's display name
                        if (spec.Name == Path.GetFileName(attachmentPath))
                        {
                            attachmentFound = true;
                            break;
                        }
                    }
                }
            }

            // Output the test result
            Console.WriteLine(attachmentFound
                ? "Test Passed: Attachment is present after Save."
                : "Test Failed: Attachment not found after Save.");

            // Clean up temporary files (optional)
            try { File.Delete(sourcePdfPath); } catch { }
            try { File.Delete(attachmentPath); } catch { }
            try { File.Delete(outputPdfPath); } catch { }
        }

        // Helper method to create a minimal PDF document
        private static void CreateSimplePdf(string path)
        {
            using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document())
            {
                // Add a blank page
                doc.Pages.Add();

                // Save the blank PDF
                doc.Save(path);
            }
        }
    }
}