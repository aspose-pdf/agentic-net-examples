using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF using the FormEditor facade
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPath);

            // JavaScript that clears the field when it receives focus
            string clearOnFocusJs = "this.value='';";

            // Attach the JavaScript to the field named "DiscountCode"
            formEditor.AddFieldScript("DiscountCode", clearOnFocusJs);

            // Save the updated PDF
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"JavaScript attached and saved to '{outputPath}'.");
    }
}