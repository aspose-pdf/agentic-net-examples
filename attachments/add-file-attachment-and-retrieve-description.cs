using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string attachmentPath = "attachment.txt";
        const string outputPdf = "output_with_attachment.pdf";
        const string descriptionOutputPath = "attachment_description.txt";

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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create a FileSpecification for the attachment
            var fileSpec = new FileSpecification(attachmentPath, Path.GetFileName(attachmentPath))
            {
                // Set a description for the attachment
                Description = "Sample attachment description"
            };

            // Add the file specification to the document's embedded files collection
            doc.EmbeddedFiles.Add(fileSpec);

            // Save the modified PDF
            doc.Save(outputPdf);

            // Retrieve the description of the first embedded file (collections are 1‑based)
            if (doc.EmbeddedFiles.Count > 0)
            {
                string description = doc.EmbeddedFiles[1].Description ?? string.Empty;

                // Write the description to a text file
                File.WriteAllText(descriptionOutputPath, description);
                Console.WriteLine($"Attachment description saved to '{descriptionOutputPath}'.");
            }
            else
            {
                Console.WriteLine("No embedded files found in the document.");
            }
        }
    }
}
