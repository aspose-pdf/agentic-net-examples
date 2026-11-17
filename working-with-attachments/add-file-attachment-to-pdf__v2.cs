using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string attachmentPath = "attachment.txt";

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

        try
        {
            // Load the existing PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Create a FileSpecification for the attachment (description is optional)
                var fileSpec = new FileSpecification(attachmentPath, "Sample attachment");

                // Add the file specification to the EmbeddedFiles collection
                doc.EmbeddedFiles.Add(fileSpec);

                // Save the modified PDF
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Attachment added and saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
