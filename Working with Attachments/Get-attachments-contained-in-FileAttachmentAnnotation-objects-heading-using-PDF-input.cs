using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputFolder = "Attachments";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Lifecycle rule: wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Annotations collection is also 1‑based
                for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                {
                    Annotation ann = page.Annotations[annIndex];

                    // Filter only FileAttachmentAnnotation objects
                    if (ann is FileAttachmentAnnotation fileAnn)
                    {
                        // The attached file is described by the FileSpecification object
                        FileSpecification fileSpec = fileAnn.File;

                        // Guard against missing specification or content stream
                        if (fileSpec?.Contents == null)
                            continue;

                        // Determine a safe file name for the extracted attachment
                        string rawName = fileSpec.Name ?? $"attachment_page{pageIndex}_ann{annIndex}";
                        string safeName = Path.GetFileName(rawName);
                        string outputPath = Path.Combine(outputFolder, safeName);

                        // Write the attachment stream to disk
                        using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                        {
                            // Reset stream position in case it is not at the beginning
                            if (fileSpec.Contents.CanSeek)
                                fileSpec.Contents.Position = 0;

                            fileSpec.Contents.CopyTo(outStream);
                        }

                        Console.WriteLine($"Extracted attachment: {outputPath}");
                    }
                }
            }
        }
    }
}