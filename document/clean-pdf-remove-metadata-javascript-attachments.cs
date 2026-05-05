using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for FileAttachmentAnnotation

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

        // Load the PDF, process, and save – all within a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // 1. Remove all document metadata (Info dictionary, XMP metadata, etc.)
            doc.RemoveMetadata();

            // 2. Remove any JavaScript actions embedded in the PDF
            //    Clear document‑level OpenAction and page‑level actions.
            doc.OpenAction = null;
            foreach (Page page in doc.Pages)
            {
                page.Actions.OnOpen = null;
                page.Actions.OnClose = null;
            }

            // 3. Delete all embedded files (attachments) from the PDF
            foreach (Page page in doc.Pages)
            {
                // Iterate backwards because Delete shifts the collection indexes.
                for (int i = page.Annotations.Count; i >= 1; i--)
                {
                    if (page.Annotations[i] is FileAttachmentAnnotation)
                        page.Annotations.Delete(i);
                }
            }

            // 4. Optimize resources to drop unused objects and compress streams.
            //    The parameter‑less overload applies default safe optimizations.
            doc.OptimizeResources();

            // Save the cleaned PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cleaned PDF saved to '{outputPath}'.");
    }
}
