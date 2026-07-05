using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class BatchAddHiddenSessionId
{
    static void Main()
    {
        // Folder containing the source PDFs
        const string inputFolder  = @"C:\PdfBatch\Input";
        // Folder where the processed PDFs will be saved
        const string outputFolder = @"C:\PdfBatch\Output";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found in the input folder.");
            return;
        }

        foreach (string inputPath in pdfFiles)
        {
            // Generate a new GUID for this document
            string sessionId = Guid.NewGuid().ToString();

            // Load the PDF inside a using block (document disposal rule)
            using (Document doc = new Document(inputPath))
            {
                // Create a hidden text field on the first page.
                // The rectangle is zero‑size because the field is hidden.
                TextBoxField hiddenField = new TextBoxField(
                    doc.Pages[1],
                    new Aspose.Pdf.Rectangle(0, 0, 0, 0))
                {
                    PartialName = "SessionId",
                    Value       = sessionId
                    // No need to set Flags; a zero‑size rectangle makes the field invisible.
                };

                // Add the field to the document's AcroForm.
                doc.Form.Add(hiddenField);

                // Build the output file path (same file name, different folder)
                string outputPath = Path.Combine(outputFolder, Path.GetFileName(inputPath));

                // Save the modified PDF (save rule)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Processed '{Path.GetFileName(inputPath)}' – SessionId: {sessionId}");
        }

        Console.WriteLine("Batch processing completed.");
    }
}
