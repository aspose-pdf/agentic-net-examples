using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted.pdf";   // Path to the encrypted PDF
        const string password  = "user123";        // Decryption password
        const string outputDir = "Attachments";    // Folder to store extracted files

        // Ensure the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create output directory if it does not exist
        Directory.CreateDirectory(outputDir);

        try
        {
            // Open the encrypted PDF using the password (Document ctor handles encrypted files)
            using (Document doc = new Document(inputPath, password))
            {
                // Decrypt the document so that all content, including embedded files, is accessible
                doc.Decrypt();

                // Iterate over all embedded files (attachments) using reflection – avoids direct dependency on EmbeddedFile type
                foreach (var embedded in doc.EmbeddedFiles)
                {
                    // Get the attachment name via reflection
                    var nameProp = embedded.GetType().GetProperty("Name");
                    string attachmentName = nameProp?.GetValue(embedded) as string ?? "unknown.bin";

                    // Build a full path for the extracted attachment
                    string outPath = Path.Combine(outputDir, attachmentName);

                    // Try to invoke the Save(string) method via reflection
                    var saveMethod = embedded.GetType().GetMethod("Save", new[] { typeof(string) });
                    if (saveMethod != null)
                    {
                        saveMethod.Invoke(embedded, new object[] { outPath });
                    }
                    else
                    {
                        // Fallback: extract the raw stream from the FileSpecification if Save is unavailable
                        var fileSpec = embedded.GetType().GetProperty("FileSpecification")?.GetValue(embedded);
                        var contents = fileSpec?.GetType().GetProperty("Contents")?.GetValue(fileSpec) as Stream;
                        if (contents != null)
                        {
                            using (var fs = File.Create(outPath))
                                contents.CopyTo(fs);
                        }
                        else
                        {
                            Console.Error.WriteLine($"Unable to extract attachment: {attachmentName}");
                            continue;
                        }
                    }

                    Console.WriteLine($"Saved attachment: {outPath}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
