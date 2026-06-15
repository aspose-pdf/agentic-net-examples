using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // Path to the PDF form
        const string fieldName = "RadioGroup1";      // Replace with the actual radio button field name

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Initialize the Form facade with the PDF file
        using (Form form = new Form(pdfPath))
        {
            // Get the currently selected value of the radio button group
            string currentValue = form.GetButtonOptionCurrentValue(fieldName);
            Console.WriteLine($"Current value of '{fieldName}': {currentValue}");
        }
    }
}