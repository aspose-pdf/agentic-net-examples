using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // PDF containing the push button named "ShowInfo"
        const string outputPdf = "output.pdf";  // PDF with the JavaScript attached

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Open the PDF, attach JavaScript to the button, and save the result.
        using (FormEditor formEditor = new FormEditor())
        {
            // Load the source PDF.
            formEditor.BindPdf(inputPdf);

            // Set JavaScript for the push button field "ShowInfo".
            // This replaces any existing script on the button.
            bool scriptSet = formEditor.SetFieldScript(
                "ShowInfo",
                "app.alert('Form loaded');"
            );

            if (!scriptSet)
            {
                Console.Error.WriteLine("Failed to set JavaScript on the button.");
                return;
            }

            // Save the modified PDF.
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"JavaScript attached and saved to '{outputPdf}'.");
    }
}