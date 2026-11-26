using System;
using System.IO;
using Aspose.Pdf; // DocSaveOptions and Document are in this namespace

class Program
{
    static void Main()
    {
        // Input PDF files to be converted
        string[] pdfFiles = { "first.pdf", "second.pdf", "third.pdf" };

        // Ensure output directory exists
        string outputDir = "DocxOutputs";
        Directory.CreateDirectory(outputDir);

        // Configure DocSaveOptions to produce DOCX format
        DocSaveOptions docxOptions = new DocSaveOptions
        {
            Format = DocSaveOptions.DocFormat.DocX
        };

        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Build output DOCX file path
            string outputPath = Path.Combine(outputDir,
                Path.GetFileNameWithoutExtension(pdfPath) + ".docx");

            // Load PDF and save as DOCX using the prepared options
            using (Document pdfDoc = new Document(pdfPath))
            {
                pdfDoc.Save(outputPath, docxOptions);
            }

            Console.WriteLine($"Converted '{pdfPath}' to '{outputPath}'.");
        }
    }
}