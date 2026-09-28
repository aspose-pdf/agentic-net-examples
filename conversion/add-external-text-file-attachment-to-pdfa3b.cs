using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF
        const string attachmentPath = "notes.txt";        // external text file to embed
        const string outputPdfAPath = "output_pdfa3b.pdf"; // resulting PDF/A‑3b file
        const string conversionLogPath = "conversion_log.xml"; // optional log for conversion

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document doc = new Document(inputPdfPath))
            {
                // Create a FileSpecification for the external file (stream + file name)
                var fileSpec = new FileSpecification(
                    new MemoryStream(File.ReadAllBytes(attachmentPath)),
                    Path.GetFileName(attachmentPath));

                // Set the required PDF/A‑3 relationship (Source is appropriate for an attached document)
                fileSpec.AFRelationship = Aspose.Pdf.AFRelationship.Source;

                // Add the file to the document's EmbeddedFiles collection
                doc.EmbeddedFiles.Add(fileSpec);

                // Convert the document to PDF/A‑3b compliance
                doc.Convert(conversionLogPath, PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);

                // Save the PDF/A‑3b document with the embedded attachment
                doc.Save(outputPdfAPath);
            }

            Console.WriteLine($"PDF/A‑3b file created with attachment: {outputPdfAPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
