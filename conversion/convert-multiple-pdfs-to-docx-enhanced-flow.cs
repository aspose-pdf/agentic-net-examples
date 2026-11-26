using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // List of PDF files to convert
        string[] pdfFiles = { "input1.pdf", "input2.pdf", "input3.pdf" };
        // Directory where DOCX files will be saved
        string outputDir = "DocxOutputs";

        Directory.CreateDirectory(outputDir);

        // Configure DocSaveOptions for DOCX output with EnhancedFlow recognition mode
        DocSaveOptions saveOptions = new DocSaveOptions
        {
            Format = DocSaveOptions.DocFormat.DocX,
            Mode   = DocSaveOptions.RecognitionMode.EnhancedFlow
        };

        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Build output DOCX file path
            string docxPath = Path.Combine(outputDir, Path.GetFileNameWithoutExtension(pdfPath) + ".docx");

            // Load the PDF and save it as DOCX using the specified options
            using (Document pdfDoc = new Document(pdfPath))
            {
                pdfDoc.Save(docxPath, saveOptions);
            }

            Console.WriteLine($"Converted '{pdfPath}' to '{docxPath}'.");
        }
    }
}