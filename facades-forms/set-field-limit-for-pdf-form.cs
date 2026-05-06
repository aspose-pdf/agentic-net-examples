using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string fieldName = "PhoneNumber";
        const int    maxLength = 15;

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // FormEditor implements IDisposable – wrap it in a using block.
        using (FormEditor formEditor = new FormEditor())
        {
            // Load the source PDF.
            formEditor.BindPdf(inputPdf);

            // Set the maximum character count for the specified text field.
            bool result = formEditor.SetFieldLimit(fieldName, maxLength);
            if (!result)
            {
                Console.Error.WriteLine($"Failed to set field limit for '{fieldName}'.");
            }

            // Save the modified PDF.
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"Field '{fieldName}' limit set to {maxLength} characters. Saved to '{outputPdf}'.");
    }
}