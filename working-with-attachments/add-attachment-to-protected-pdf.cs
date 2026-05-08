using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths and credentials
        const string inputPdf = "protected.pdf";
        const string outputPdf = "protected_with_attachment.pdf";
        const string userPassword = "user123";
        const string attachmentFile = "attachment.txt";
        const string attachmentDescription = "Sample attachment";

        // Validate input files
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(attachmentFile))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFile}");
            return;
        }

        // Open the encrypted PDF using the correct password
        using (Document doc = new Document(inputPdf, userPassword))
        {
            // Create a FileSpecification for the attachment
            FileSpecification fileSpec = new FileSpecification(attachmentFile, attachmentDescription);

            // Add the attachment to the document's EmbeddedFiles collection
            doc.EmbeddedFiles.Add(fileSpec);

            // Save the updated PDF (encryption is preserved)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Attachment added and saved to '{outputPdf}'.");
    }
}