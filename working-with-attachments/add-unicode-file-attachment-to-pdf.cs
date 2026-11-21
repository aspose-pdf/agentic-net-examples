using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the output PDF and the attachment file.
        const string sourcePdfPath = "source.pdf";
        const string outputPdfPath = "output_with_attachment.pdf";

        // Unicode filename for the attachment (e.g., Chinese characters).
        string unicodeFileName = "示例文件.txt";
        string attachmentFilePath = Path.Combine(Path.GetTempPath(), unicodeFileName);

        // Ensure the attachment file exists – create a simple text file.
        File.WriteAllText(attachmentFilePath, "This is a test attachment with a Unicode filename.");

        // If the source PDF does not exist, create a minimal PDF with one page.
        if (!File.Exists(sourcePdfPath))
        {
            using (Document doc = new Document())
            {
                Page page = doc.Pages.Add();
                page.Paragraphs.Add(new TextFragment("Sample PDF for attachment test."));
                doc.Save(sourcePdfPath);
            }
        }

        // Open the source PDF, add the attachment, and save the result.
        using (Document doc = new Document(sourcePdfPath))
        {
            // Create a FileSpecification for the attachment.
            // The Name property holds the Unicode name that will appear in PDF viewers.
            var attachmentSpec = new FileSpecification(Path.GetFileName(attachmentFilePath))
            {
                // Set the file data.
                Contents = new MemoryStream(File.ReadAllBytes(attachmentFilePath)),
                // Override the stored name with the Unicode filename.
                Name = unicodeFileName,
                Description = "Unicode attachment"
            };

            // Add the attachment to the PDF via the EmbeddedFiles collection.
            doc.EmbeddedFiles.Add(attachmentSpec);

            // Save the PDF with the attachment.
            doc.Save(outputPdfPath);
        }

        // Verify that the attachment is present and its filename is correct.
        using (Document verifiedDoc = new Document(outputPdfPath))
        {
            Console.WriteLine($"Attachments in '{outputPdfPath}':");
            foreach (FileSpecification fs in verifiedDoc.EmbeddedFiles)
            {
                // The Name property returns the Unicode name stored in the PDF.
                Console.WriteLine($"- {fs.Name}");
            }
        }

        // Clean up the temporary attachment file.
        try { File.Delete(attachmentFilePath); } catch { /* ignore cleanup errors */ }
    }
}
