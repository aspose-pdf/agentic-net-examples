using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "OptionsForm.pdf";
        const string outputPath = "SizeSelected.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Form implements IDisposable, so wrap it in a using block for deterministic disposal
        using (Form form = new Form())
        {
            // Bind the PDF that contains the form fields
            form.BindPdf(pdfPath);

            // Retrieve the current selection of the radio button group named "SizeButton"
            // GetField returns the value of the selected option as a string
            string selectedSize = form.GetField("SizeButton");

            // Save the selection to a text file
            File.WriteAllText(outputPath, selectedSize ?? string.Empty);
        }

        Console.WriteLine($"Selected size saved to '{outputPath}'.");
    }
}