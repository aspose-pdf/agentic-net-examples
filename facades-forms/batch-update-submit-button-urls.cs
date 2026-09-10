using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing the source PDFs
        const string inputFolder = @"C:\PdfBatch\Input";
        // Folder where the updated PDFs will be saved
        const string outputFolder = @"C:\PdfBatch\Output";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // New URL to assign to all submit buttons
        const string newUrl = "https://new.example.com/submit";

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            // Load the form to obtain the submit button names
            using (Form form = new Form(inputPath))
            {
                string[] submitButtonNames = form.FormSubmitButtonNames;

                // If the document contains submit buttons, update their URLs
                if (submitButtonNames != null && submitButtonNames.Length > 0)
                {
                    using (FormEditor editor = new FormEditor())
                    {
                        // Bind the source PDF
                        editor.BindPdf(inputPath);

                        // Update each submit button URL
                        foreach (string btnName in submitButtonNames)
                        {
                            editor.SetSubmitUrl(btnName, newUrl);
                        }

                        // Save the modified PDF to the output location
                        editor.Save(outputPath);
                    }
                }
                else
                {
                    // No submit buttons found – copy the original file unchanged
                    File.Copy(inputPath, outputPath, overwrite: true);
                }
            }

            Console.WriteLine($"Processed: {fileName}");
        }

        Console.WriteLine("Batch update completed.");
    }
}