using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    // New URL to set for all submit buttons
    private const string NewSubmitUrl = "https://new.example.com/submit";

    static void Main()
    {
        // Folder containing PDFs to process
        const string inputFolder = @"C:\PdfBatch\Input";
        // Optional: folder for updated PDFs (can overwrite original files)
        const string outputFolder = @"C:\PdfBatch\Output";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(pdfPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Load the PDF with FormEditor (facade for editing form fields)
                using (FormEditor formEditor = new FormEditor())
                {
                    formEditor.BindPdf(pdfPath);

                    // Use the Form facade to retrieve all submit button names
                    using (Form form = new Form(pdfPath))
                    {
                        foreach (string btnName in form.FormSubmitButtonNames)
                        {
                            // Update the URL of each submit button
                            bool updated = formEditor.SetSubmitUrl(btnName, NewSubmitUrl);
                            if (!updated)
                            {
                                Console.WriteLine($"Warning: Could not update URL for button '{btnName}' in '{fileName}'.");
                            }
                        }
                    }

                    // Save the modified PDF (overwrites the original or writes to a new file)
                    formEditor.Save(outputPath);
                }

                Console.WriteLine($"Processed: {fileName} → {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{fileName}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch update completed.");
    }
}