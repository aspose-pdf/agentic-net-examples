using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // -----------------------------------------------------------------
        // Step 1: Adjust the appearance of the field named "Price"
        // -----------------------------------------------------------------
        using (FormEditor editor = new FormEditor())
        {
            // Load the PDF document into the FormEditor facade
            editor.BindPdf(inputPdf);

            // Example appearance flag – make the field printable.
            // (SetFieldAppearance works with AnnotationFlags.)
            editor.SetFieldAppearance("Price", AnnotationFlags.Print);

            // Persist the appearance changes
            editor.Save(outputPdf);
        }

        // -----------------------------------------------------------------
        // Step 2: Set the field value formatted with two decimal places
        // -----------------------------------------------------------------
        using (Form form = new Form(outputPdf))
        {
            // Ensure the value is formatted to two decimal places
            string formattedPrice = 123.45.ToString("F2"); // replace with your actual value

            // Fill the "Price" field with the formatted string
            form.FillField("Price", formattedPrice);

            // Save the final PDF
            form.Save(outputPdf);
        }

        Console.WriteLine($"Field 'Price' updated and saved to '{outputPdf}'.");
    }
}