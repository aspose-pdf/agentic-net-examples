using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string attachmentName = "myfile.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Check if the attachment exists
                bool found = false;
                foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
                {
                    if (fileSpec.Name.Equals(attachmentName, StringComparison.OrdinalIgnoreCase))
                    {
                        found = true;
                        break;
                    }
                }

                if (found)
                {
                    // Delete the attachment by its filename
                    doc.EmbeddedFiles.Delete(attachmentName);

                    // Save the modified PDF
                    doc.Save(outputPath);
                    Console.WriteLine($"Attachment '{attachmentName}' deleted. Saved to '{outputPath}'.");
                }
                else
                {
                    Console.WriteLine($"Attachment '{attachmentName}' not found in the PDF.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
