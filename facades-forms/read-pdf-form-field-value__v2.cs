using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "FormA.pdf";
        const string txtPath = "Question1Current.txt";

        // Verify the PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Bind the PDF form using Aspose.Pdf.Facades.Form
        Form form = new Form();
        form.BindPdf(pdfPath);

        // Retrieve the current value of the field named "Question1"
        // Form.GetField returns the field's value directly as a string, not a field object.
        string currentValue = form.GetField("Question1") ?? string.Empty;

        // Write the value to a text file
        File.WriteAllText(txtPath, currentValue);

        Console.WriteLine($"Current value of 'Question1' saved to '{txtPath}'.");
    }
}
