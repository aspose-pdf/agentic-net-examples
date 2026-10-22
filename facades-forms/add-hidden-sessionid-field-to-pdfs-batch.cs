using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = @"C:\PdfBatch\Input";
        // Folder where processed PDFs will be saved
        const string outputFolder = @"C:\PdfBatch\Output";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Generate a new GUID for this document
            string sessionId = Guid.NewGuid().ToString();

            // Load the PDF with the core Document API
            using (Document doc = new Document(inputPath))
            {
                // Create a hidden text field named "SessionId"
                // The rectangle is set to zero size because the field is hidden.
                TextBoxField hiddenField = new TextBoxField(doc.Pages[1], new Rectangle(0, 0, 0, 0))
                {
                    Value = sessionId
                };
                hiddenField.PartialName = "SessionId";

                // Add the field to the document's form collection
                doc.Form.Add(hiddenField);

                // Define the final output path (same file name as the source)
                string outputPath = Path.Combine(outputFolder, Path.GetFileName(inputPath));

                // Save the PDF with the hidden field applied
                doc.Save(outputPath);
            }

            Console.WriteLine($"Processed '{Path.GetFileName(inputPath)}' – SessionId: {sessionId}");
        }

        Console.WriteLine("Batch processing completed.");
    }
}
