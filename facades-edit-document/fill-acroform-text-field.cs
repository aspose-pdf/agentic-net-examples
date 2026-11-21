using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "filled.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document.
        Document pdf = new Document(inputPath);

        // Ensure the document contains a form and the target field exists.
        if (pdf.Form != null && pdf.Form.HasField("CustomerName"))
        {
            // The field is a text box; cast and set its value.
            if (pdf.Form["CustomerName"] is TextBoxField txtField)
            {
                txtField.Value = "Acme Corporation";
            }
            else
            {
                Console.Error.WriteLine("Field 'CustomerName' is not a text box field.");
            }
        }
        else
        {
            Console.Error.WriteLine("AcroForm field 'CustomerName' not found in the PDF.");
        }

        // Save the modified PDF.
        pdf.Save(outputPath);

        Console.WriteLine($"AcroForm field 'CustomerName' filled and saved to '{outputPath}'.");
    }
}
