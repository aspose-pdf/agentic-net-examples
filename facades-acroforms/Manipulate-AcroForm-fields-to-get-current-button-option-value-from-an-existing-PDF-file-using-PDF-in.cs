using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // existing PDF with AcroForm
        const string outputPath = "output.pdf";  // path to save (unchanged) PDF
        const string buttonFieldName = "RadioGroup1"; // fully qualified name of the button field

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF using the Form facade (load rule)
        using (Form form = new Form(inputPath))
        {
            // Retrieve the current selected option value of the radio button group
            string currentValue = form.GetButtonOptionCurrentValue(buttonFieldName);
            Console.WriteLine($"Current value of button '{buttonFieldName}': {currentValue}");

            // Demonstrate saving the document (save rule) – no changes are made
            form.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}