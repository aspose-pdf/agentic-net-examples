using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Open the PDF and bind it to the Form facade.
        // The constructor (input, output) creates a Form object that works on the source file
        // and will save the result to the specified output file.
        Form form = new Form(inputPdf, outputPdf);

        // Retrieve the current value of the source field.
        // GetField returns an object; for a text field it will be a string.
        object sourceValueObj = form.GetField("SourceNotes");
        string sourceValue = sourceValueObj?.ToString() ?? string.Empty;

        // Set the same value into the target field.
        // FillField returns true if the field exists and was filled successfully.
        bool filled = form.FillField("TargetNotes", sourceValue);
        if (!filled)
        {
            Console.Error.WriteLine("Target field 'TargetNotes' not found or could not be filled.");
        }

        // Save the modified PDF.
        form.Save();

        Console.WriteLine($"Field content copied and saved to '{outputPdf}'.");
    }
}