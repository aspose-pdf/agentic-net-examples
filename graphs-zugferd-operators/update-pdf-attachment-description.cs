using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "updated_attachments.pdf";
        const string attachmentName = "Report.pdf"; // name of the attachment to update
        const string newDescription = "Report – latest version (v2.3)";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Locate the embedded file (attachment) by its name
            FileSpecification target = null;
            foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
            {
                if (fileSpec.Name == attachmentName)
                {
                    target = fileSpec;
                    break;
                }
            }

            if (target == null)
            {
                Console.Error.WriteLine($"Attachment \"{attachmentName}\" not found.");
                return;
            }

            // Update the description property
            target.Description = newDescription;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Attachment description updated and saved to '{outputPath}'.");
    }
}
