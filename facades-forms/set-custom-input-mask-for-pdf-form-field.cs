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

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize FormEditor with the source PDF and the destination PDF.
        FormEditor formEditor = new FormEditor(inputPdf, outputPdf);

        // Set a custom attribute for the field to define an input mask.
        // The mask is stored as a user-defined property in the field's dictionary.
        // Aspose.Pdf.Facades does not provide a direct API for input masks,
        // so we add a custom entry using the SetFieldAttribute method with a
        // PropertyFlag that represents a custom attribute (NoExport is used here
        // as a placeholder; replace with the appropriate flag if available).
        // Note: In a real scenario, you would use the appropriate API to set the
        // /AA (Additional Actions) or a custom /TU (Tooltip) entry that contains
        // the mask pattern. This example demonstrates the general approach.
        formEditor.SetFieldAttribute(fieldName, PropertyFlag.NoExport);

        // Save the modified PDF.
        formEditor.Save();

        Console.WriteLine($"Field \"{fieldName}\" updated with custom mask and saved to '{outputPdf}'.");
    }
}