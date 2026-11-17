using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_updated.pdf";
        const string attachmentName = "myAttachment.txt"; // name of the attachment to modify
        const string newDescription = "Updated description for attachment";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Locate the embedded file (attachment) by its name using the EmbeddedFiles collection
                FileSpecification target = doc.EmbeddedFiles
                    .FirstOrDefault(f => f.Name.Equals(attachmentName, StringComparison.OrdinalIgnoreCase));

                if (target != null)
                {
                    // Update the description of the found attachment
                    target.Description = newDescription;
                    Console.WriteLine($"Attachment '{attachmentName}' description updated.");
                }
                else
                {
                    Console.WriteLine($"Attachment '{attachmentName}' not found.");
                }

                // Save the modified PDF document
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}