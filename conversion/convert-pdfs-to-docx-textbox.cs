using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // List of PDF files to convert
        string[] inputFiles = { "file1.pdf", "file2.pdf", "file3.pdf" };
        // Directory where DOCX files will be saved
        string outputDir = "DocxOutputs";

        Directory.CreateDirectory(outputDir);

        foreach (string inputPath in inputFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"Input file not found: {inputPath}");
                continue;
            }

            // Build output DOCX path based on input file name
            string outputFileName = Path.GetFileNameWithoutExtension(inputPath) + ".docx";
            string outputPath = Path.Combine(outputDir, outputFileName);

            try
            {
                // Load PDF inside a using block for deterministic disposal
                using (Document pdfDoc = new Document(inputPath))
                {
                    // Configure DocSaveOptions to produce DOCX using textbox recognition mode
                    var saveOptions = new DocSaveOptions();
                    // Set the desired output format (DOCX)
                    saveOptions.Format = DocSaveOptions.DocFormat.DocX;
                    // Set the recognition mode – Textbox as requested
                    saveOptions.Mode = DocSaveOptions.RecognitionMode.Textbox;

                    // Save the PDF as DOCX with the specified options
                    pdfDoc.Save(outputPath, saveOptions);
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
