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

        // Ensure the output folder exists
        Directory.CreateDirectory(outputFolder);

        // Verify the input folder exists; if not, inform the user and exit gracefully
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder '{inputFolder}' does not exist. No files to process.");
            return;
        }

        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        const string newUrl = "https://new.example.com/submit";

        foreach (string inputPath in pdfFiles)
        {
            string fileName = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            // Retrieve all submit button names in the current PDF
            Form form = new Form(inputPath);
            string[] submitButtons = form.FormSubmitButtonNames;

            if (submitButtons != null && submitButtons.Length > 0)
            {
                // Use the parameter‑less constructor (the overload with destination is obsolete)
                using (FormEditor editor = new FormEditor())
                {
                    // Bind the source PDF
                    editor.BindPdf(inputPath);

                    // Update each submit button URL
                    foreach (string btnName in submitButtons)
                    {
                        editor.SetSubmitUrl(btnName, newUrl);
                    }

                    // Save the modified PDF to the desired output location
                    editor.Save(outputPath);
                }
            }
            else
            {
                // If there are no submit buttons, simply copy the original file
                File.Copy(inputPath, outputPath, true);
            }
        }

        Console.WriteLine("All submit button URLs have been updated.");
    }
}
