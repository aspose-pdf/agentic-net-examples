using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input_form.pdf";
        const string outputPdf = "filled_form.pdf";
        const string configFile = "form_defaults.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPdf);
            return;
        }

        if (!File.Exists(configFile))
        {
            Console.Error.WriteLine("Configuration file not found: " + configFile);
            return;
        }

        // Load the PDF form and specify the output file name
        using (Form pdfForm = new Form(inputPdf, outputPdf))
        {
            // Each line in the config file should be in the format: fieldName=fieldValue
            string[] lines = File.ReadAllLines(configFile);
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                {
                    continue; // Skip empty lines and comments
                }

                int separatorIndex = line.IndexOf('=');
                if (separatorIndex <= 0)
                {
                    continue; // Invalid line format
                }

                string fieldName = line.Substring(0, separatorIndex).Trim();
                string fieldValue = line.Substring(separatorIndex + 1).Trim();

                // Fill the field with the provided value
                bool filled = pdfForm.FillField(fieldName, fieldValue);
                if (!filled)
                {
                    Console.WriteLine("Field not found or not filled: " + fieldName);
                }
            }

            // Save the updated PDF form
            pdfForm.Save();
        }

        Console.WriteLine("Form fields filled and saved to " + outputPdf);
    }
}
