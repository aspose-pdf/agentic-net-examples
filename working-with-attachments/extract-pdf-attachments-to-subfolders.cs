using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source PDF containing attachments (embedded files)
        const string inputPdfPath = "input.pdf";

        // Base folder where attachment subfolders will be created
        const string outputBaseFolder = "Attachments";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the base output folder exists
        Directory.CreateDirectory(outputBaseFolder);

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Use the EmbeddedFiles collection – Attachments property does not exist
            if (pdfDoc.EmbeddedFiles == null || pdfDoc.EmbeddedFiles.Count == 0)
            {
                Console.WriteLine("No embedded files (attachments) found in the PDF.");
                return;
            }

            // Iterate over each embedded file
            for (int i = 0; i < pdfDoc.EmbeddedFiles.Count; i++)
            {
                FileSpecification fileSpec = pdfDoc.EmbeddedFiles[i];

                // Determine a safe folder name for the attachment
                string safeFolderName = Path.GetFileNameWithoutExtension(fileSpec.Name);
                if (string.IsNullOrWhiteSpace(safeFolderName))
                {
                    safeFolderName = $"Attachment_{i + 1}";
                }

                // Combine base folder with the attachment‑specific subfolder
                string attachmentFolder = Path.Combine(outputBaseFolder, safeFolderName);
                Directory.CreateDirectory(attachmentFolder);

                // Full path where the attachment file will be written
                string attachmentFilePath = Path.Combine(attachmentFolder, fileSpec.Name);

                // Write the embedded file's contents to disk – FileSpecification.Save does not exist
                using (FileStream outStream = new FileStream(attachmentFilePath, FileMode.Create, FileAccess.Write))
                {
                    Stream contents = fileSpec.Contents;
                    if (contents != null)
                    {
                        if (contents.CanSeek)
                            contents.Position = 0;
                        contents.CopyTo(outStream);
                    }
                }

                Console.WriteLine($"Saved attachment '{fileSpec.Name}' to '{attachmentFolder}'.");
            }
        }

        Console.WriteLine("All attachments have been extracted.");
    }
}
