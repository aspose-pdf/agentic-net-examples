using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input folder containing PDFs
        const string inputFolder = @"C:\PdfInput";
        // Output folder for processed PDFs
        const string outputFolder = @"C:\PdfOutput";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Load the PDF document (lifecycle rule: use using)
                using (Document doc = new Document(inputPath))
                {
                    // Retrieve the form field safely – the Form indexer returns a WidgetAnnotation,
                    // so we must cast it to Aspose.Pdf.Forms.Field before using field‑specific members.
                    Field? field = doc.Form["Comments"] as Field;
                    if (field is TextBoxField textBox)
                    {
                        // Set maximum length to 100 characters
                        textBox.MaxLen = 100;
                    }
                    else
                    {
                        Console.WriteLine($"'Comments' field not found or not a TextBoxField in {fileName}");
                    }

                    // Save the modified document (lifecycle rule: use Save)
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {fileName}: {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}
