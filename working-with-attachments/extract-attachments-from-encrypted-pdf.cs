using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string encryptedPdfPath = "encrypted.pdf";
        const string password = "userPassword";
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
            // Open the encrypted PDF supplying the user password
            using (Document doc = new Document(encryptedPdfPath, password))
            {
                // Decrypt the document (no parameters required)
                doc.Decrypt();

                // Check if there are any embedded files (attachments)
                if (doc.EmbeddedFiles != null && doc.EmbeddedFiles.Count > 0)
                {
                    // Iterate through the EmbeddedFiles collection
                    foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
                    {
                        string safeFileName = Path.GetFileName(fileSpec.Name);
                        string outputPath = Path.Combine(outputFolder, safeFileName);

                        // Write the attachment's content stream to disk
                        using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                        {
                            if (fileSpec.Contents.CanSeek)
                                fileSpec.Contents.Position = 0;
                            fileSpec.Contents.CopyTo(outStream);
                        }

                        Console.WriteLine($"Saved attachment: {outputPath}");
                    }
                }
                else
                {
                    Console.WriteLine("No attachments found in the PDF.");
                }
            }
        }
        catch (InvalidPasswordException)
        {
            Console.Error.WriteLine("Incorrect password provided for the encrypted PDF.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
