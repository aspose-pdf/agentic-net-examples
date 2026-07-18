using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf   = "encrypted.pdf";   // Encrypted PDF path
        const string password   = "userPassword";   // Decryption password
        const string outputDir  = "ExtractedAttachments";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Open the encrypted PDF using the password
            using (Document doc = new Document(inputPdf, password))
            {
                // Decrypt the document (required before accessing embedded files)
                doc.Decrypt();

                // Iterate over embedded files (attachments) using reflection to avoid a direct
                // dependency on the EmbeddedFile type, which may vary between Aspose.Pdf versions.
                foreach (var embedded in doc.EmbeddedFiles)
                {
                    // Retrieve the file name via the "Name" property.
                    var nameProp = embedded.GetType().GetProperty("Name");
                    var saveMethod = embedded.GetType().GetMethod("Save", new[] { typeof(string) });

                    if (nameProp == null || saveMethod == null)
                        continue; // Skip if the expected members are not present.

                    string attachmentName = nameProp.GetValue(embedded) as string;
                    if (string.IsNullOrEmpty(attachmentName))
                        continue;

                    string attachmentPath = Path.Combine(outputDir, attachmentName);
                    // Invoke the Save(string) method to write the attachment to disk.
                    saveMethod.Invoke(embedded, new object[] { attachmentPath });
                    Console.WriteLine($"Extracted: {attachmentName}");
                }
            }

            Console.WriteLine("Attachment extraction completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
