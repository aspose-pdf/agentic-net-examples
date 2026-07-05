using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // Path to the PDF form
        const string radioFieldName = "RadioGroup1"; // Fully qualified name of the radio button group

        // Verify that the PDF file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Initialize the Form facade (loads the PDF)
        using (Form form = new Form(pdfPath))
        {
            // Retrieve the currently selected value of the radio button group
            string selectedValue = form.GetButtonOptionCurrentValue(radioFieldName);

            // Output the result
            Console.WriteLine($"Current selected value for '{radioFieldName}': {selectedValue}");
        }
    }
}