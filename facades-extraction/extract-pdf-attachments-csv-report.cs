using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string attachmentsDir = "Attachments";
        const string csvReportPath = "attachments_report.csv";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        Directory.CreateDirectory(attachmentsDir);

        // Load the PDF document
        Document doc = new Document(pdfPath);

        using (StreamWriter csvWriter = new StreamWriter(csvReportPath, false, Encoding.UTF8))
        {
            // CSV header
            csvWriter.WriteLine("AttachmentName,SizeBytes");

            // Iterate over embedded files (attachments)
            foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
            {
                // Attachment name
                string name = fileSpec.Name;

                // Determine size – prefer Params.Size, otherwise compute from the stream
                long size = 0;
                if (fileSpec.Params != null && fileSpec.Params.Size > 0)
                {
                    size = fileSpec.Params.Size;
                }
                else if (fileSpec.Contents != null)
                {
                    if (fileSpec.Contents.CanSeek)
                    {
                        size = fileSpec.Contents.Length;
                    }
                    else
                    {
                        // Non‑seekable stream – copy to a MemoryStream to get length
                        using (MemoryStream ms = new MemoryStream())
                        {
                            fileSpec.Contents.CopyTo(ms);
                            size = ms.Length;
                            ms.Position = 0;
                            // Reset the original stream position for later copy (if possible)
                            if (fileSpec.Contents.CanSeek)
                                fileSpec.Contents.Position = 0;
                        }
                    }
                }

                // Save attachment to the output folder using the Contents stream
                string destPath = Path.Combine(attachmentsDir, name);
                using (FileStream outStream = new FileStream(destPath, FileMode.Create, FileAccess.Write))
                {
                    if (fileSpec.Contents != null)
                    {
                        if (fileSpec.Contents.CanSeek)
                            fileSpec.Contents.Position = 0;
                        fileSpec.Contents.CopyTo(outStream);
                    }
                }

                // Escape commas in the name for CSV compliance
                string escapedName = name.Contains(",") ? $"\"{name}\"" : name;
                csvWriter.WriteLine($"{escapedName},{size}");
            }
        }

        Console.WriteLine($"Attachment extraction completed. CSV report saved to '{csvReportPath}'.");
    }
}
