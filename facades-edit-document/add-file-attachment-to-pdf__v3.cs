using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Included as requested

class Program
{
    static void Main()
    {
        const string inputPdfPath      = "input.pdf";
        const string outputPdfPath     = "output.pdf";
        const string attachmentPath    = "Terms.pdf";
        const string attachmentDesc    = "Contract Terms";

        // Verify that the source PDF and the attachment file exist
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

        // Load the existing PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a FileSpecification for the attachment
            using (FileStream attachStream = File.OpenRead(attachmentPath))
            {
                // Constructor with stream and file name
                FileSpecification fileSpec = new FileSpecification(attachStream, Path.GetFileName(attachmentPath));

                // Set optional properties
                fileSpec.Description   = attachmentDesc;          // Human‑readable description
                fileSpec.AFRelationship = AFRelationship.Data;    // Relationship type (optional)

                // Add the attachment to the PDF's EmbeddedFiles collection
                pdfDoc.EmbeddedFiles.Add(fileSpec);
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Attachment '{attachmentPath}' added with description '{attachmentDesc}'. Saved as '{outputPdfPath}'.");
    }
}