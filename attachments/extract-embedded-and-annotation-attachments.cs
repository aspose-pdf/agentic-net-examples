using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class ExtractAttachments
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputDir    = "ExtractedAttachments";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // -----------------------------------------------------------------
            // 1. Extract EmbeddedFiles (files stored in the PDF's EmbeddedFiles collection)
            // -----------------------------------------------------------------
            foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
            {
                // Use the file name supplied in the specification; fall back to a generic name
                string fileName = !string.IsNullOrEmpty(fileSpec.Name)
                                  ? fileSpec.Name
                                  : "EmbeddedFile";

                string safeFilePath = GetUniqueFilePath(outputDir, fileName);

                // Save the embedded file's content to disk
                using (FileStream outStream = File.Create(safeFilePath))
                using (Stream content = fileSpec.Contents)
                {
                    content.CopyTo(outStream);
                }

                Console.WriteLine($"Embedded file saved: {safeFilePath}");
            }

            // -----------------------------------------------------------------
            // 2. Extract files attached via FileAttachmentAnnotation on any page
            // -----------------------------------------------------------------
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                foreach (Annotation annot in page.Annotations)
                {
                    if (annot is FileAttachmentAnnotation fileAnnot && fileAnnot.File != null)
                    {
                        FileSpecification spec = fileAnnot.File;

                        // Use the file name from the specification; otherwise generate a name
                        string fileName = !string.IsNullOrEmpty(spec.Name)
                                          ? spec.Name
                                          : $"Page{i}_Attachment";

                        string safeFilePath = GetUniqueFilePath(outputDir, fileName);

                        using (FileStream outStream = File.Create(safeFilePath))
                        using (Stream content = spec.Contents)
                        {
                            content.CopyTo(outStream);
                        }

                        Console.WriteLine($"Annotation attachment saved: {safeFilePath}");
                    }
                }
            }
        }

        Console.WriteLine("All attachments have been extracted.");
    }

    // Helper: returns a file path that does not overwrite an existing file
    private static string GetUniqueFilePath(string directory, string fileName)
    {
        // Remove any characters that are invalid in file names
        char[] invalidChars = Path.GetInvalidFileNameChars();
        string sanitized = string.Concat(Array.FindAll(fileName.ToCharArray(), c => Array.IndexOf(invalidChars, c) < 0));

        string fullPath = Path.Combine(directory, sanitized);
        string nameOnly = Path.GetFileNameWithoutExtension(sanitized);
        string extension = Path.GetExtension(sanitized);
        int counter = 1;

        while (File.Exists(fullPath))
        {
            string tempFileName = $"{nameOnly}_{counter}{extension}";
            fullPath = Path.Combine(directory, tempFileName);
            counter++;
        }

        return fullPath;
    }
}
