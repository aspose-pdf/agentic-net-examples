using System;
using System.IO;
using Aspose.Pdf; // Core PDF API

class ExtractAttachments
{
    static void Main()
    {
        const string inputPdf = "encrypted.pdf";   // Encrypted source PDF
        const string password = "user123";        // Password to open the PDF
        const string outputDir = "Attachments";   // Folder to store extracted files

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Open the encrypted PDF using the password (Document ctor overload)
            using (Document doc = new Document(inputPdf, password))
            {
                // Decrypt the document so that all content (including attachments) is accessible
                doc.Decrypt();

                // The EmbeddedFiles collection holds all file attachments.
                // Use reflection to avoid a direct compile‑time dependency on the EmbeddedFile type
                foreach (var attachment in doc.EmbeddedFiles)
                {
                    // Retrieve the attachment name via reflection
                    var nameProp = attachment.GetType().GetProperty("Name");
                    string safeName = nameProp != null ? Path.GetFileName(nameProp.GetValue(attachment) as string) : "unknown.bin";
                    string outPath = Path.Combine(outputDir, safeName);

                    // Invoke the Save(string) method via reflection
                    var saveMethod = attachment.GetType().GetMethod("Save", new[] { typeof(string) });
                    if (saveMethod != null)
                    {
                        saveMethod.Invoke(attachment, new object[] { outPath });
                        Console.WriteLine($"Extracted: {safeName} → {outPath}");
                    }
                    else
                    {
                        // Fallback: try to copy the raw stream if Save method is unavailable
                        var fileSpecProp = attachment.GetType().GetProperty("FileSpecification");
                        var fileSpec = fileSpecProp?.GetValue(attachment);
                        var contentsProp = fileSpec?.GetType().GetProperty("Contents");
                        var contents = contentsProp?.GetValue(fileSpec) as Stream;
                        if (contents != null)
                        {
                            using (var outStream = File.Create(outPath))
                                contents.CopyTo(outStream);
                            Console.WriteLine($"Extracted (stream): {safeName} → {outPath}");
                        }
                        else
                        {
                            Console.Error.WriteLine($"Unable to extract attachment: {safeName}");
                        }
                    }
                }
            }

            Console.WriteLine("Attachment extraction completed.");
        }
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Incorrect password: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
