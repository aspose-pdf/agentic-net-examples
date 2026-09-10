using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;

class BatchAttachmentExtractor
{
    static void Main()
    {
        // Directory containing the source PDF files
        const string inputDirectory = @"C:\PdfInputs";

        // Path for the consolidated ZIP archive that will hold all extracted attachments
        const string outputZipPath = @"C:\ExtractedAttachments\AllAttachments.zip";

        // Ensure the input directory exists
        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDirectory}");
            return;
        }

        // Create (or overwrite) the ZIP archive
        using (ZipArchive zip = ZipFile.Open(outputZipPath, ZipArchiveMode.Create))
        {
            // Enumerate all PDF files in the input directory (non‑recursive)
            foreach (string pdfPath in Directory.GetFiles(inputDirectory, "*.pdf"))
            {
                // Load each PDF inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // If the document has no embedded files, skip it
                    if (doc.EmbeddedFiles == null || doc.EmbeddedFiles.Count == 0)
                        continue;

                    // Iterate over each embedded file (attachment) in the current PDF using reflection
                    foreach (var embeddedObj in doc.EmbeddedFiles)
                    {
                        // Retrieve the attachment name via reflection
                        var nameProp = embeddedObj.GetType().GetProperty("Name");
                        string attachmentName = nameProp?.GetValue(embeddedObj) as string ?? "unknown";

                        // Build a ZIP entry name that includes the source PDF name for uniqueness
                        string pdfBaseName = Path.GetFileNameWithoutExtension(pdfPath);
                        string entryName = $"{pdfBaseName}/{attachmentName}";

                        // Create a new entry in the ZIP archive
                        ZipArchiveEntry zipEntry = zip.CreateEntry(entryName, CompressionLevel.Optimal);

                        // Write the attachment data directly into the ZIP entry stream
                        using (Stream entryStream = zipEntry.Open())
                        {
                            // Use reflection to call the Save(Stream) method of the embedded file object
                            var saveMethod = embeddedObj.GetType().GetMethod("Save", new[] { typeof(Stream) });
                            if (saveMethod != null)
                            {
                                saveMethod.Invoke(embeddedObj, new object[] { entryStream });
                            }
                            else
                            {
                                // Fallback: try to copy the raw contents if Save(Stream) is unavailable
                                var fileSpecProp = embeddedObj.GetType().GetProperty("FileSpecification");
                                var fileSpec = fileSpecProp?.GetValue(embeddedObj);
                                var contentsProp = fileSpec?.GetType().GetProperty("Contents");
                                var contentsStream = contentsProp?.GetValue(fileSpec) as Stream;
                                if (contentsStream != null)
                                {
                                    contentsStream.CopyTo(entryStream);
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
