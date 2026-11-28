using System;
using System.IO;
using Aspose.Pdf.Facades;          // PdfExtractor
using Aspose.Pdf;                // Document, FileSpecification

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputDir = "Attachments";
        const string metadataFile = "Metadata.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // ---------------------------------------------------------------------
        // 1. Extract attachments using PdfExtractor (the API that is guaranteed to
        //    exist across Aspose.Pdf versions).
        // ---------------------------------------------------------------------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(pdfPath);
            extractor.ExtractAttachment(outputDir);
        }

        // ---------------------------------------------------------------------
        // 2. Retrieve attachment metadata.  The older PdfExtractor API does not
        //    expose GetAttachmentCount / GetAttachmentInfo, therefore we fall
        //    back to the Document model which always provides the EmbeddedFiles
        //    collection.
        // ---------------------------------------------------------------------
        Document pdfDoc = new Document(pdfPath);
        int attachmentCount = pdfDoc.EmbeddedFiles.Count;

        using (StreamWriter writer = new StreamWriter(metadataFile, false))
        {
            writer.WriteLine($"PDF: {Path.GetFileName(pdfPath)}");
            writer.WriteLine($"Total Attachments: {attachmentCount}");
            writer.WriteLine();

            for (int i = 0; i < attachmentCount; i++)
            {
                // The collection returns a FileSpecification object that contains
                // the same information that AttachmentInfo would have provided.
                var spec = pdfDoc.EmbeddedFiles[i];

                // CreationDate is not directly exposed – we output N/A.
                string creationDate = "N/A";

                // Modification date handling – Params may be null and ModDate is a
                // non‑nullable DateTime. We need to guard against a missing Params
                // object and against the default DateTime value.
                string modificationDate = "N/A";
                if (spec.Params != null && spec.Params.ModDate != default(DateTime))
                {
                    modificationDate = spec.Params.ModDate.ToString();
                }

                // Size can be obtained from Params.Size; if not available we fall back to the stream length.
                long size = spec.Params?.Size ?? (spec.Contents?.Length ?? 0);

                writer.WriteLine($"Attachment #{i + 1}");
                writer.WriteLine($"  Name               : {spec.Name}");
                writer.WriteLine($"  Description        : {spec.Description}");
                writer.WriteLine($"  Creation Date      : {creationDate}");
                writer.WriteLine($"  Modification Date  : {modificationDate}");
                writer.WriteLine($"  Size (bytes)       : {size}");
                writer.WriteLine();
            }
        }

        Console.WriteLine($"Attachments extracted to '{outputDir}'.");
        Console.WriteLine($"Metadata written to '{metadataFile}'.");
    }
}
