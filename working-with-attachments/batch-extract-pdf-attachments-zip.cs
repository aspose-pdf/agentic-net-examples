using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class BatchAttachmentExtractor
{
    static void Main()
    {
        // Folder containing the source PDF files. Use a path that works on any OS.
        // If the absolute Windows style path does not exist, fall back to a relative folder.
        const string rawInputFolder = @"C:\InputPdfs"; // original intent
        string inputFolder = Directory.Exists(rawInputFolder) ? rawInputFolder : Path.GetFullPath("InputPdfs");

        // Path for the consolidated ZIP archive (also made OS‑agnostic)
        const string rawOutputZipPath = @"C:\Output\attachments.zip";
        string outputZipPath = Path.GetFullPath(
            Directory.Exists(Path.GetDirectoryName(rawOutputZipPath) ?? string.Empty)
                ? rawOutputZipPath
                : Path.Combine("Output", "attachments.zip"));

        // Ensure the output directory exists
        string? outputDir = Path.GetDirectoryName(outputZipPath);
        if (!string.IsNullOrEmpty(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Collect all PDF files in the input folder
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found in the specified folder.");
            return;
        }

        // Create the ZIP archive (overwrite if it already exists)
        using (FileStream zipStream = new FileStream(outputZipPath, FileMode.Create))
        using (ZipArchive zip = new ZipArchive(zipStream, ZipArchiveMode.Create))
        {
            foreach (string pdfPath in pdfFiles)
            {
                // Open each PDF inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
                    for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                    {
                        Page page = doc.Pages[pageIndex];

                        // Iterate through all annotations on the page
                        for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                        {
                            Annotation ann = page.Annotations[annIndex];

                            // We're interested only in file attachment annotations
                            if (ann is FileAttachmentAnnotation fileAnn && fileAnn.File != null)
                            {
                                FileSpecification fileSpec = fileAnn.File;

                                // Determine a unique entry name inside the ZIP:
                                // <pdf‑file‑name>/<attachment‑file‑name>
                                string pdfBaseName = Path.GetFileNameWithoutExtension(pdfPath);
                                string attachmentName = !string.IsNullOrEmpty(fileSpec.Name)
                                    ? fileSpec.Name
                                    : $"attachment_{Guid.NewGuid()}";
                                string entryName = $"{pdfBaseName}/{attachmentName}";

                                // Create a new entry in the ZIP archive
                                ZipArchiveEntry entry = zip.CreateEntry(entryName, CompressionLevel.Optimal);
                                using (Stream entryStream = entry.Open())
                                using (Stream contentStream = fileSpec.Contents)
                                {
                                    // Copy the embedded file data directly into the ZIP entry
                                    contentStream.CopyTo(entryStream);
                                }
                            }
                        }
                    }
                }
            }
        }

        Console.WriteLine($"All attachments have been extracted to: {outputZipPath}");
    }
}
