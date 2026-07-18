using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = "InputPdfs";
        // Folder where processed PDFs will be saved
        const string outputFolder = "OutputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string sourcePath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(sourcePath);
            string targetPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Load the PDF document (lifecycle rule: use using)
                using (Document doc = new Document(sourcePath))
                {
                    // Retrieve the field named "Comments" (if it exists)
                    // The Form indexer returns a WidgetAnnotation, so we need an explicit cast to Field.
                    Field field = doc.Form["Comments"] as Field;
                    if (field != null)
                    {
                        // The typical "Comments" field is a text box; set its character limit
                        if (field is TextBoxField txtField)
                        {
                            txtField.MaxLen = 100; // 100‑character limit
                        }
                        // If the field type does not support MaxLen, it is ignored
                    }

                    // Save the modified PDF (lifecycle rule: use Save)
                    doc.Save(targetPath);
                }

                Console.WriteLine($"Processed: {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{fileName}': {ex.Message}");
            }
        }
    }
}
