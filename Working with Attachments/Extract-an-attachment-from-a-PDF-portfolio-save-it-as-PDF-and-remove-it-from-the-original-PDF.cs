using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "portfolio.pdf";
        const string outputAttachment = "extracted.pdf";
        const string outputPdf = "portfolio_updated.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF portfolio
        using (Document doc = new Document(inputPdf))
        {
            // Verify that the document contains at least one embedded file
            if (doc.EmbeddedFiles.Count == 0)
            {
                Console.WriteLine("No attachments found in the PDF portfolio.");
                return;
            }

            // Retrieve the first attachment (EmbeddedFileCollection uses 1‑based indexing)
            FileSpecification fileSpec = doc.EmbeddedFiles[1];
            string attachmentName = fileSpec.Name;

            // Extract the attachment content and save it as a separate PDF file
            using (FileStream outStream = File.Create(outputAttachment))
            using (Stream contentStream = fileSpec.Contents)
            {
                contentStream.CopyTo(outStream);
            }

            Console.WriteLine($"Attachment '{attachmentName}' extracted to '{outputAttachment}'.");

            // Remove the extracted attachment from the original PDF portfolio
            doc.EmbeddedFiles.Delete(attachmentName);

            // Save the modified PDF portfolio
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Updated PDF portfolio saved as '{outputPdf}'.");
    }
}
