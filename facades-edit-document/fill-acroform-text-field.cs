using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "filled_output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF form
            Form form = new Form(inputPdf);

            // Fill the text field named "CustomerName" with the desired value
            bool filled = form.FillField("CustomerName", "Acme Corporation");
            if (!filled)
            {
                Console.Error.WriteLine("Field 'CustomerName' not found or could not be filled.");
                return;
            }

            // Save the updated PDF
            form.Save(outputPdf);
            Console.WriteLine($"Form field filled and saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}