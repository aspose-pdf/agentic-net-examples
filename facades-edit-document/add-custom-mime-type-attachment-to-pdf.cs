using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string attachmentPath = "attachment.txt";
        const string outputPdf = "output.pdf";
        const string customMime = "application/custom-type";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Create a file specification for the attachment
            FileSpecification fileSpec = new FileSpecification(attachmentPath, "Attachment description");

            // Set the custom MIME type
            fileSpec.MIMEType = customMime;

            // Add the file specification to the PDF's embedded files collection
            doc.EmbeddedFiles.Add(fileSpec);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Attachment added with MIME type '{customMime}' to '{outputPdf}'.");
    }
}