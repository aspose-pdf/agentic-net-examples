using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // existing PDF with a push button named "ShowInfo"
        const string outputPdf = "output.pdf";  // PDF with JavaScript attached

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF, add JavaScript to the button, and save the result.
        using (FormEditor formEditor = new FormEditor())
        {
            // Bind the existing PDF document.
            formEditor.BindPdf(inputPdf);

            // Attach a JavaScript alert that runs when the button is clicked.
            // The script uses the Acrobat JavaScript API.
            bool success = formEditor.AddFieldScript("ShowInfo", "app.alert('Form loaded');");

            if (!success)
            {
                Console.Error.WriteLine("Failed to add JavaScript to the button.");
            }

            // Save the modified PDF.
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"JavaScript attached and saved to '{outputPdf}'.");
    }
}