using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing the PDFs to process
        const string inputFolder = @"C:\PdfBatch\Input";
        // Folder where the updated PDFs will be saved
        const string outputFolder = @"C:\PdfBatch\Output";

        // Verify that the input folder exists before proceeding
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // URL to set for all submit buttons
        const string newUrl = "https://new.example.com/submit";

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Determine output file path (same file name in output folder)
            string outputPath = Path.Combine(outputFolder, Path.GetFileName(inputPath));

            try
            {
                // Load the PDF document to retrieve submit button names
                Form form = new Form(inputPath);
                string[] submitButtonNames = form.FormSubmitButtonNames;

                // If there are no submit buttons, just copy the file
                if (submitButtonNames == null || submitButtonNames.Length == 0)
                {
                    // Simple copy when no buttons to update
                    File.Copy(inputPath, outputPath, true);
                    continue;
                }

                // Use FormEditor to modify the submit button URLs
                using (FormEditor editor = new FormEditor())
                {
                    // Bind the PDF file to the editor
                    editor.BindPdf(inputPath);

                    // Update each submit button's URL
                    foreach (string btnName in submitButtonNames)
                    {
                        editor.SetSubmitUrl(btnName, newUrl);
                    }

                    // Save the modified PDF to the output location
                    editor.Save(outputPath);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch update completed.");
    }
}
