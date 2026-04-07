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

        // Bind the PDF using PdfContentEditor (required by the task description)
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Fill the AcroForm text field "CustomerName" using the Form class
        Form form = new Form(inputPath);
        bool filled = form.FillField("CustomerName", "Acme Corporation");
        if (!filled)
        {
            Console.Error.WriteLine("Field 'CustomerName' not found.");
        }

        // Save the updated PDF
        form.Save(outputPath);

        // Close the editor (PdfContentEditor does not implement IDisposable)
        editor.Close();

        Console.WriteLine($"Form field filled and saved to '{outputPath}'.");
    }
}
