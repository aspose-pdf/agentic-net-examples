using System;
using System.IO;
using Aspose.Pdf;

class PdfAttachmentSaver
{
    /// <summary>
    /// Adds the specified files as attachments to the PDF and saves the result
    /// in the given output folder.
    /// </summary>
    /// <param name="inputPdfPath">Path to the source PDF file.</param>
    /// <param name="attachmentPaths">Array of file paths to be attached.</param>
    /// <param name="outputFolder">Folder where the new PDF will be saved.</param>
    public static void SavePdfWithAttachments(string inputPdfPath, string[] attachmentPaths, string outputFolder)
    {
        // Validate input PDF existence
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Build the output file name (original name + suffix)
        string outputFileName = Path.GetFileNameWithoutExtension(inputPdfPath) + "_with_attachments.pdf";
        string outputPath = Path.Combine(outputFolder, outputFileName);

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Add each file as an embedded file (attachment)
            foreach (string attachPath in attachmentPaths)
            {
                if (!File.Exists(attachPath))
                {
                    Console.Error.WriteLine($"Attachment not found and will be skipped: {attachPath}");
                    continue;
                }

                // Create a FileSpecification for the attachment
                var fileSpec = new FileSpecification(attachPath, Path.GetFileName(attachPath));
                // Embed the file contents (required for many PDF viewers)
                fileSpec.Contents = new MemoryStream(File.ReadAllBytes(attachPath));
                pdfDoc.EmbeddedFiles.Add(fileSpec);
            }

            // Save the modified PDF to the specified output path.
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"PDF with attachments saved to: {outputPath}");
    }

    // Example usage
    static void Main()
    {
        string sourcePdf = "sample.pdf";
        string[] filesToAttach = { "image1.png", "document.docx" };
        string destinationFolder = "OutputPdfs";

        SavePdfWithAttachments(sourcePdf, filesToAttach, destinationFolder);
    }
}