using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Included as per requirement

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        string outputDir = Path.Combine("Output", "Attachments");

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF document (Document implements IDisposable)
            using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(inputPath))
            {
                // Check for embedded files (attachments)
                if (pdfDoc.EmbeddedFiles != null && pdfDoc.EmbeddedFiles.Count > 0)
                {
                    foreach (Aspose.Pdf.FileSpecification fileSpec in pdfDoc.EmbeddedFiles)
                    {
                        // Get the attachment name; fallback if missing
                        string fileName = string.IsNullOrEmpty(fileSpec.Name) ? "attachment.bin" : fileSpec.Name;
                        string outPath = Path.Combine(outputDir, fileName);

                        // Save the attachment content to disk
                        using (System.IO.Stream contentStream = fileSpec.Contents)
                        using (System.IO.FileStream fs = new System.IO.FileStream(outPath, System.IO.FileMode.Create, System.IO.FileAccess.Write))
                        {
                            contentStream.CopyTo(fs);
                        }

                        Console.WriteLine($"Saved attachment: {outPath}");
                    }
                }
                else
                {
                    Console.WriteLine("No attachments found in the PDF.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}