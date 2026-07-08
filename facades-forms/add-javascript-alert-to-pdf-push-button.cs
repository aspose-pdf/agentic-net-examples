using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // PDF containing the push button named "ShowInfo"
        const string outputPdf = "output.pdf";     // PDF with JavaScript attached

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Bind the existing PDF to the FormEditor facade
        FormEditor formEditor = new FormEditor();
        formEditor.BindPdf(inputPdf);

        // Add JavaScript to the push button "ShowInfo"
        // The script shows an alert when the button is clicked
        bool added = formEditor.AddFieldScript("ShowInfo", "app.alert('Form loaded');");
        if (!added)
        {
            Console.Error.WriteLine("Failed to add JavaScript to the button.");
        }

        // Save the modified PDF
        formEditor.Save(outputPdf);
        formEditor.Close();

        Console.WriteLine($"JavaScript attached and saved to '{outputPdf}'.");
    }
}