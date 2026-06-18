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
            // Load the PDF form
            using (Form form = new Form(inputPdf))
            {
                // Set the checkbox field to true (checked)
                bool success = form.FillField(fieldName, true);
                if (!success)
                {
                    Console.Error.WriteLine($"Field '{fieldName}' not found or could not be filled.");
                }

                // Save the updated PDF
                form.Save(outputPdf);
            }

            Console.WriteLine($"Checkbox '{fieldName}' set to true and saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}