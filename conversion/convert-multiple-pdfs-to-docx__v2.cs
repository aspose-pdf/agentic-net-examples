using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // List of PDF files to convert
        string[] inputFiles = { "file1.pdf", "file2.pdf", "file3.pdf" };

        foreach (string inputPath in inputFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"Input file not found: {inputPath}");
                continue;
            }

            // Output DOCX file name (same base name, .docx extension)
            string outputPath = Path.ChangeExtension(inputPath, ".docx");

            try
            {
                // Load the PDF document inside a using block for deterministic disposal
                using (Document pdfDoc = new Document(inputPath))
                {
                    // Configure DOCX save options as required
                    DocSaveOptions saveOpts = new DocSaveOptions
                    {
                        Format = DocSaveOptions.DocFormat.DocX,               // Save as DOCX
                        Mode = DocSaveOptions.RecognitionMode.Textbox,       // Use textbox recognition mode
                        RecognizeBullets = true                              // Enable bullet recognition
                    };

                    // Save the PDF as DOCX using the specified options
                    pdfDoc.Save(outputPath, saveOpts);
                }

                Console.WriteLine($"Converted '{inputPath}' to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error converting '{inputPath}': {ex.Message}");
            }
        }
    }
}