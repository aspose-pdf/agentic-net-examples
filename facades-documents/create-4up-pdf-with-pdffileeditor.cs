using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Example: read a PDF from a file into a stream (replace with any input stream)
        const string inputFilePath = "input.pdf";
        if (!File.Exists(inputFilePath))
        {
            Console.Error.WriteLine($"Input file not found: {inputFilePath}");
            return;
        }

        // Load the source PDF from a stream into a Document
        using (FileStream inputStream = File.OpenRead(inputFilePath))
        using (Aspose.Pdf.Document sourceDoc = new Aspose.Pdf.Document(inputStream))
        {
            // Save the Document to a temporary file – PdfFileEditor works with file paths
            string tempSourcePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");
            sourceDoc.Save(tempSourcePath);

            // Prepare an array with four copies of the same source file to achieve a 4‑up layout
            string[] sourceFiles = new string[] { tempSourcePath, tempSourcePath, tempSourcePath, tempSourcePath };

            // Define a temporary output file
            string tempOutputPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");

            // Use PdfFileEditor.MakeNUp to create the 4‑up PDF
            Aspose.Pdf.Facades.PdfFileEditor editor = new Aspose.Pdf.Facades.PdfFileEditor();
            bool success = editor.MakeNUp(sourceFiles, tempOutputPath, false); // false = portrait orientation

            if (!success)
            {
                Console.Error.WriteLine("MakeNUp operation failed.");
                // Clean up temporary files before exiting
                File.Delete(tempSourcePath);
                return;
            }

            // Read the resulting PDF into a MemoryStream
            using (MemoryStream outputMemory = new MemoryStream())
            using (FileStream resultFile = File.OpenRead(tempOutputPath))
            {
                resultFile.CopyTo(outputMemory);
                outputMemory.Position = 0; // reset for downstream consumers

                // At this point, outputMemory contains the 4‑up PDF.
                // Example: write it to a file to verify the result (optional)
                const string verificationPath = "output_4up.pdf";
                File.WriteAllBytes(verificationPath, outputMemory.ToArray());
                Console.WriteLine($"4‑up PDF written to '{verificationPath}'.");
            }

            // Clean up temporary files
            File.Delete(tempSourcePath);
            File.Delete(tempOutputPath);
        }
    }
}