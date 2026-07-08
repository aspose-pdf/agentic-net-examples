using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputFolder = @"C:\PdfFolder";   // folder containing PDFs
        const string outputFolder = @"C:\PdfFolder\Processed";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Load the PDF document
                using (Document doc = new Document(pdfPath))
                {
                    // Retrieve the field as a generic Field and then cast to TextBoxField
                    Field genericField = doc.Form["Comments"] as Field;
                    if (genericField is TextBoxField textBox)
                    {
                        // Set maximum character length to 100
                        textBox.MaxLen = 100;
                    }
                    else
                    {
                        Console.WriteLine($"'Comments' field not found or not a text box in: {Path.GetFileName(pdfPath)}");
                    }

                    // Save the modified PDF to the output folder
                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(pdfPath));
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{Path.GetFileName(pdfPath)}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}
