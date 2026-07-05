using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string attachmentName = "ZUGFeRD.xml"; // typical ZUGFeRD attachment name

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that the document contains embedded files
            if (doc.EmbeddedFiles != null)
            {
                bool found = false;

                // Search for the ZUGFeRD attachment by name
                foreach (var file in doc.EmbeddedFiles)
                {
                    if (string.Equals(file.Name, attachmentName, StringComparison.OrdinalIgnoreCase))
                    {
                        found = true;
                        break;
                    }
                }

                if (found)
                {
                    // Remove the specific attachment
                    doc.EmbeddedFiles.Delete(attachmentName);
                    Console.WriteLine($"Attachment '{attachmentName}' removed.");
                }
                else
                {
                    Console.WriteLine($"Attachment '{attachmentName}' not found.");
                }
            }

            // Save the modified PDF, preserving all other content
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}