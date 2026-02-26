using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade classes for form manipulation

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // source PDF containing the form
        const string buttonFieldName = "RadioGroup1"; // fully‑qualified name of the button (radio) field

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Wrap the Form facade in a using block for deterministic disposal (lifecycle rule)
        using (Form form = new Form(inputPdf))
        {
            // Get the currently selected option value of the radio button group
            string currentValue = form.GetButtonOptionCurrentValue(buttonFieldName);

            Console.WriteLine($"Current value of button field \"{buttonFieldName}\": {currentValue}");
        }
    }
}