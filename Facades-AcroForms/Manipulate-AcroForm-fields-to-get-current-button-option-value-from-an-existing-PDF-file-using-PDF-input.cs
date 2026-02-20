using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Path to the PDF file containing the AcroForm
        const string pdfPath = "input.pdf";

        // Fully qualified name of the radio button group (or button option field)
        const string fieldName = "RadioGroup1";

        // Verify that the PDF file exists before proceeding
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        try
        {
            // Create a Form facade and bind it to the existing PDF
            using (Form form = new Form())
            {
                form.BindPdf(pdfPath);

                // Retrieve the current selected value of the button option field
                string currentValue = form.GetButtonOptionCurrentValue(fieldName);

                Console.WriteLine($"Current value of button option field '{fieldName}': {currentValue}");
            }
        }
        catch (Exception ex)
        {
            // Output any unexpected errors
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}