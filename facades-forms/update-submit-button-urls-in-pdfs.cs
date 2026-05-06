using System;
using System.IO;
using Aspose.Pdf.Facades; // Provides Form and FormEditor classes

class Program
{
    static void Main()
    {
        // Input folder containing PDFs to process
        const string inputDir = "InputPdfs";
        // Output folder for updated PDFs
        const string outputDir = "OutputPdfs";
        // New URL to assign to all submit buttons
        const string newUrl = "https://new.example.com/submit";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Verify that the input directory exists; if not, inform the user and exit gracefully
        if (!Directory.Exists(inputDir))
        {
            Console.WriteLine($"Input directory '{inputDir}' does not exist. Please create it and place PDF files to process.");
            return;
        }

        // Process each PDF file in the input directory
        string[] pdfFiles = Directory.GetFiles(inputDir, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{inputDir}'. Nothing to process.");
            return;
        }

        foreach (string inputPath in pdfFiles)
        {
            string fileName = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputDir, fileName);

            // Retrieve all submit button names from the form
            string[] submitButtons;
            using (Form form = new Form(inputPath))
            {
                submitButtons = form.FormSubmitButtonNames;
            }

            // Update the URL of each submit button
            using (FormEditor editor = new FormEditor())
            {
                editor.BindPdf(inputPath);
                foreach (string btnName in submitButtons)
                {
                    editor.SetSubmitUrl(btnName, newUrl);
                }
                // Save the modified PDF to the output path
                editor.Save(outputPath);
            }

            Console.WriteLine($"Processed: {fileName}");
        }
    }
}
