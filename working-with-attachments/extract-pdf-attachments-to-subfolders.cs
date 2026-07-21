using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputRootFolder = "Attachments";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the root output folder exists
        Directory.CreateDirectory(outputRootFolder);

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // If there are no embedded files, inform the user and exit
            if (pdfDoc.EmbeddedFiles == null || pdfDoc.EmbeddedFiles.Count == 0)
            {
                Console.WriteLine("No attachments found in the PDF.");
                return;
            }

            int attachmentIndex = 1;

            // Iterate over each embedded file using reflection to avoid a direct reference to the EmbeddedFile type
            foreach (object embeddedObj in pdfDoc.EmbeddedFiles)
            {
                var embeddedType = embeddedObj.GetType();
                var nameProp = embeddedType.GetProperty("Name");
                var contentProp = embeddedType.GetProperty("Content");

                string embeddedName = nameProp?.GetValue(embeddedObj) as string ?? string.Empty;
                byte[] embeddedContent = contentProp?.GetValue(embeddedObj) as byte[];

                // If we cannot retrieve the content, skip this entry
                if (embeddedContent == null)
                {
                    Console.WriteLine($"Skipping attachment #{attachmentIndex} because its content could not be read.");
                    attachmentIndex++;
                    continue;
                }

                // Determine a safe folder name for the attachment
                string baseFolderName = Path.GetFileNameWithoutExtension(embeddedName);
                if (string.IsNullOrWhiteSpace(baseFolderName))
                {
                    baseFolderName = $"Attachment_{attachmentIndex}";
                }

                // Create a subfolder for this attachment
                string attachmentFolder = Path.Combine(outputRootFolder, baseFolderName);
                Directory.CreateDirectory(attachmentFolder);

                // Determine the file name to write (fallback if Name is empty)
                string fileName = string.IsNullOrWhiteSpace(embeddedName)
                    ? $"attachment_{attachmentIndex}"
                    : embeddedName;

                string outputFilePath = Path.Combine(attachmentFolder, fileName);

                // Write the attachment's binary content to the file
                File.WriteAllBytes(outputFilePath, embeddedContent);

                Console.WriteLine($"Saved attachment '{embeddedName}' to '{outputFilePath}'");
                attachmentIndex++;
            }
        }
    }
}
