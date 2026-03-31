using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputDirectory = "input";
        const string newUrl = "https://new.example.com/submit";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine("Input directory not found: " + inputDirectory);
            return;
        }

        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf");
        foreach (string pdfPath in pdfFiles)
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
            string outputPath = Path.Combine(inputDirectory, fileNameWithoutExt + "_updated.pdf");

            // Retrieve submit button names from the form
            using (Form form = new Form(pdfPath))
            {
                string[] submitButtonNames = form.FormSubmitButtonNames;

                using (FormEditor editor = new FormEditor(pdfPath, outputPath))
                {
                    foreach (string buttonName in submitButtonNames)
                    {
                        editor.SetSubmitUrl(buttonName, newUrl);
                    }
                    // Write changes to the output file
                    editor.Save(outputPath);
                }
            }

            Console.WriteLine("Processed: " + Path.GetFileName(pdfPath));
        }
    }
}