using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string encryptedPdfPath = "encrypted.pdf";
        const string password = "user123";
        const string outputFolder = "Attachments";

        if (!File.Exists(encryptedPdfPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Open the encrypted PDF using the supplied password
            using (Document doc = new Document(encryptedPdfPath, password))
            {
                // Decrypt the document in memory (optional – Document constructor already opens it for reading)
                doc.Decrypt();

                // Iterate through all embedded files (attachments) using reflection to avoid compile‑time dependency on a specific class name
                foreach (var attachment in doc.EmbeddedFiles)
                {
                    // Get the attachment name
                    var nameProp = attachment.GetType().GetProperty("Name");
                    var name = nameProp?.GetValue(attachment) as string;
                    if (string.IsNullOrEmpty(name))
                        continue;

                    // Build the full path for the extracted attachment
                    string outputPath = Path.Combine(outputFolder, name);

                    // Invoke the Save(string) method via reflection
                    var saveMethod = attachment.GetType().GetMethod("Save", new[] { typeof(string) });
                    saveMethod?.Invoke(attachment, new object[] { outputPath });

                    Console.WriteLine($"Extracted: {outputPath}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
