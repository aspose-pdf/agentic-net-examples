using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input directory containing PDF files
        const string inputDirectory = @"C:\InputPdfs";
        // Output directory where extracted attachments will be saved
        const string outputDirectory = @"C:\ExtractedAttachments";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDirectory}");
            return;
        }

        Directory.CreateDirectory(outputDirectory);

        // Get all PDF files in the input directory (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found to process.");
            return;
        }

        // Process each PDF in parallel
        Parallel.ForEach(pdfFiles, pdfPath =>
        {
            try
            {
                string pdfName = Path.GetFileNameWithoutExtension(pdfPath);
                // Create a subfolder for each PDF's attachments
                string pdfOutputFolder = Path.Combine(outputDirectory, pdfName);
                Directory.CreateDirectory(pdfOutputFolder);

                // Load the PDF document inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Export all embedded attachments using reflection (avoids direct dependency on EmbeddedFile type)
                    if (doc.EmbeddedFiles != null && doc.EmbeddedFiles.Count > 0)
                    {
                        foreach (var attachment in doc.EmbeddedFiles)
                        {
                            // Retrieve the attachment name via reflection
                            var nameProp = attachment.GetType().GetProperty("Name");
                            string safeName = nameProp != null && nameProp.GetValue(attachment) is string n && !string.IsNullOrEmpty(n)
                                ? n
                                : Guid.NewGuid().ToString();

                            string destPath = Path.Combine(pdfOutputFolder, safeName);

                            // Invoke the Save(string) method via reflection
                            var saveMethod = attachment.GetType().GetMethod("Save", new[] { typeof(string) });
                            if (saveMethod != null)
                            {
                                saveMethod.Invoke(attachment, new object[] { destPath });
                            }
                            else
                            {
                                // Fallback: try to copy the raw stream if Save method is unavailable
                                var fileSpecProp = attachment.GetType().GetProperty("FileSpecification");
                                var fileSpec = fileSpecProp?.GetValue(attachment);
                                var contentsProp = fileSpec?.GetType().GetProperty("Contents");
                                var contents = contentsProp?.GetValue(fileSpec) as Stream;
                                if (contents != null)
                                {
                                    using (var outStream = File.Create(destPath))
                                    {
                                        contents.CopyTo(outStream);
                                    }
                                }
                            }
                        }
                        Console.WriteLine($"Attachments extracted from '{pdfName}.pdf' to '{pdfOutputFolder}'.");
                    }
                    else
                    {
                        Console.WriteLine($"No attachments found in '{pdfName}.pdf'.");
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
