using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source PDF (first argument or default)
        string pdfPath = args.Length > 0 ? args[0] : "sample.pdf";

        // Directory where extracted files will be saved (second argument or default)
        string outputDir = args.Length > 1 ? args[1] : "Attachments";

        // Verify that the PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found: {pdfPath}");
            return;
        }

        // Ensure the output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(pdfPath);

            // Extract document‑level embedded files
            ExtractEmbeddedFiles(pdfDocument, outputDir);

            // Extract file‑attachment annotations from each page
            ExtractFileAttachmentAnnotations(pdfDocument, outputDir);

            Console.WriteLine("Attachment extraction completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }

    // Handles files stored in the PDF's EmbeddedFiles collection
    static void ExtractEmbeddedFiles(Document doc, string outputDir)
    {
        var embeddedFiles = doc.EmbeddedFiles;
        if (embeddedFiles == null || embeddedFiles.Count == 0)
        {
            Console.WriteLine("No document‑level embedded files found.");
            return;
        }

        foreach (FileSpecification fileSpec in embeddedFiles)
        {
            string fileName = fileSpec.Name;
            string outPath = Path.Combine(outputDir, fileName);

            using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
            {
                fileSpec.Contents.CopyTo(fs);
            }

            Console.WriteLine($"Extracted embedded file: {fileName}");
        }
    }

    // Handles files attached via FileAttachmentAnnotation on pages
    static void ExtractFileAttachmentAnnotations(Document doc, string outputDir)
    {
        foreach (Page page in doc.Pages)
        {
            foreach (Annotation annot in page.Annotations)
            {
                if (annot is FileAttachmentAnnotation fileAnnot && fileAnnot.File != null)
                {
                    string fileName = fileAnnot.File.Name;
                    string outPath = Path.Combine(outputDir, fileName);

                    using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                    {
                        fileAnnot.File.Contents.CopyTo(fs);
                    }

                    // Fixed: use page.Number (PageInfo does not expose Number)
                    Console.WriteLine($"Extracted attachment from page {page.Number}: {fileName}");
                }
            }
        }
    }
}
