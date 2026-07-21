using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // Path to the PDF form
        const string fieldName = "RadioGroup1";      // Fully qualified name of the radio button group

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Open the PDF using the Form facade (wrapped in using for deterministic disposal)
        using (Form form = new Form(pdfPath))
        {
            // Retrieve the currently selected option value for the specified radio button group
            string currentValue = form.GetButtonOptionCurrentValue(fieldName);

            Console.WriteLine($"Current selected value for '{fieldName}': {currentValue}");
        }
    }
}