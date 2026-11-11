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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF, set the field as required (the Required flag automatically shows the asterisk)
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPath);

            // Mark the field "Agreement" as required. The Required flag causes the asterisk indicator to appear.
            formEditor.SetFieldAttribute("Agreement", PropertyFlag.Required);

            // Save the modified PDF
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Field 'Agreement' set to required. Saved to '{outputPath}'.");
    }
}
