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

        // Use FormEditor with parameterless constructor and bind the PDF.
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPdf);

            // SetFieldAttribute can only set predefined flags (NoExport, ReadOnly, Required).
            // Custom attributes like "data-id" are not supported via this method.
            // Here we set the Required flag as an example.
            bool success = formEditor.SetFieldAttribute("OrderNumber", PropertyFlag.Required);
            Console.WriteLine($"SetFieldAttribute succeeded: {success}");

            // Save the updated PDF.
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPdf}'.");
    }
}