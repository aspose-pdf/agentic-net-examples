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
        const string fieldName = "OrderNumber";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Open the PDF document with FormEditor (facade for form operations)
        using (FormEditor formEditor = new FormEditor())
        {
            // Bind the source PDF
            formEditor.BindPdf(inputPdf);

            // Set a standard field attribute (e.g., Required) – Aspose.Pdf does not support
            // arbitrary custom attributes like "data-id". Only predefined flags can be set.
            // Here we demonstrate setting the Required flag as an example.
            bool attributeSet = formEditor.SetFieldAttribute(fieldName, PropertyFlag.Required);
            if (!attributeSet)
            {
                Console.Error.WriteLine($"Failed to set attribute on field '{fieldName}'.");
            }

            // Save the modified PDF
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }
}