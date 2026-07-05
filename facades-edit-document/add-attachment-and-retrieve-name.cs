using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "input.pdf";
        const string attachmentPath = "attachment.txt";
        const string attachmentDescription = "Sample attachment";
        const string outputPdfPath = "output_with_attachment.pdf";

        // ------------------------------------------------------------
        // Ensure a source PDF exists – create a minimal one if it does not.
        // ------------------------------------------------------------
        if (!File.Exists(sourcePdfPath))
        {
            using (Document doc = new Document())
            {
                doc.Pages.Add(); // add a blank page
                doc.Save(sourcePdfPath);
            }
        }

        // ------------------------------------------------------------
        // Ensure the attachment file exists – create a simple text file.
        // ------------------------------------------------------------
        if (!File.Exists(attachmentPath))
        {
            File.WriteAllText(attachmentPath, "This is a sample attachment.");
        }

        // ------------------------------------------------------------
        // ---------- Add an attachment ----------
        // ------------------------------------------------------------
        using (Document pdfDoc = new Document(sourcePdfPath))
        {
            // Create a FileSpecification for the external file.
            var fileSpec = new FileSpecification(attachmentPath, attachmentDescription);
            // Populate the file contents via a stream (required for PDF/A compliance).
            fileSpec.Contents = new MemoryStream(File.ReadAllBytes(attachmentPath));
            // Add the specification to the document's EmbeddedFiles collection.
            pdfDoc.EmbeddedFiles.Add(fileSpec);
            // Save the updated PDF.
            pdfDoc.Save(outputPdfPath);
        }

        // ------------------------------------------------------------
        // ---------- Retrieve and verify the attachment name ----------
        // ------------------------------------------------------------
        using (Document pdfDoc = new Document(outputPdfPath))
        {
            IList<string> attachmentNames = new List<string>();
            foreach (FileSpecification spec in pdfDoc.EmbeddedFiles)
            {
                // The Name property holds the specification name.
                attachmentNames.Add(spec.Name);
            }

            foreach (string name in attachmentNames)
            {
                Console.WriteLine($"Attachment name: {name}");
            }
        }
    }
}
