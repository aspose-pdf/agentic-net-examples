using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // Path to the PDF containing the form
        const string radioFieldName = "RadioGroup1"; // Name of the radio button group

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Form is a Facade class; it does not implement IDisposable, so no using block is needed.
        Form form = new Form();

        try
        {
            // Bind the PDF document to the Form object.
            form.BindPdf(pdfPath);

            // Retrieve the currently selected option value of the specified radio button group.
            string selectedValue = form.GetButtonOptionCurrentValue(radioFieldName);

            if (selectedValue != null)
                Console.WriteLine($"Selected value for '{radioFieldName}': {selectedValue}");
            else
                Console.WriteLine($"Radio button group '{radioFieldName}' has no selection or does not exist.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing form: {ex.Message}");
        }
        finally
        {
            // Release resources held by the Form object.
            form.Dispose();
        }
    }
}