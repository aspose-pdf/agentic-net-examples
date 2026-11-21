using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // PDF containing the attachment
        const string outputPdf = "output.pdf";        // PDF to write after updating
        const string attachmentName = "report.docx"; // Name of the attachment to update
        const string newDescription = "Report – version 2.1 (latest)";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Verify that the PDF actually contains embedded files
            if (doc.EmbeddedFiles == null || doc.EmbeddedFiles.Count == 0)
            {
                Console.Error.WriteLine("No embedded files found in the PDF.");
                return;
            }

            // Locate the embedded file (FileSpecification) with the requested name
            FileSpecification target = null;
            foreach (FileSpecification fs in doc.EmbeddedFiles)
            {
                if (string.Equals(fs.Name, attachmentName, StringComparison.OrdinalIgnoreCase))
                {
                    target = fs;
                    break;
                }
            }

            if (target == null)
            {
                Console.Error.WriteLine($"Attachment \"{attachmentName}\" not found.");
                return;
            }

            // Update the description property of the found attachment
            target.Description = newDescription;

            // Save the modified PDF (extension determines format; SaveOptions not required)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Attachment description updated and saved to \"{outputPdf}\".");
    }
}
