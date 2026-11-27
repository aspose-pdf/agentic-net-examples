using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string attachFile1 = "attachment1.txt";
        const string attachFile2 = "attachment2.jpg";

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(attachFile1) || !File.Exists(attachFile2))
        {
            Console.Error.WriteLine("One or both attachment files are missing.");
            return;
        }

        try
        {
            // -----------------------------------------------------------------
            // 1. Delete any existing attachments using PdfContentEditor.DeleteAttachments
            // -----------------------------------------------------------------
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(inputPdf);
                editor.DeleteAttachments();
                // Save the intermediate result so that the PDF on disk no longer has
                // the old attachments. This step is required because the high‑level
                // Document API works on the file content, not on the in‑memory editor.
                editor.Save(inputPdf);
            }

            // -----------------------------------------------------------------
            // 2. Add the new attachments using the high‑level Document API
            // -----------------------------------------------------------------
            Document pdf = new Document(inputPdf);

            // Helper to create a FileSpecification from a file on disk
            FileSpecification CreateFileSpec(string path, string description)
            {
                return new FileSpecification(Path.GetFileName(path))
                {
                    Description = description,
                    // The file content must be provided as a stream
                    Contents = new MemoryStream(File.ReadAllBytes(path))
                };
            }

            // Add the first attachment
            pdf.EmbeddedFiles.Add(CreateFileSpec(attachFile1, "First attachment"));
            // Add the second attachment
            pdf.EmbeddedFiles.Add(CreateFileSpec(attachFile2, "Second attachment"));

            // -----------------------------------------------------------------
            // 3. Save the final PDF with the new attachments
            // -----------------------------------------------------------------
            pdf.Save(outputPdf);

            Console.WriteLine($"PDF saved with new attachments: {outputPdf}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
