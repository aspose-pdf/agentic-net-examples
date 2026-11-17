using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputRoot = "Attachments";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the root folder for extracted files exists
        Directory.CreateDirectory(outputRoot);

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // If there are no embedded files, inform the user and exit
                if (doc.EmbeddedFiles == null || doc.EmbeddedFiles.Count == 0)
                {
                    Console.WriteLine("No attachments (embedded files) found in the PDF.");
                    return;
                }

                int index = 0;
                foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
                {
                    index++;
                    string attachmentName = fileSpec.Name ?? $"attachment_{index}";

                    // Create a subfolder for this attachment (named after the file without extension)
                    string folderName = Path.GetFileNameWithoutExtension(attachmentName);
                    if (string.IsNullOrWhiteSpace(folderName))
                        folderName = $"attachment_{index}";
                    string subFolderPath = Path.Combine(outputRoot, folderName);
                    Directory.CreateDirectory(subFolderPath);

                    // Write the embedded file data to a file inside its subfolder
                    string outputFilePath = Path.Combine(subFolderPath, attachmentName);
                    using (FileStream outStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
                    {
                        Stream contentStream = fileSpec.Contents;
                        if (contentStream == null)
                        {
                            Console.WriteLine($"Warning: attachment '{attachmentName}' has no content.");
                            continue;
                        }
                        if (contentStream.CanSeek)
                            contentStream.Position = 0;
                        contentStream.CopyTo(outStream);
                    }
                    Console.WriteLine($"Extracted: {outputFilePath}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
