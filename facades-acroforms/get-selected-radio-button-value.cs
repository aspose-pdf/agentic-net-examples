using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "form.pdf";               // input PDF containing the form
        const string radioFieldName = "Gender";          // name of the radio button group

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Form is a Facades class; it does not implement IDisposable, so no using block.
            Form form = new Form();

            // Load the PDF document into the Form object.
            form.BindPdf(pdfPath);

            // Retrieve the currently selected option value of the specified radio button group.
            string selectedValue = form.GetButtonOptionCurrentValue(radioFieldName);

            Console.WriteLine($"Radio button \"{radioFieldName}\" selected value: {selectedValue}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading radio button value: {ex.Message}");
        }
    }
}