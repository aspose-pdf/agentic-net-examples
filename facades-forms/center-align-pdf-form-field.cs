using System;
using System.IO;
using Aspose.Pdf.Facades; // FormEditor, FormFieldFacade

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // source PDF with the form
        const string outputPath = "output.pdf";  // destination PDF after alignment change

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a FormEditor instance bound to the input and output files
        FormEditor formEditor = new FormEditor(inputPath, outputPath);

        // Set the horizontal alignment of the field named "Address" to center
        // FormFieldFacade.AlignCenter is the constant for center alignment
        bool success = formEditor.SetFieldAlignment("Address", FormFieldFacade.AlignCenter);
        Console.WriteLine($"SetFieldAlignment succeeded: {success}");

        // Persist the changes to the output PDF
        formEditor.Save();
    }
}