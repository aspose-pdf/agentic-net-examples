using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "filled.pdf";
        const string fieldName = "TextBox1";
        const string fieldValue = "Hello World";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize the Form facade with the source PDF
        using (Form form = new Form(inputPdf))
        {
            // Fill the specified text box field
            bool success = form.FillField(fieldName, fieldValue);
            if (!success)
            {
                Console.Error.WriteLine($"Failed to fill field '{fieldName}'.");
            }

            // Save the updated PDF to a new file
            form.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }
}