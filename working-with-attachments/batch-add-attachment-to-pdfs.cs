using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing the PDFs to process
        const string inputFolder = "InputPdfs";
        // Folder where the processed PDFs will be saved
        const string outputFolder = "OutputPdfs";
        // Path to the file that will be attached to each PDF
        const string attachmentPath = "attachment.pdf";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Read the attachment once – reuse the byte array for every PDF
        byte[] attachmentBytes = File.ReadAllBytes(attachmentPath);
        string attachmentFileName = Path.GetFileName(attachmentPath);

        // Process each PDF file in the input folder
        foreach (string pdfFilePath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(pdfFilePath);
            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Open the PDF inside a using block for deterministic disposal
                using (Document doc = new Document(pdfFilePath))
                {
                    // Create a FileSpecification for the attachment and add it to the PDF
                    var attachmentSpec = new FileSpecification(attachmentFileName)
                    {
                        // Provide the file data via a MemoryStream
                        Contents = new MemoryStream(attachmentBytes)
                    };
                    doc.EmbeddedFiles.Add(attachmentSpec);

                    // Save the modified PDF to the output folder (overwrites if exists)
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {fileName}: {ex.Message}");
            }
        }
    }
}
