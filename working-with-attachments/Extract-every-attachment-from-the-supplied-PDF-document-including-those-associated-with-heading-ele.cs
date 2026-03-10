using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "Attachments";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Keep track of already extracted file names to avoid duplicates
        var extractedNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPdf))
        {
            // 1. Extract document‑level embedded files (1‑based indexing)
            for (int i = 1; i <= doc.EmbeddedFiles.Count; i++)
            {
                FileSpecification fileSpec = doc.EmbeddedFiles[i];
                string fileName = fileSpec.Name;

                if (extractedNames.Contains(fileName))
                    continue; // skip if already extracted

                string outPath = Path.Combine(outputDir, fileName);
                using (FileStream outStream = File.Create(outPath))
                using (Stream content = fileSpec.Contents)
                {
                    content.CopyTo(outStream);
                }

                extractedNames.Add(fileName);
                Console.WriteLine($"Extracted embedded file: {fileName}");
            }

            // 2. Extract file attachment annotations (including those on headings)
            // Pages are also 1‑based
            for (int p = 1; p <= doc.Pages.Count; p++)
            {
                Page page = doc.Pages[p];
                // Annotation collections are 1‑based
                for (int a = 1; a <= page.Annotations.Count; a++)
                {
                    Annotation ann = page.Annotations[a];
                    if (ann is FileAttachmentAnnotation fileAnn)
                    {
                        FileSpecification fileSpec = fileAnn.File;
                        string fileName = fileSpec.Name;

                        if (extractedNames.Contains(fileName))
                            continue; // skip duplicates

                        string outPath = Path.Combine(outputDir, fileName);
                        using (FileStream outStream = File.Create(outPath))
                        using (Stream content = fileSpec.Contents)
                        {
                            content.CopyTo(outStream);
                        }

                        extractedNames.Add(fileName);
                        Console.WriteLine($"Extracted attachment annotation: {fileName}");
                    }
                }
            }
        }

        Console.WriteLine("Attachment extraction completed.");
    }
}