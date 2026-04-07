using System;
using Aspose.Pdf.Facades;   // FormEditor, FormFieldFacade
using Aspose.Pdf;           // Document (if needed)

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the output PDF
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_centered.pdf";

        // Fully qualified name of the text field whose alignment should be changed.
        // Adjust this name to match the actual field in your PDF.
        const string fieldName = "myForm[0].TextField[0]";

        // Ensure the source file exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use FormEditor to modify the form field alignment.
        // FormEditor implements IDisposable via SaveableFacade, so wrap it in a using block.
        using (FormEditor editor = new FormEditor())
        {
            // Bind the existing PDF document.
            editor.BindPdf(inputPdf);

            // Set the horizontal alignment of the specified text field to center.
            // FormFieldFacade.AlignCenter is the constant for center alignment.
            bool success = editor.SetFieldAlignment(fieldName, FormFieldFacade.AlignCenter);

            if (!success)
            {
                Console.Error.WriteLine($"Failed to set alignment for field '{fieldName}'.");
            }

            // Save the modified PDF to the output path.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Alignment set to center and saved to '{outputPdf}'.");
    }
}