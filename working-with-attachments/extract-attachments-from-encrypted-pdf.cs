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
            // Open the encrypted PDF supplying the user password
            using (Document doc = new Document(encryptedPdfPath, password))
            {
                // Optional: remove encryption for further operations
                doc.Decrypt();

                // Use the EmbeddedFiles collection – Attachments property does not exist
                if (doc.EmbeddedFiles != null && doc.EmbeddedFiles.Count > 0)
                {
                    foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
                    {
                        // Determine a safe file name for the extracted attachment
                        string safeName = Path.GetFileName(fileSpec.Name);
                        if (string.IsNullOrEmpty(safeName))
                            safeName = Guid.NewGuid().ToString();

                        string outputPath = Path.Combine(outputFolder, safeName);

                        // Write the file specification's content stream to disk
                        if (fileSpec.Contents != null)
                        {
                            if (fileSpec.Contents.CanSeek)
                                fileSpec.Contents.Position = 0;

                            using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                            {
                                fileSpec.Contents.CopyTo(outStream);
                            }

                            Console.WriteLine($"Extracted: {outputPath}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No embedded files found in the PDF.");
                }
            }

            Console.WriteLine("Attachment extraction completed.");
        }
        catch (InvalidPasswordException)
        {
            Console.Error.WriteLine("Incorrect password supplied for the encrypted PDF.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
