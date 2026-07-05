using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF
        const string outputPdfPath = "output_pdfa3b.pdf"; // result PDF/A‑3b
        const string attachmentFilePath = "attachment.txt"; // external text file to embed

        // Verify required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(attachmentFilePath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFilePath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Create a FileSpecification for the external file and add it to the embedded files collection
            using (FileStream fs = File.OpenRead(attachmentFilePath))
            {
                FileSpecification fileSpec = new FileSpecification(fs, Path.GetFileName(attachmentFilePath));
                doc.EmbeddedFiles.Add(fileSpec);
            }

            // OPTIONAL: add a visible file‑attachment annotation on the first page
            Page firstPage = doc.Pages[1];
            // Define a small rectangle where the annotation icon will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 120, 120);
            // Use the first (and only) embedded file specification
            FileAttachmentAnnotation fileAnnot = new FileAttachmentAnnotation(firstPage, rect, doc.EmbeddedFiles[0])
            {
                // Icon property is optional; default icon will be used.
                Contents = "Attached text file"
            };
            firstPage.Annotations.Add(fileAnnot);

            // Convert the document to PDF/A‑3b compliance
            doc.Convert("conversion_log.xml", PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);

            // Save the compliant document
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF/A‑3b file with attachment saved to '{outputPdfPath}'.");
    }
}
