using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string attachmentToRemove = "OldReport.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document. Embedded files are accessed via the EmbeddedFiles collection.
            using (Document pdfDoc = new Document(inputPath))
            {
                // Remove the specified embedded file if it exists.
                bool removed = false;
                for (int i = pdfDoc.EmbeddedFiles.Count - 1; i >= 0; i--)
                {
                    FileSpecification fileSpec = pdfDoc.EmbeddedFiles[i];
                    if (string.Equals(fileSpec.Name, attachmentToRemove, StringComparison.OrdinalIgnoreCase))
                    {
                        // Delete expects the attachment name, not the index.
                        pdfDoc.EmbeddedFiles.Delete(fileSpec.Name);
                        removed = true;
                        // No need to continue looping after the target is removed.
                        break;
                    }
                }

                if (!removed)
                {
                    Console.WriteLine($"Attachment '{attachmentToRemove}' not found in the document.");
                }
                else
                {
                    Console.WriteLine($"Attachment '{attachmentToRemove}' removed.");
                }

                // Save the modified PDF.
                pdfDoc.Save(outputPath);
                Console.WriteLine($"Output saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
