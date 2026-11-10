using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF containing embedded file attachments
        const string inputPdfPath = "input.pdf";

        // Directory where extracted attachments will be saved
        const string outputDir = "ExtractedAttachments";

        // Validate input file
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPdfPath}'.");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF document using the Document class (PdfFileEditor does not support extracting embedded files).
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Check if there are any embedded files.
                if (pdfDoc.EmbeddedFiles != null && pdfDoc.EmbeddedFiles.Count > 0)
                {
                    // Iterate over the embedded files (1‑based indexing) and write each to the output directory.
                    for (int i = 1; i <= pdfDoc.EmbeddedFiles.Count; i++)
                    {
                        FileSpecification fileSpec = pdfDoc.EmbeddedFiles[i];
                        string attachmentName = fileSpec.Name;
                        string outputPath = Path.Combine(outputDir, attachmentName);

                        using (FileStream outStream = File.Create(outputPath))
                        using (Stream contentStream = fileSpec.Contents)
                        {
                            contentStream.CopyTo(outStream);
                        }

                        Console.WriteLine($"Extracted: {attachmentName} -> {Path.GetFullPath(outputPath)}");
                    }
                }
                else
                {
                    Console.WriteLine("No embedded attachments were found in the PDF.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Extraction failed: {ex.Message}");
        }
    }
}
