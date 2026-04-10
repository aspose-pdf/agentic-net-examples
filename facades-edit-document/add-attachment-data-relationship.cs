using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf      = "input.pdf";      // source PDF
        const string outputPdf     = "output.pdf";     // PDF with attachment
        const string attachmentFile = "attachment.pdf"; // file to attach
        const string description   = "Sample attachment";

        // Ensure source files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(attachmentFile))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFile}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Create a file specification for the attachment
            FileSpecification fileSpec = new FileSpecification(attachmentFile, description);

            // Set the relationship type to Data (AFRelationship.Data)
            fileSpec.AFRelationship = AFRelationship.Data;

            // Add the specification to the document's embedded files collection
            doc.EmbeddedFiles.Add(fileSpec);

            // Save the updated PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Attachment added and saved to '{outputPdf}'.");
    }
}