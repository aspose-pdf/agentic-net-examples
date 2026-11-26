using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Define the three source PDF files
        string[] pdfFiles = { "first.pdf", "second.pdf", "third.pdf" };

        foreach (string pdfPath in pdfFiles)
        {
            // Verify the source PDF exists
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"Source file not found: {pdfPath}");
                continue;
            }

            // Build the output DOCX path (same name, .docx extension)
            string docxPath = Path.ChangeExtension(pdfPath, ".docx");

            // Load the PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Configure DOCX save options: set format to DocX and enable bullet recognition
                DocSaveOptions saveOptions = new DocSaveOptions
                {
                    Format = DocSaveOptions.DocFormat.DocX,
                    RecognizeBullets = true
                };

                // Save the PDF as DOCX using the specified options
                pdfDoc.Save(docxPath, saveOptions);
            }

            Console.WriteLine($"Converted '{pdfPath}' to '{docxPath}'.");
        }
    }
}