using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Folder containing input PDFs
        const string inputFolder = @"C:\InputPdfs";
        // Folder where processed PDFs will be saved
        const string outputFolder = @"C:\OutputPdfs";

        // Verify that the input folder exists before trying to enumerate files.
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder '{inputFolder}' does not exist. No PDFs will be processed.");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Load the PDF document (using ensures proper disposal)
                using (Document doc = new Document(pdfPath))
                {
                    // Retrieve the 'Comments' field and set its maximum length to 100 characters.
                    // Directly cast to TextBoxField because we know the field type.
                    if (doc.Form["Comments"] is TextBoxField commentsField)
                    {
                        commentsField.MaxLen = 100;
                    }

                    // Save the modified document to the output folder
                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(pdfPath));
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}