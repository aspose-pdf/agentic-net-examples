using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Adjust these paths as needed. They can be absolute or relative to the executable's folder.
        string inputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.pdf");
        string outputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output.pdf");

        // Verify that the source PDF exists before attempting to open it.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: The file '{inputPath}' was not found. Please ensure the PDF exists at the specified location.");
            return;
        }

        try
        {
            using (Document document = new Document(inputPath))
            {
                // The ZUGFeRD XML attachment is usually named "invoice.xml" but it can differ.
                // First, check whether the attachment actually exists to avoid an exception.
                const string attachmentName = "invoice.xml";
                bool attachmentFound = false;
                foreach (FileSpecification fileSpec in document.EmbeddedFiles)
                {
                    if (string.Equals(fileSpec.Name, attachmentName, StringComparison.OrdinalIgnoreCase))
                    {
                        attachmentFound = true;
                        break;
                    }
                }

                if (attachmentFound)
                {
                    // Remove the ZUGFeRD attachment while keeping the rest of the PDF intact.
                    document.EmbeddedFiles.Delete(attachmentName);
                    Console.WriteLine($"Attachment '{attachmentName}' removed.");
                }
                else
                {
                    Console.WriteLine($"Attachment '{attachmentName}' not found – no changes were made.");
                }

                // Save the modified PDF.
                document.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}
