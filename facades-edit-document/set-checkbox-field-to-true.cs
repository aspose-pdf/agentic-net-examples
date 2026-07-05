using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string fieldName = "TermsAccepted";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Initialize the Form facade with input and output files
            using (Form form = new Form(inputPdf, outputPdf))
            {
                // Fill the checkbox field with true (checked)
                bool success = form.FillField(fieldName, true);
                if (!success)
                {
                    Console.Error.WriteLine($"Field '{fieldName}' not found.");
                }

                // Persist changes to the output PDF
                form.Save();
            }

            Console.WriteLine($"Checkbox '{fieldName}' set to true and saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}