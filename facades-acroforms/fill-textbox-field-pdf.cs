using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF containing a text box field
        const string inputPdf = "input.pdf";
        // Output PDF after filling the field
        const string outputPdf = "filled_output.pdf";
        // Name of the text box field (case‑sensitive)
        const string fieldName = "TextBox1";
        // Value to set in the field
        const string fieldValue = "Hello, Aspose!";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Form is a facade for AcroForm operations; it implements IDisposable.
        using (Form form = new Form(inputPdf))
        {
            // Fill the specified text box field with the desired value.
            // Returns true if the field was found and filled successfully.
            bool filled = form.FillField(fieldName, fieldValue);
            if (!filled)
            {
                Console.Error.WriteLine($"Field \"{fieldName}\" not found or could not be filled.");
                return;
            }

            // Save the modified document to the output path.
            form.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with filled field to \"{outputPdf}\".");
    }
}