using System;
using System.IO;
using Aspose.Pdf.Facades;   // Form class resides in this namespace

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF with AcroForm
        const string outputPdf = "output_filled.pdf"; // destination PDF

        // Verify that the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the Form facade on the source PDF
        Form form = new Form(inputPdf);

        // Fill the text field named "CustomerName" with the desired value
        bool success = form.FillField("CustomerName", "Acme Corporation");
        if (!success)
        {
            Console.Error.WriteLine("The field 'CustomerName' was not found or could not be filled.");
        }

        // Save the updated PDF to the output path
        form.Save(outputPdf);
        Console.WriteLine($"AcroForm field filled and saved to '{outputPdf}'.");
    }
}