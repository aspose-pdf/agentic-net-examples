using System;
using System.IO;
using Aspose.Pdf;

class UpdateZugferdAttachment
{
    static void Main()
    {
        const string inputPdfPath = "invoice_original.pdf";
        const string newXmlPath = "invoice_updated.xml";
        const string outputPdfPath = "invoice_updated.pdf";
        const string attachmentName = "ZUGFeRD-invoice.xml";

        // Ensure source files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(newXmlPath))
        {
            Console.Error.WriteLine($"New ZUGFeRD XML not found: {newXmlPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document pdf = new Document(inputPdfPath))
        {
            // Remove existing ZUGFeRD attachment if it exists
            if (pdf.EmbeddedFiles[attachmentName] != null)
            {
                pdf.EmbeddedFiles.Delete(attachmentName);
            }

            // Read the updated XML into a memory stream
            byte[] xmlBytes = File.ReadAllBytes(newXmlPath);
            using (MemoryStream xmlStream = new MemoryStream(xmlBytes))
            {
                // Create a FileSpecification for the embedded file
                // The constructor that takes a file path is used only for the description;
                // we then replace the Contents with our stream.
                var fileSpec = new FileSpecification(attachmentName, "ZUGFeRD invoice XML");
                fileSpec.Contents = xmlStream;
                // Add the file specification to the PDF's EmbeddedFiles collection
                pdf.EmbeddedFiles.Add(fileSpec);
            }

            // Save the modified PDF
            pdf.Save(outputPdfPath);
        }

        Console.WriteLine($"ZUGFeRD attachment updated and saved to '{outputPdfPath}'.");
    }
}
