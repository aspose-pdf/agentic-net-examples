using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "cleaned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, then strip annotations (comments) and embedded file attachments.
        using (Document doc = new Document(inputPath))
        {
            // Remove all annotations from every page.
            foreach (Page page in doc.Pages)
            {
                page.Annotations?.Clear();
            }

            // Remove all embedded files (attachments) from the document.
            if (doc.EmbeddedFiles != null && doc.EmbeddedFiles.Count > 0)
            {
                // EmbeddedFileCollection uses 1‑based indexing. Delete each file by its name.
                for (int i = doc.EmbeddedFiles.Count; i >= 1; i--)
                {
                    var fileSpec = doc.EmbeddedFiles[i];
                    if (fileSpec != null && !string.IsNullOrEmpty(fileSpec.Name))
                    {
                        doc.EmbeddedFiles.Delete(fileSpec.Name);
                    }
                }
            }

            // Save the cleaned PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF cleaned and saved to '{outputPath}'.");
    }
}
