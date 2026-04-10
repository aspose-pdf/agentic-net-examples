using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF containing the AcroForm
        const string outputPdf = "filled_output.pdf"; // destination PDF after filling

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use the Form facade (Aspose.Pdf.Facades) to fill an AcroForm text field.
        // The Form class provides the FillField method which is the correct API for this task.
        // It works with full field names; here we assume the field name is exactly "CustomerName".
        Form form = new Form(inputPdf, outputPdf);
        bool filled = form.FillField("CustomerName", "Acme Corporation");

        if (!filled)
        {
            Console.Error.WriteLine("Field 'CustomerName' was not found or could not be filled.");
        }
        else
        {
            // Save the updated PDF. The Form constructor with (input, output) already sets the target,
            // but calling Save explicitly makes the intent clear.
            form.Save();
            Console.WriteLine($"Field filled and saved to '{outputPdf}'.");
        }
    }
}