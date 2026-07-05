using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing the PDFs to process
        const string inputFolder = "InputPdfs";
        // Folder where the updated PDFs will be saved
        const string outputFolder = "OutputPdfs";
        // New URL to assign to all submit buttons
        const string newSubmitUrl = "https://new.example.com/submit";

        // Ensure the output folder exists
        Directory.CreateDirectory(outputFolder);

        // Verify that the input folder exists; if not, inform the user and exit gracefully
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder '{inputFolder}' does not exist. No PDFs to process.");
            return;
        }

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName + "_updated.pdf");

            // Retrieve the names of all submit buttons in the current PDF
            Form form = new Form(inputPath);
            string[] submitButtonNames = form.FormSubmitButtonNames;

            // Use FormEditor to modify the submit button URLs
            using (FormEditor editor = new FormEditor())
            {
                editor.BindPdf(inputPath);

                foreach (string buttonName in submitButtonNames)
                {
                    // Set the new URL for each submit button
                    editor.SetSubmitUrl(buttonName, newSubmitUrl);
                }

                // Save the modified PDF to the output location
                editor.Save(outputPath);
            }
        }

        Console.WriteLine("All PDFs have been updated with the new submit URL.");
    }
}
