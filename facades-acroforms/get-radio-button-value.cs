using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "PdfForm.pdf";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        const string fieldName = "btnField";
        using (Form pdfForm = new Form(inputPath))
        {
            string currentValue = pdfForm.GetButtonOptionCurrentValue(fieldName);
            Console.WriteLine($"Current selected value for '{fieldName}': {currentValue}");
        }
    }
}