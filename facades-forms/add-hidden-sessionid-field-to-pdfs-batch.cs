using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Folder containing PDFs to process
        const string inputFolder  = @"C:\PdfBatch\Input";
        const string outputFolder = @"C:\PdfBatch\Output";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Generate a new GUID for this document
            string sessionId = Guid.NewGuid().ToString();

            // Determine output file path (same name, different folder)
            string outputPath = Path.Combine(outputFolder, Path.GetFileName(inputPath));

            // Load the PDF document
            Document doc = new Document(inputPath);

            // Create a hidden text field named "SessionId" and assign the GUID value
            // Use a zero‑size rectangle because the field is hidden; the Flags property marks it as hidden.
            TextBoxField hiddenField = new TextBoxField(
                doc.Pages[1],
                new Rectangle(0, 0, 0, 0) // zero‑size rectangle – not rendered
            )
            {
                PartialName = "SessionId",
                Value = sessionId
            };
            hiddenField.Flags = AnnotationFlags.Hidden; // make the field invisible in viewers

            // Add the hidden field to the first page (page numbers are 1‑based)
            doc.Form.Add(hiddenField, 1);

            // Save the modified PDF
            doc.Save(outputPath);

            Console.WriteLine($"Processed '{Path.GetFileName(inputPath)}' – SessionId: {sessionId}");
        }

        Console.WriteLine("Batch processing completed.");
    }
}
