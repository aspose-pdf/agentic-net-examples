using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "filled.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document.
        Document pdf = new Document(inputPath);

        // Fill the AcroForm text field named "CustomerName".
        if (pdf.Form != null && pdf.Form["CustomerName"] is TextBoxField txtField)
        {
            txtField.Value = "Acme Corporation";
        }
        else
        {
            Console.Error.WriteLine("Form field 'CustomerName' not found or is not a text box.");
        }

        // Save the modified PDF.
        pdf.Save(outputPath);

        Console.WriteLine($"Form field filled and saved to '{outputPath}'.");
    }
}
