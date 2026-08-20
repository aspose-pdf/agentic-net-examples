using System;
using System.IO;
using Aspose.Pdf;

class ExtractAttachments
{
    static void Main()
    {
        // Path to the source PDF file
        const string pdfPath = "input.pdf";

        // Base directory where subfolders for each attachment will be created
        const string outputBaseDir = "Attachments";

        // Validate input PDF existence
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure the base output directory exists
        Directory.CreateDirectory(outputBaseDir);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Iterate over all embedded files (attachments) in the PDF
            foreach (var embeddedFile in doc.EmbeddedFiles)
            {
                // Use reflection/dynamic to access the Name property and Save method without
                // depending on the concrete EmbeddedFile type (avoids CS0246).
                string attachmentFileName = (string)embeddedFile.GetType().GetProperty("Name")?.GetValue(embeddedFile) ?? "UnnamedAttachment";

                // Create a subfolder named after the attachment (without extension) to hold the file
                string subFolderName = Path.GetFileNameWithoutExtension(attachmentFileName);
                if (string.IsNullOrWhiteSpace(subFolderName))
                    subFolderName = Guid.NewGuid().ToString(); // fallback for empty names

                string subFolderPath = Path.Combine(outputBaseDir, subFolderName);
                Directory.CreateDirectory(subFolderPath);

                // Full path for the extracted file
                string outputFilePath = Path.Combine(subFolderPath, attachmentFileName);

                // Invoke the Save method via reflection
                var saveMethod = embeddedFile.GetType().GetMethod("Save", new[] { typeof(string) });
                if (saveMethod != null)
                {
                    saveMethod.Invoke(embeddedFile, new object[] { outputFilePath });
                }
                else
                {
                    // Fallback: try to get the file stream from the FileSpecification and copy it manually
                    var fileSpec = embeddedFile.GetType().GetProperty("FileSpecification")?.GetValue(embeddedFile);
                    var contents = fileSpec?.GetType().GetProperty("Contents")?.GetValue(fileSpec) as Stream;
                    if (contents != null)
                    {
                        using (var outStream = File.Create(outputFilePath))
                        {
                            contents.CopyTo(outStream);
                        }
                    }
                }

                Console.WriteLine($"Extracted '{attachmentFileName}' to '{subFolderPath}'");
            }
        }

        Console.WriteLine("All attachments have been extracted.");
    }
}
