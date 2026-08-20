using System;
using System.IO;
using Aspose.Pdf.Facades;   // FormEditor, FormFieldFacade
using Aspose.Pdf;           // FormFieldFacade constants

class Program
{
    static void Main()
    {
        // Input PDF containing the form and the field named "Address"
        const string inputPdf  = "input.pdf";
        // Output PDF where the alignment change will be saved
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // FormEditor implements IDisposable, so wrap it in a using block.
        // Constructor takes the source PDF and the destination PDF.
        using (FormEditor editor = new FormEditor(inputPdf, outputPdf))
        {
            // Set the horizontal alignment of the field "Address" to center.
            // FormFieldFacade.AlignCenter is the constant for center alignment.
            bool result = editor.SetFieldAlignment("Address", FormFieldFacade.AlignCenter);

            if (!result)
            {
                Console.Error.WriteLine("Failed to set alignment for field 'Address'.");
            }

            // Save the changes to the output PDF.
            editor.Save();
        }

        Console.WriteLine($"Alignment of field 'Address' set to center. Output saved to '{outputPdf}'.");
    }
}