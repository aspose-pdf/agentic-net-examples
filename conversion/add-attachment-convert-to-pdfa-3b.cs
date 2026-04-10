using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath      = "input.pdf";          // source PDF
        const string attachmentFilePath = "attachment.txt";    // external text file to embed
        const string outputPdfPath     = "output_pdfa3b.pdf"; // resulting PDF/A‑3b file

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(attachmentFilePath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFilePath}");
            return;
        }

        // Load the source PDF, embed the external file, convert to PDF/A‑3b and save.
        using (Document doc = new Document(inputPdfPath))
        {
            // Create a FileSpecification for the external text file.
            using (FileStream fs = File.OpenRead(attachmentFilePath))
            {
                // The second argument is the name that will appear in the attachment list.
                FileSpecification fileSpec = new FileSpecification(fs, Path.GetFileName(attachmentFilePath));

                // Add the file specification to the document's embedded files collection.
                doc.EmbeddedFiles.Add(fileSpec);
            }

            // Convert the document to PDF/A‑3b compliance.
            // The conversion log is optional; an empty string disables logging.
            doc.Convert(string.Empty, PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);

            // Save the resulting PDF/A‑3b document.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF/A‑3b document with attachment saved to '{outputPdfPath}'.");
    }
}