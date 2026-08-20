using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // Set field appearance flags for the "Price" field.
            // Here we set the field to be printable (you can combine flags as needed).
            // -----------------------------------------------------------------
            using (FormEditor formEditor = new FormEditor())
            {
                formEditor.BindPdf(doc);
                formEditor.SetFieldAppearance("Price", AnnotationFlags.Print);
            }

            // -----------------------------------------------------------------
            // Fill the "Price" field with a value formatted to two decimal places.
            // The value is provided as a string with the desired formatting.
            // -----------------------------------------------------------------
            using (Form form = new Form(doc))
            {
                // Example value; replace with your actual price as needed.
                string priceValue = "123.45"; // two decimal places
                form.FillField("Price", priceValue);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}