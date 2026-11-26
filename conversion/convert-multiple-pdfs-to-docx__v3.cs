using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // List of PDF files to convert
        string[] pdfFiles = { "input1.pdf", "input2.pdf", "input3.pdf" };

        foreach (string pdfPath in pdfFiles)
        {
            // Verify the source PDF exists
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Determine the DOCX output path (same name, .docx extension)
            string docxPath = Path.ChangeExtension(pdfPath, ".docx");

            // Load the PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Configure DocSaveOptions for DOCX conversion
                DocSaveOptions docOptions = new DocSaveOptions
                {
                    // Output format: DOCX
                    Format = DocSaveOptions.DocFormat.DocX,
                    // Use flow recognition mode
                    Mode = DocSaveOptions.RecognitionMode.Flow,
                    // Enable bullet list detection
                    RecognizeBullets = true
                };

                // Save the document as DOCX using the specified options
                pdfDoc.Save(docxPath, docOptions);
            }

            Console.WriteLine($"Converted '{pdfPath}' to '{docxPath}'.");
        }
    }
}