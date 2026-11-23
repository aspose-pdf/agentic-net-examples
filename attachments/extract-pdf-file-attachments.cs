using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "ExtractedAttachments";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Iterate over all annotations on the page
                foreach (Annotation annot in page.Annotations)
                {
                    // Identify FileAttachmentAnnotation objects
                    if (annot is FileAttachmentAnnotation fileAnnot)
                    {
                        // The attached file is stored in the File property (FileSpecification)
                        FileSpecification fileSpec = fileAnnot.File;
                        if (fileSpec?.Contents != null)
                        {
                            // Use the original file name if available; otherwise generate a unique name
                            string fileName = !string.IsNullOrEmpty(fileSpec.Name)
                                ? fileSpec.Name
                                : $"attachment_page{pageIndex}_{Guid.NewGuid()}.bin";

                            string outputPath = Path.Combine(outputFolder, fileName);

                            // Save the embedded file to disk using the Contents stream
                            using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                            {
                                // Ensure the stream is at the beginning
                                if (fileSpec.Contents.CanSeek)
                                    fileSpec.Contents.Position = 0;
                                fileSpec.Contents.CopyTo(outStream);
                            }

                            Console.WriteLine($"Extracted: {outputPath}");
                        }
                    }
                }
            }
        }

        Console.WriteLine("Extraction completed.");
    }
}
