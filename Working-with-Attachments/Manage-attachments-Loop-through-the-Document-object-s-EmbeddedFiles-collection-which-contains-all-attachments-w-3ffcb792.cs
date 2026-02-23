using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Paths for input PDF and output PDF
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPath);

            // Access the collection of embedded files (attachments)
            EmbeddedFileCollection attachments = pdfDocument.EmbeddedFiles;

            // Iterate through each attachment
            foreach (FileSpecification fileSpec in attachments)
            {
                // Output attachment name
                Console.WriteLine($"Found attachment: {fileSpec.Name}");

                // Optional: extract the attachment to the same folder as the output PDF
                if (!string.IsNullOrEmpty(fileSpec.Name))
                {
                    string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath))
                                        ?? Directory.GetCurrentDirectory();
                    string attachmentPath = Path.Combine(outputDir, fileSpec.Name);

                    using (FileStream fs = new FileStream(attachmentPath, FileMode.Create, FileAccess.Write))
                    {
                        // Copy the attachment's content stream to a file
                        fileSpec.Contents.CopyTo(fs);
                    }

                    Console.WriteLine($"Saved attachment to: {attachmentPath}");
                }
            }

            // Save the (potentially unchanged) PDF document
            // document-save rule
            pdfDocument.Save(outputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}