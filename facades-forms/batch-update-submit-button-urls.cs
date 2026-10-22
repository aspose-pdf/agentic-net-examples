using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing PDFs to process
        const string inputFolder = "InputPdfs";
        // Folder where updated PDFs will be saved
        const string outputFolder = "OutputPdfs";
        // New URL for all submit buttons
        const string newSubmitUrl = "https://new.example.com/submit";

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

            // First, read the form field names using the Form facade
            string[] fieldNames;
            using (Form form = new Form())
            {
                form.BindPdf(pdfPath);
                fieldNames = form.FieldNames; // array of all field names
            }

            // Now edit the PDF to update submit button actions
            using (FormEditor editor = new FormEditor())
            {
                editor.BindPdf(pdfPath);

                // Re‑bind a Form to query each field's type. The older Aspose.Pdf versions do not expose the FormFieldType enum,
                // so we simply attempt to set the submit URL for every field and ignore any exceptions that indicate the field
                // is not a submit button.
                using (Form form = new Form())
                {
                    form.BindPdf(pdfPath);
                    foreach (string fieldName in fieldNames)
                    {
                        try
                        {
                            // Attempt to set the submit URL. If the field is not a submit button, Aspose throws an exception
                            // which we swallow – the field is left unchanged.
                            editor.SetSubmitUrl(fieldName, newSubmitUrl);
                        }
                        catch (Exception)
                        {
                            // Ignore fields that are not submit buttons or any other errors.
                        }
                    }
                }

                // Save the modified PDF
                editor.Save(outputPath);
            }

            Console.WriteLine($"Processed: {fileName}");
        }

        Console.WriteLine("All PDFs have been updated.");
    }
}
