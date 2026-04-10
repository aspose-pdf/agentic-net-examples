using System;
using System.IO;
using Aspose.Pdf.Facades;   // FormEditor, PropertyFlag
using Aspose.Pdf;          // Document (if needed)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // PDF containing the form
        const string outputPdf = "output.pdf";  // PDF with required field set

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // FormEditor works with two file paths: source and destination.
        // It loads the source PDF, modifies the form, and saves to the destination.
        using (FormEditor formEditor = new FormEditor(inputPdf, outputPdf))
        {
            // Set the "Agreement" field as required.
            // This adds the Required flag to the field definition.
            bool success = formEditor.SetFieldAttribute("Agreement", PropertyFlag.Required);

            if (!success)
            {
                Console.Error.WriteLine("Failed to set the required attribute on the field 'Agreement'.");
            }

            // Save the changes to the output PDF.
            formEditor.Save();
        }

        Console.WriteLine($"Processed PDF saved to '{outputPdf}'.");
    }
}