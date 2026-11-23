using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "attachments_demo.pdf";
        const string attachmentContent = "This is the content of the attachment.";
        const string attachmentFileName = "sample.txt";
        const string extractDir = "ExtractedAttachments";

        // Ensure the extraction directory exists
        Directory.CreateDirectory(extractDir);

        // -------------------------------------------------
        // Create a simple PDF and add multiple embedded files
        // with identical file names.
        // -------------------------------------------------
        using (Document doc = new Document())
        {
            // Add a blank page (required for a valid PDF)
            doc.Pages.Add();

            // Add three embedded files that all have the same name.
            for (int i = 0; i < 3; i++)
            {
                // Create a stream containing the attachment data.
                using (MemoryStream ms = new MemoryStream())
                using (StreamWriter writer = new StreamWriter(ms))
                {
                    writer.Write($"{attachmentContent} Instance {i + 1}");
                    writer.Flush();
                    ms.Position = 0; // Reset stream position for reading

                    // Create a FileSpecification with the duplicate name.
                    var fileSpec = new FileSpecification(attachmentFileName, $"Sample attachment instance {i + 1}");
                    fileSpec.Contents = ms; // Assign the stream as the file content

                    // Add the specification to the EmbeddedFiles collection.
                    doc.EmbeddedFiles.Add(fileSpec);
                }
            }

            // Save the PDF containing the embedded files.
            doc.Save(pdfPath);
        }

        // -------------------------------------------------
        // Load the PDF, extract each embedded file, and save them
        // with distinct generated file names.
        // -------------------------------------------------
        using (Document doc = new Document(pdfPath))
        {
            int counter = 1;
            foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
            {
                // Generate a unique file name for each extracted file.
                // Example: sample_1.txt, sample_2.txt, etc.
                string baseName = Path.GetFileNameWithoutExtension(fileSpec.Name);
                string extension = Path.GetExtension(fileSpec.Name);
                string uniqueName = $"{baseName}_{counter}{extension}";
                string outputPath = Path.Combine(extractDir, uniqueName);

                // Ensure the content stream is positioned at the start.
                if (fileSpec.Contents.CanSeek)
                    fileSpec.Contents.Position = 0;

                // Write the embedded file to disk.
                using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    fileSpec.Contents.CopyTo(outStream);
                }

                Console.WriteLine($"Extracted attachment saved as: {outputPath}");
                counter++;
            }
        }

        Console.WriteLine("All attachments have been extracted with distinct names.");
    }
}
