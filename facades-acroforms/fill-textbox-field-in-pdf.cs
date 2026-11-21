using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // PDF containing the textbox field
        const string outputPdf = "filled_output.pdf";
        const string fieldName = "TextBox1";       // name of the form field to fill
        const string fieldValue = "Hello, Aspose!";

        // Verify the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // Form is a Facades class that implements IDisposable – use a using block for deterministic cleanup
        using (Form form = new Form())
        {
            // Load the PDF document into the Form object
            form.BindPdf(inputPdf);

            // Fill the specified textbox field with the desired string value
            form.FillField(fieldName, fieldValue);

            // Save the modified PDF to a new file
            form.Save(outputPdf);
        }

        Console.WriteLine($"Form field '{fieldName}' filled and saved to '{outputPdf}'.");
    }
}