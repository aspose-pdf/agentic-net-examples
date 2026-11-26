using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths of the three PDF files to convert
        string[] pdfFiles = { "input1.pdf", "input2.pdf", "input3.pdf" };

        foreach (string pdfPath in pdfFiles)
        {
            // Verify the source PDF exists
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Determine the output DOCX file name (same base name, .docx extension)
            string docxPath = Path.ChangeExtension(pdfPath, ".docx");

            // Load the PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Configure DocSaveOptions for DOCX output with Flow recognition mode
                var saveOptions = new DocSaveOptions();
                // The correct property for setting the recognition mode is 'Mode'
                saveOptions.Mode = DocSaveOptions.RecognitionMode.Flow;

                // Save the PDF as DOCX using the specified options
                pdfDoc.Save(docxPath, saveOptions);
            }

            Console.WriteLine($"Converted '{pdfPath}' to '{docxPath}'.");
        }
    }
}
