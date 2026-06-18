using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public class Program
{
    public static void Main()
    {
        // Step 1: Create a sample PDF (self‑contained example)
        using (Document sampleDoc = new Document())
        {
            // Add a page (1‑based indexing)
            Page page = sampleDoc.Pages.Add();
            // Add some text to the page
            TextFragment textFragment = new TextFragment("This is a sample PDF.");
            page.Paragraphs.Add(textFragment);
            // Save the PDF as a source file
            sampleDoc.Save("input.pdf");
        }

        // Step 2: Create a temporary attachment file
        string attachmentPath = "secret.txt";
        using (FileStream attachmentStream = new FileStream(attachmentPath, FileMode.Create, FileAccess.Write))
        {
            byte[] attachmentBytes = Encoding.UTF8.GetBytes("Sensitive information inside attachment.");
            attachmentStream.Write(attachmentBytes, 0, attachmentBytes.Length);
        }

        // Step 3: Open the PDF, add the attachment, encrypt the document, and save the result
        using (Document doc = new Document("input.pdf"))
        {
            // Create a FileSpecification for the attachment using the constructor (File and Description)
            FileSpecification fileSpec = new FileSpecification(attachmentPath, "Encrypted attachment");
            // Add the attachment to the PDF via the EmbeddedFiles collection
            doc.EmbeddedFiles.Add(fileSpec);

            // Define permissions (default permissions are sufficient for this example)
            Permissions permissions = new Permissions();
            // Choose an encryption algorithm
            CryptoAlgorithm cryptoAlgorithm = CryptoAlgorithm.AESx128;
            // Encrypt the PDF with user and owner passwords
            doc.Encrypt("user123", "owner123", permissions, cryptoAlgorithm);

            // Save the encrypted PDF
            doc.Save("output.pdf");
        }

        // Optional: delete the temporary attachment file
        if (File.Exists(attachmentPath))
        {
            File.Delete(attachmentPath);
        }
    }
}
