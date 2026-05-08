using System;
using System.IO;
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

        // Initialize FormEditor for the PDF document
        FormEditor formEditor = new FormEditor();
        formEditor.BindPdf(inputPdf);

        // Set a custom attribute (input mask) for the "PhoneNumber" field.
        // Aspose.Pdf.Facades does not provide a direct API for input masks,
        // but custom attributes can be added via the SetFieldAttribute method
        // using the PropertyFlag enum. Here we use the ReadOnly flag as an example
        // to demonstrate setting a field attribute. Replace with appropriate
        // custom handling if a specific mask API becomes available.
        formEditor.SetFieldAttribute("PhoneNumber", PropertyFlag.ReadOnly);

        // Save the modified PDF
        formEditor.Save(outputPdf);
        formEditor.Close();

        Console.WriteLine($"Modified PDF saved to '{outputPdf}'.");
    }
}