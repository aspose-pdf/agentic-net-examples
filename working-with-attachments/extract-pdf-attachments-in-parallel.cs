using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF files – adjust the paths as needed
        List<string> pdfFiles = new List<string>
        {
            "doc1.pdf",
            "doc2.pdf",
            "doc3.pdf"
        };

        // Directory where extracted attachments will be saved
        string outputDirectory = "ExtractedAttachments";
        Directory.CreateDirectory(outputDirectory);

        // Process each PDF in parallel
        Parallel.ForEach(pdfFiles, pdfPath =>
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                return;
            }

            try
            {
                // Load the PDF document (lifecycle rule: use using for disposal)
                using (Document doc = new Document(pdfPath))
                {
                    // Iterate over embedded files (attachments) if any using reflection
                    foreach (var attachment in doc.EmbeddedFiles)
                    {
                        // Get the attachment name via reflection
                        var nameProp = attachment.GetType().GetProperty("Name");
                        string attachmentName = nameProp?.GetValue(attachment) as string ?? "unknown";

                        // Build a unique file name for the extracted attachment
                        string attachmentFileName = $"{Path.GetFileNameWithoutExtension(pdfPath)}_{attachmentName}";
                        string attachmentPath = Path.Combine(outputDirectory, attachmentFileName);

                        // Try to invoke the Save(string) method via reflection
                        var saveMethod = attachment.GetType().GetMethod("Save", new[] { typeof(string) });
                        if (saveMethod != null)
                        {
                            saveMethod.Invoke(attachment, new object[] { attachmentPath });
                            Console.WriteLine($"Extracted: {attachmentPath}");
                        }
                        else
                        {
                            // Fallback: attempt to read the file specification stream directly
                            var fileSpecProp = attachment.GetType().GetProperty("FileSpecification");
                            var fileSpec = fileSpecProp?.GetValue(attachment);
                            var contentsProp = fileSpec?.GetType().GetProperty("Contents");
                            var contents = contentsProp?.GetValue(fileSpec) as Stream;
                            if (contents != null)
                            {
                                using (var outStream = File.Create(attachmentPath))
                                {
                                    contents.CopyTo(outStream);
                                }
                                Console.WriteLine($"Extracted (stream fallback): {attachmentPath}");
                            }
                            else
                            {
                                Console.Error.WriteLine($"Unable to extract attachment '{attachmentName}' from '{pdfPath}'.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        });

        Console.WriteLine("Attachment extraction completed.");
    }
}
