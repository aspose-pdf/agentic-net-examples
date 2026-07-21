using System;
using System.IO;
using Aspose.Pdf.Facades;   // FormEditor, FormFieldFacade
using Aspose.Pdf;          // Document (if needed for other operations)

// Set the alignment of the "Address" field to center in an existing PDF form.
class Program
{
    static void Main()
    {
        const string inputPdf  = "input_form.pdf";   // source PDF containing the form
        const string outputPdf = "output_form.pdf";  // PDF after alignment change

        // Ensure the source file exists.
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // FormEditor works as a disposable facade; wrap it in a using block.
        using (FormEditor editor = new FormEditor(inputPdf, outputPdf))
        {
            // Set horizontal alignment to center for the field named "Address".
            // FormFieldFacade.AlignCenter is the constant defining center alignment.
            bool result = editor.SetFieldAlignment("Address", FormFieldFacade.AlignCenter);

            if (!result)
            {
                Console.Error.WriteLine("Field \"Address\" not found or alignment could not be set.");
            }

            // Persist changes to the output file.
            editor.Save();
        }

        Console.WriteLine($"Alignment updated and saved to '{outputPdf}'.");
    }
}