using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF with AcroForm
        const string outputPdf = "justified_output.pdf"; // result PDF
        const string fieldName = "MyTextBox";          // fully qualified name of the textbox field

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // FormEditor binds the source PDF and prepares to write the result to outputPdf
            using (FormEditor editor = new FormEditor(inputPdf, outputPdf))
            {
                // Set horizontal alignment to justified (value 3) and vertical alignment to top (value 0)
                // Alignment values: 0 = Left, 1 = Center, 2 = Right, 3 = Justify
                editor.SetFieldAlignment(fieldName, 3);
                editor.SetFieldAlignmentV(fieldName, 0);

                // Save the modified document
                editor.Save(outputPdf);
            }

            Console.WriteLine($"Justified PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}