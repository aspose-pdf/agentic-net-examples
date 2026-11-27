using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_attachments.pdf";

        // Files to be attached – replace with your actual paths or streams
        const string attachPath1 = "attachment1.txt";
        const string attachPath2 = "attachment2.jpg";
        const string attachPath3 = "attachment3.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(attachPath1) || !File.Exists(attachPath2) || !File.Exists(attachPath3))
        {
            Console.Error.WriteLine("One or more attachment files not found.");
            return;
        }

        try
        {
            // Load the PDF with the high‑level Document API
            Document pdf = new Document(inputPdf);

            // Helper local function to add a single attachment
            void AddAttachment(string filePath)
            {
                var fileSpec = new FileSpecification(Path.GetFileName(filePath))
                {
                    // The file content is supplied via a stream (MemoryStream in this case)
                    Contents = new MemoryStream(File.ReadAllBytes(filePath))
                };
                pdf.EmbeddedFiles.Add(fileSpec);
            }

            // Add the three attachments
            AddAttachment(attachPath1);
            AddAttachment(attachPath2);
            AddAttachment(attachPath3);

            // Save the modified PDF
            pdf.Save(outputPdf);

            Console.WriteLine($"PDF saved with attachments to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
