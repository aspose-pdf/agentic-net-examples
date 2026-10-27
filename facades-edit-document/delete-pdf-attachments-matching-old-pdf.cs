using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (Document implements IDisposable)
        using (Document pdfDoc = new Document(inputPath))
        {
            // Collect the embedded file specifications that match the pattern
            var toRemove = new List<FileSpecification>();
            foreach (FileSpecification fileSpec in pdfDoc.EmbeddedFiles)
            {
                if (!string.IsNullOrEmpty(fileSpec.Name) &&
                    fileSpec.Name.EndsWith("_old.pdf", StringComparison.OrdinalIgnoreCase))
                {
                    toRemove.Add(fileSpec);
                }
            }

            // Remove the matching attachments using the Delete method (required by Aspose.Pdf)
            foreach (FileSpecification fileSpec in toRemove)
            {
                // The EmbeddedFiles collection does not expose a Remove(FileSpecification) overload.
                // Use Delete(string name) to remove the attachment by its name.
                pdfDoc.EmbeddedFiles.Delete(fileSpec.Name);
            }

            // Save the modified PDF
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Removed matching attachments and saved to '{outputPath}'.");
    }
}
