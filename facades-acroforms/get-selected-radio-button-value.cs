using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // Path to the PDF containing the form
        const string fieldName = "RadioGroup1";      // Full name of the radio button group

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Open the PDF form using the Facades Form class (IDisposable, so wrap in using)
        using (Form form = new Form(pdfPath))
        {
            // Get the currently selected value of the specified radio button group
            string selectedValue = form.GetButtonOptionCurrentValue(fieldName);

            Console.WriteLine($"Current value of radio button '{fieldName}': {selectedValue}");
        }
    }
}